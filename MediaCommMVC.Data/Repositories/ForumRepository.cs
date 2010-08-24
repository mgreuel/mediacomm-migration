#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Core.Parameters;
using MediaCommMVC.Data.NHInfrastructure;

using NHibernate;
using NHibernate.Linq;

#endregion

namespace MediaCommMVC.Data.Repositories
{
    /// <summary>
    ///   Implements the IForumRepository using NHibernate.
    /// </summary>
    public class ForumRepository : RepositoryBase, IForumRepository
	{
        #region Constants and Fields

        /// <summary>
        ///   The timespan a topic can be marked as unread.
        /// </summary>
        private readonly TimeSpan topicUnreadValidity = new TimeSpan(days: 30, hours: 0, minutes: 0, seconds: 0);

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ForumRepository" /> class.
        /// </summary>
        /// <param name = "sessionManager">The NHibernate session manager.</param>
        /// <param name = "configAccessor">The config Accessor.</param>
        /// <param name = "logger">The logger.</param>
        public ForumRepository(ISessionManager sessionManager, IConfigAccessor configAccessor, ILogger logger)
			: base(sessionManager, configAccessor, logger)
		{
		}

        #endregion

        #region Implemented Interfaces

        #region IForumRepository

        /// <summary>
        ///   Adds the the forum to the persistence layer.
        /// </summary>
        /// <param name = "forum">The forum to add.</param>
        public void AddForum(Forum forum)
		{
			this.Logger.Debug("Adding forum to the DB: " + forum);

			this.InvokeTransaction(s => s.Save(forum));
		}

        /// <summary>
        ///   Adds the post.
        /// </summary>
        /// <param name = "post">The post to add.</param>
        public void AddPost(Post post)
		{
			this.Logger.Debug("Adding Post: " + post);

			this.InvokeTransaction(
				delegate(ISession session)
				{
					session.Save(post);
					UpdateLastPostInfo(session, post);
				});
		}

        /// <summary>
        ///   Adds the topic.
        /// </summary>
        /// <param name = "topic">The topic.</param>
        /// <param name = "post">The topic's first post.</param>
        /// <returns>The added topic.</returns>
        public Topic AddTopic(Topic topic, Post post)
		{
			this.Logger.Debug("Adding topic {0} with first post: {1}", topic, post);

			this.InvokeTransaction(
				delegate(ISession session)
				{
					topic.LastPostAuthor = post.Author.UserName;
					topic.LastPostTime = DateTime.Now;
					topic.CreatedBy = post.Author.UserName;
					topic.Created = DateTime.Now;
					post.Created = DateTime.Now;
					post.Topic = topic;

					session.Save(topic);
					session.Save(post);

					UpdateLastPostInfo(session, post);
				});

			this.Logger.Debug("Finished adding topic");

			return topic;
		}

        /// <summary>
        ///   Deletes the forum.
        /// </summary>
        /// <param name = "forum">The forum.</param>
        public void DeleteForum(Forum forum)
		{
			this.Logger.Debug("Deleting forum: " + forum);

			this.InvokeTransaction(
				delegate(ISession session)
				{
					session.Linq<Topic>().Where(t => t.Forum.Equals(forum)).ToList().ForEach(t => this.DeleteTopic(t, session));

					session.Delete(forum);
				});

			this.Logger.Debug("Finished deleting forum");
		}

        /// <summary>
        ///   Deletes the post.
        /// </summary>
        /// <param name = "post">The post to delete.</param>
        public void DeletePost(Post post)
		{
			this.Logger.Debug("Deleting post:" + post);

			this.InvokeTransaction(
				delegate(ISession session)
				{
					session.Delete(post);

					// delete the topic, if the post is the first one.
					if (this.PostWasTheFirstInTopic(session, post))
					{
						this.Logger.Debug("Post was the first in its topic. Topic will also be deleted");

						this.DeleteTopic(post.Topic, session);

						Post lastPost =
							session.Linq<Post>().Where(p => p.Topic.Forum.Id.Equals(post.Topic.Forum.Id)).OrderByDescending(
								p => p.Created).ThenByDescending(p => p.Id).First();

						UpdateLastPostInfo(session, lastPost);
					}
					else
					{
						Post lastPost =
							session.Linq<Post>().Where(p => p.Topic.Id.Equals(post.Topic.Id)).OrderByDescending(
								p => p.Created).ThenByDescending(p => p.Id).First();

						UpdateLastPostInfo(session, lastPost);
					}
				});
		}

