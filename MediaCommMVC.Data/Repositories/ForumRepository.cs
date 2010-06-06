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
    /// <summary>Implements the IForumRepository using NHibernate.</summary>
    public class ForumRepository : RepositoryBase, IForumRepository
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ForumRepository"/> class.</summary>
        /// <param name="sessionManager">The NHibernate session manager.</param>
        /// <param name="configAccessor">The config Accessor.</param>
        /// <param name="logger">The logger.</param>
        public ForumRepository(ISessionManager sessionManager, IConfigAccessor configAccessor, ILogger logger)
            : base(sessionManager, configAccessor, logger)
        {
        }

        #endregion

        #region Implemented Interfaces

        #region IForumRepository

        /// <summary>Adds the the forum to the persistence layer.</summary>
        /// <param name="forum">The forum to add.</param>
        public void AddForum(Forum forum)
        {
            this.Logger.Debug("Adding forum to the DB: " + forum);

            this.InvokeTransaction(s => s.Save(forum));

            this.Logger.Debug("Finished adding forum");
        }

        /// <summary>Adds the post.</summary>
        /// <param name="post">The post to add.</param>
        public void AddPost(Post post)
        {
            this.Logger.Debug("Adding Post: " + post);

            this.InvokeTransaction(
                delegate(ISession session)
                {
                    session.Save(post);
                    this.UpdateLastPostInfo(session, post.Topic);
                });

            this.Logger.Debug("Finished adding post");
        }

        /// <summary>Adds the topic.</summary>
        /// <param name="topic">The topic.</param>
        /// <param name="post">The topic's first post.</param>
        public void AddTopic(Topic topic, Post post)
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
                });

            this.Logger.Debug("Finished adding topic");
        }

        /// <summary>Deletes the forum.</summary>
        /// <param name="forum">The forum.</param>
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

        /// <summary>Deletes the post.</summary>
        /// <param name="post">The post to delete.</param>
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
                    }
                    else
                    {
                        this.UpdateLastPostInfo(session, post.Topic);
                    }
                });

            this.Logger.Debug("Finished deleting post");
        }

        /// <summary>Gets all forums.</summary>
        /// <returns>The list of forums.</returns>
        public IEnumerable<Forum> GetAllForums()
        {
            this.Logger.Debug("Getting all forums");

            var allForums = this.Session.Linq<Forum>();

            this.Logger.Debug("Got {0} forums", allForums.Count());

            return allForums;
        }

        /// <summary>Gets a single forum by id.</summary>
        /// <param name="id">The forum's id.</param>
        /// <returns>The forum with the provided id.</returns>
        public Forum GetForumById(int id)
        {
            this.Logger.Debug("Getting forum with the id '{0}'", id);

            Forum forum = this.Session.Get<Forum>(id);

            this.Logger.Debug("Got the forum " + forum.Title);

            return forum;
        }

        /// <summary>Gets the last page number for a topic.</summary>
        /// <param name="topicId">The topic id.</param>
        /// <param name="pageSize">Size of each page.</param>
        /// <returns>The last page.</returns>
        public int GetLastPageNumberForTopic(int topicId, int pageSize)
        {
            this.Logger.Debug("Getting the last page number for topicId '{0}' with page size '{1}'", topicId, pageSize);

            int totalPostCount = this.Session.Linq<Post>().Where(p => p.Topic.Id.Equals(topicId)).Count();
            int lastPage = ((totalPostCount - 1) / pageSize) + 1;

            this.Logger.Debug("Got '{0}' as last page number", lastPage);

            return lastPage;
        }

        /// <summary>Gets a post by id.</summary>
        /// <param name="id">The post id.</param>
        /// <returns>The post with the id.</returns>
        public Post GetPostById(int id)
        {
            this.Logger.Debug("Getting post with the id: '{0}'", id);

            Post post = this.Session.Get<Post>(id);

            this.Logger.Debug("Got the post: " + post);

            return post;
        }

        /// <summary>Gets the posts for the topic.</summary>
        /// <param name="topicId">The topic ID.</param>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <returns>The posts.</returns>
        public IEnumerable<Post> GetPostsForTopic(int topicId, PagingParameters pagingParameters)
        {
            this.Logger.Debug("Getting posts for topic with id '{0}' and paging parameters: {1}", topicId, pagingParameters);

            int postsToSkip = (pagingParameters.CurrentPage - 1) * pagingParameters.PageSize;

            IEnumerable<Post> posts =
                this.Session.Linq<Post>().Where(p => p.Topic.Id.Equals(topicId))
                    .OrderBy(p => p.Created)
                    .ThenBy(p => p.Id)
                    .Skip(postsToSkip)
                    .Take(pagingParameters.PageSize).ToList();

            this.Logger.Debug("Got {0} posts", posts.Count());

            return posts;
        }

        /// <summary>Gets the topic by id.</summary>
        /// <param name="id">The forum id.</param>
        /// <returns>The forum.</returns>
        public Topic GetTopicById(int id)
        {
            this.Logger.Debug("Getting Topic with the id '{0}'", id);

            Topic topic = this.Session.Get<Topic>(id);

            this.Logger.Debug("Got the topic: " + topic);

            return topic;
        }

        /// <summary>Gets the topics for the forum.</summary>
        /// <param name="forumId">The forum id.</param>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <returns>The topics.</returns>
        public IEnumerable<Topic> GetTopicsForForum(int forumId, PagingParameters pagingParameters)
        {
            this.Logger.Debug("Getting topics for forum with id '{0}' and paging parameters: {1}", forumId, pagingParameters);

            IEnumerable<Topic> topics =
                this.Session.Linq<Topic>().Where(t => t.Forum.Id.Equals(forumId))
                    .OrderByDescending(t => t.DisplayPriority)
                    .ThenByDescending(t => t.LastPostTime)
                    .ThenByDescending(t => t.Id)
                    .Skip((pagingParameters.CurrentPage - 1) * pagingParameters.PageSize)
                    .Take(pagingParameters.PageSize).ToList();

            this.Logger.Debug("Got '{0}' topics", topics.Count());

            return topics;
        }

        /// <summary>Updates the forum in the persistence layer.</summary>
        /// <param name="forum">The forum to update.</param>
        public void UpdateForum(Forum forum)
        {
            this.Logger.Debug("Updating forum: " + forum);

            this.InvokeTransaction(a => a.Update(forum));

            this.Logger.Debug("Finished updating forum");
        }

        /// <summary>Updates the post.</summary>
        /// <param name="post">The post to update.</param>
        public void UpdatePost(Post post)
        {
            this.Logger.Debug("Updating post: " + post);

            this.InvokeTransaction(s => s.Update(post));

            this.Logger.Debug("Finished updating post");
        }

        /// <summary>Updates the topic.</summary>
        /// <param name="topic">The topic.</param>
        public void UpdateTopic(Topic topic)
        {
            this.Logger.Debug("Updating topic: " + topic);

            this.InvokeTransaction(s => s.Update(topic));

            this.Logger.Debug("Finished updating topic");
        }

        /// <summary>Updates the forums with information about unread posts.</summary>
        /// <param name="forums">The forums.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The updated forums.</returns>
        public IEnumerable<Forum> UpdateUnreadPosts(IEnumerable<Forum> forums, string userName)
        {
            this.Logger.Debug("Updating forums unread posts status for username: '{0}'", userName);

            DateTime? lastVisit =
                this.Session.Linq<MediaCommUser>().Where(u => u.UserName.Equals(userName)).Single().LastVisit;

            if (lastVisit != null)
            {
                this.Logger.Debug("User's last visit: " + lastVisit);
                foreach (Forum forum in forums)
                {
                    forum.HasUnreadTopics = this.Session.Linq<Post>().Where(p => p.Topic.Forum.Id.Equals(forum.Id) && p.Created > lastVisit).Any();
                    this.Logger.Debug("User has unread posts in the forum '{0}': {1}", forum, forum.HasUnreadTopics);
                }
            }

            this.Logger.Debug("Finished updating forums unread status");

            return forums;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>Deletes the topic.</summary>
        /// <param name="topic">The topic.</param>
        /// <param name="session">The session.</param>
        private void DeleteTopic(Topic topic, ISession session)
        {
            this.Logger.Debug("Deleting all posts in the topic: " + topic);
            session.Linq<Post>().Where(p => p.Topic.Equals(topic)).ToList()
                .ForEach(session.Delete);

            this.Logger.Debug("Deleting topic: " + topic);
            session.Delete(topic);

            this.Logger.Debug("Finished deleting topic and all its posts");
        }

        /// <summary>Posts the is the first in topic.</summary>
        /// <param name="session">The session.</param>
        /// <param name="post">The post to check..</param>
        /// <returns>Whether the post ist hte first in its topic.</returns>
        private bool PostWasTheFirstInTopic(ISession session, Post post)
        {
            this.Logger.Debug("Checking if the post '{0}' is the first of its topic", post);

            bool topicHasOlderPosts =
                session.Linq<Post>().Where(p => p.Topic.Id.Equals(post.Topic.Id) && p.Id < post.Id).Any();

            this.Logger.Debug("Post is the first in its topic: " + !topicHasOlderPosts);

            return !topicHasOlderPosts;
        }

        /// <summary>Updates the last post info.</summary>
        /// <param name="session">The session.</param>
        /// <param name="topic">The topic.</param>
        private void UpdateLastPostInfo(ISession session, Topic topic)
        {
            this.Logger.Debug("Updating topic's last post info. Topic: " + topic);

            topic.LastPostAuthor =
                session.Linq<Post>().Where(p => p.Topic.Id.Equals(topic.Id))
                    .OrderByDescending(p => p.Created)
                    .ThenByDescending(p => p.Id).First().Author.UserName;

            topic.LastPostTime =
                session.Linq<Post>().Where(p => p.Topic.Id.Equals(topic.Id))
                    .OrderByDescending(p => p.Created)
                    .ThenByDescending(p => p.Id).First().Created;

            this.Logger.Debug("Setting lastPostAuthor to '{0}' and lastPostTIme to '{1}'", topic.LastPostAuthor, topic.LastPostTime);

            session.Update(topic);
        }

        #endregion
    }
}