        /// <summary>
        ///   Gets all forums.
        /// </summary>
        /// <param name = "currentUser">The current user.</param>
        /// <returns>The list of forums.</returns>
        public IEnumerable<Forum> GetAllForums(MediaCommUser currentUser)
		{
			List<Forum> allForums = this.Session.Linq<Forum>().ToList();

			foreach (Forum forum in allForums)
			{
				forum.HasUnreadTopics =
				   bool.Parse(this.Session.CreateSQLQuery(
						@"select case when COUNT(id) = 0 then 'False' else 'True' end
						from ForumTopics 
						where ForumID = :forumId
							and LastPostTime > DATEADD(day, -30, GETDATE())
							and Id not in 
								(select ReadTopicID from TopicRead where ReadByUserID = :userId and LastVisit > DATEADD(day, -30, GETDATE()))")
					.SetParameter("forumId", forum.Id)
					.SetParameter("userId", currentUser.Id)
					.UniqueResult<string>());
			}

			this.Logger.Debug("Got {0} forums", allForums.Count());

			return allForums;
		}

        /// <summary>
        ///   Gets a single forum by id.
        /// </summary>
        /// <param name = "id">The forum's id.</param>
        /// <returns>The forum with the provided id.</returns>
        public Forum GetForumById(int id)
		{
			this.Logger.Debug("Getting forum with the id '{0}'", id);

			Forum forum = this.Session.Get<Forum>(id);

			this.Logger.Debug("Got the forum " + forum.Title);

			return forum;
		}

        /// <summary>
        ///   Gets the last page number for a topic.
        /// </summary>
        /// <param name = "topicId">The topic id.</param>
        /// <param name = "pageSize">Size of each page.</param>
        /// <returns>The last page.</returns>
        public int GetLastPageNumberForTopic(int topicId, int pageSize)
		{
			this.Logger.Debug("Getting the last page number for topicId '{0}' with page size '{1}'", topicId, pageSize);

			int totalPostCount = this.Session.Linq<Post>().Where(p => p.Topic.Id.Equals(topicId)).Count();
			int lastPage = ((totalPostCount - 1) / pageSize) + 1;

			this.Logger.Debug("Got '{0}' as last page number", lastPage);

			return lastPage;
		}

        /// <summary>
        ///   Gets a post by id.
        /// </summary>
        /// <param name = "id">The post id.</param>
        /// <returns>The post with the id.</returns>
        public Post GetPostById(int id)
		{
			this.Logger.Debug("Getting post with the id: '{0}'", id);

			Post post = this.Session.Get<Post>(id);

			this.Logger.Debug("Got the post: " + post);

			return post;
		}

        /// <summary>
        ///   Gets the posts for the specified page of the topic.
        /// </summary>
        /// <param name = "topicId">The topic ID.</param>
        /// <param name = "pagingParameters">The paging parameters.</param>
        /// <param name = "currentUser">The current user.</param>
        /// <returns>The posts.</returns>
        public IEnumerable<Post> GetPostsForTopic(int topicId, PagingParameters pagingParameters, MediaCommUser currentUser)
		{
			this.Logger.Debug("Getting posts for topic with id '{0}' and paging parameters: {1}", topicId, pagingParameters);

			int postsToSkip = (pagingParameters.CurrentPage - 1) * pagingParameters.PageSize;

			IEnumerable<Post> posts =
				this.Session.Linq<Post>().Where(p => p.Topic.Id.Equals(topicId))
					.OrderBy(p => p.Created)
					.ThenBy(p => p.Id)
					.Skip(postsToSkip)
					.Take(pagingParameters.PageSize).ToList();

			int lastPage = ((pagingParameters.TotalCount - 1) / pagingParameters.PageSize) + 1;
			bool isLastPage = pagingParameters.CurrentPage == lastPage;

			if (isLastPage)
			{
				this.InvokeTransaction(
					s =>
					{
						TopicRead topicRead =
							s.Linq<TopicRead>().SingleOrDefault(
								r =>
								r.ReadByUser.Id.Equals(currentUser.Id) && r.ReadTopic.Id.Equals(topicId)) ??
							new TopicRead { ReadByUser = currentUser, ReadTopic = s.Get<Topic>(topicId) };

						topicRead.LastVisit = DateTime.Now;

						s.SaveOrUpdate(topicRead);
					});
			}

			this.Logger.Debug("Got {0} posts", posts.Count());

			return posts;
		}

        /// <summary>
        ///   Gets the topic by id.
        /// </summary>
        /// <param name = "id">The forum id.</param>
        /// <returns>The forum.</returns>
        public Topic GetTopicById(int id)
		{
			this.Logger.Debug("Getting Topic with the id '{0}'", id);

			Topic topic = this.Session.Get<Topic>(id);

			this.Logger.Debug("Got the topic: " + topic);

			return topic;
		}

        /// <summary>
        ///   Gets the topics for the forum.
        /// </summary>
        /// <param name = "forumId">The forum id.</param>
        /// <param name = "pagingParameters">The paging parameters.</param>
        /// <param name = "currentUser">The current user.</param>
        /// <returns>The topics.</returns>
        public IEnumerable<Topic> GetTopicsForForum(int forumId, PagingParameters pagingParameters, MediaCommUser currentUser)
		{
			this.Logger.Debug("Getting topics for forum with id '{0}' and paging parameters: {1}", forumId, pagingParameters);

			List<Topic> topics =
				this.Session.Linq<Topic>().Where(t => t.Forum.Id.Equals(forumId)).OrderByDescending(
					t => t.DisplayPriority).ThenByDescending(t => t.LastPostTime).ThenByDescending(t => t.Id).Skip(
						(pagingParameters.CurrentPage - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).
					ToList();

			this.UpdateTopicReadStatus(topics.Where(t => t.LastPostTime > DateTime.Now - this.topicUnreadValidity), currentUser);

			this.Logger.Debug("Got '{0}' topics", topics.Count());

			return topics;
		}

        /// <summary>
        ///   Updates the forum in the persistence layer.
        /// </summary>
        /// <param name = "forum">The forum to update.</param>
        public void UpdateForum(Forum forum)
		{
			this.Logger.Debug("Updating forum: " + forum);

			this.InvokeTransaction(a => a.Update(forum));

			this.Logger.Debug("Finished updating forum");
		}

        /// <summary>
        ///   Updates the post.
        /// </summary>
        /// <param name = "post">The post to update.</param>
        public void UpdatePost(Post post)
		{
			this.Logger.Debug("Updating post: " + post);

			this.InvokeTransaction(s => s.Update(post));

			this.Logger.Debug("Finished updating post");
		}

        /// <summary>
        ///   Updates the topic.
        /// </summary>
        /// <param name = "topic">The topic.</param>
        public void UpdateTopic(Topic topic)
		{
			this.Logger.Debug("Updating topic: " + topic);

			this.InvokeTransaction(s => s.Update(topic));

			this.Logger.Debug("Finished updating topic");
		}

        #endregion

        #endregion

        #region Methods

        /// <summary>
        ///   Updates the last post info.
        /// </summary>
        /// <param name = "session">The session.</param>
        /// <param name = "post">The forum post.</param>
        private static void UpdateLastPostInfo(ISession session, Post post)
		{
			post.Topic.LastPostTime = post.Created;
			post.Topic.LastPostAuthor = post.Author.UserName;

			post.Topic.Forum.LastPostAuthor = post.Author.UserName;
			post.Topic.Forum.LastPostTime = post.Created;

			session.Update(post);
		}

        /// <summary>
        ///   Deletes the topic.
        /// </summary>
        /// <param name = "topic">The topic.</param>
        /// <param name = "session">The session.</param>
        private void DeleteTopic(Topic topic, ISession session)
		{
			this.Logger.Debug("Deleting all posts in the topic: " + topic);
			session.Linq<Post>().Where(p => p.Topic.Equals(topic)).ToList()
				.ForEach(session.Delete);

			this.Logger.Debug("Deleting topic: " + topic);
			session.Delete(topic);

			this.Logger.Debug("Finished deleting topic and all its posts");
		}

        /// <summary>
        ///   Posts the is the first in topic.
        /// </summary>
        /// <param name = "session">The session.</param>
        /// <param name = "post">The post to check..</param>
        /// <returns>Whether the post ist hte first in its topic.</returns>
        private bool PostWasTheFirstInTopic(ISession session, Post post)
		{
			this.Logger.Debug("Checking if the post '{0}' is the first of its topic", post);

			bool topicHasOlderPosts =
				session.Linq<Post>().Where(p => p.Topic.Id.Equals(post.Topic.Id) && p.Id < post.Id).Any();

			this.Logger.Debug("Post is the first in its topic: " + !topicHasOlderPosts);

			return !topicHasOlderPosts;
		}

        /// <summary>
        ///   Updates the topic read status.
        /// </summary>
        /// <param name = "topics">The topics.</param>
        /// <param name = "currentUser">The current user.</param>
        private void UpdateTopicReadStatus(IEnumerable<Topic> topics, MediaCommUser currentUser)
		{
			IEnumerable<TopicRead> readTopics = this.Session.Linq<TopicRead>().Where(r => r.LastVisit > DateTime.Now - this.topicUnreadValidity).ToList();

			foreach (Topic topic in topics)
			{
				topic.ReadByCurrentUser =
					readTopics.Any(
						r =>
						r.ReadByUser.Id.Equals(currentUser.Id) && r.ReadTopic.Id.Equals(topic.Id) &&
						topic.LastPostTime < r.LastVisit);
			}
		}

        #endregion
	}
}