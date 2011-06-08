#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data.NHInfrastructure;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Users;
using MediaCommMVC.Web.Core.Parameters;

using NHibernate;
using NHibernate.Linq;

using Enumerable = System.Linq.Enumerable;
using Queryable = System.Linq.Queryable;

#endregion

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    using MediaCommMVC.Web.Core.Infrastructure;

    /// <summary>Implements the IForumRepository using NHibernate.</summary>
    public class ForumRepository : IForumRepository
    {
        private readonly ISessionContainer sessionContainer;

        private readonly IConfigAccessor configAccessor;

        private readonly ILogger logger;

        #region Constants and Fields

        /// <summary>The timespan a topic can be marked as unread.</summary>
        private readonly TimeSpan topicUnreadValidity = new TimeSpan(days: 30, hours: 0, minutes: 0, seconds: 0);

        #endregion

        protected ISession Session
        {
            get
            {
                return this.sessionContainer.CurrentSession;
            }
        }

        #region Constructors and Destructors

        public ForumRepository(ISessionContainer sessionContainer, IConfigAccessor configAccessor, ILogger logger)
        //: base(sessionContainer, configAccessor, logger)
        {
            this.sessionContainer = sessionContainer;
            this.configAccessor = configAccessor;
            this.logger = logger;
        }

        #endregion

        #region Implemented Interfaces

        #region IForumRepository

        public void AddForum(Forum forum)
        {
            this.Session.Save(forum);
        }

        public void AddPost(Post post)
        {
            this.Session.Save(post);
            this.UpdateLastPostInfo(post);

#warning set topicRead
        }

        public Topic AddTopic(Topic topic, Post post)
        {
            topic.LastPostAuthor = post.Author.UserName;
            topic.LastPostTime = DateTime.Now;
            topic.CreatedBy = post.Author.UserName;
            topic.Created = DateTime.Now;
            post.Created = DateTime.Now;
            post.Topic = topic;

            this.Session.Save(topic);
            this.Session.Save(post);

            this.UpdateLastPostInfo(post);

#warning set topicRead

            return topic;
        }

        public void DeleteForum(Forum forum)
        {
            this.Session.Query<Topic>().Where(t => t.Forum.Equals(forum)).ToList().ForEach(this.DeleteTopic);
        }

        public void DeletePost(Post post)
        {
            this.Session.Delete(post);

            // delete the topic, if the post is the first one.
            if (this.PostWasTheFirstInTopic(post))
            {
                this.DeleteTopic(post.Topic);

                Post lastPost =
                    this.Session.Query<Post>().Where(p => p.Topic.Forum.Id == post.Topic.Forum.Id).OrderByDescending(p => p.Created).ThenByDescending(
                        p => p.Id).First();

                this.UpdateLastPostInfo(lastPost);
            }
            else
            {
                Post lastPost =
                    this.Session.Query<Post>().Where(p => p.Topic.Id == post.Topic.Id).OrderByDescending(p => p.Created).ThenByDescending(p => p.Id).
                        First();

                this.UpdateLastPostInfo(lastPost);
            }
        }

        public IEnumerable<Topic> Get10TopicsWithNewestPosts(MediaCommUser currentUser)
        {
            List<Topic> topics = this.Session.Query<Topic>().Where(t => !t.ExcludedUsers.Contains(currentUser)).OrderByDescending(t => t.LastPostTime).Take(10).ToList();

            this.UpdateTopicReadStatus(topics.Where(t => t.LastPostTime > DateTime.Now - this.topicUnreadValidity), currentUser);

            return topics;
        }

        public IEnumerable<Forum> GetAllForums(MediaCommUser currentUser)
        {
            List<Forum> allForums = this.Session.Query<Forum>().ToList();

            // FutureValue does not work, because "Sql queries in MultiQuery are currently not supported."
            foreach (Forum forum in allForums)
            {
                forum.HasUnreadTopics =
                   bool.Parse(this.Session.CreateSQLQuery(
                        @"select case when COUNT(id) = 0 then 'False' else 'True' end
						from ForumTopics 
						where ForumID = :forumId
							and LastPostTime > DATEADD(day, -30, GETDATE())
							and Id not in 
								(select ReadTopicID from ForumTopicsRead where ReadByUserID = :userId and LastVisit > DATEADD(day, -30, GETDATE()) and LastVisit > LastPostTime)")
                    .SetParameter("forumId", forum.Id)
                    .SetParameter("userId", currentUser.Id)
                    .UniqueResult<string>());
            }

            return allForums;
        }

        public Post GetFirstUnreadPostForTopic(int id, MediaCommUser user)
        {
            /* joins are not supported by linq to nhibernate
             * Post post = (from p in this.Session.Query<Post>()
                       join tr in this.Session.Query<TopicRead>() on p.Topic.Id equals tr.ReadTopic.Id
                       where p.Topic.Id == id && tr.ReadByUser.UserName == user.UserName && p.Created > tr.LastVisit
                       orderby p.Created descending
                       select p).FirstOrDefault();*/
            DateTime date =
                (this.Session.Query<TopicRead>().SingleOrDefault(tr => tr.ReadByUser.UserName == user.UserName && tr.ReadTopic.Id == id) ??
                 new TopicRead { LastVisit = DateTime.Now.AddMonths(-1) }).LastVisit;

            // Get FIrst unread post or the newwest one if all are read
            Post post =
                this.Session.Query<Post>().Where(p => p.Topic.Id == id && p.Created > date).OrderBy(p => p.Created).FirstOrDefault() ??
                this.Session.Query<Post>().Where(p => p.Topic.Id == id).OrderByDescending(p => p.Created).First();

            return post;
        }

        public Forum GetForumById(int id)
        {
            Forum forum = this.Session.Get<Forum>(id);

            return forum;
        }

        public int GetLastPageNumberForTopic(int topicId, int pageSize)
        {
            int totalPostCount = this.Session.Query<Post>().Where(p => p.Topic.Id == topicId).Count();
            int lastPage = ((totalPostCount - 1) / pageSize) + 1;

            return lastPage;
        }

        public int GetPageNumberForPost(int postId, int topicId, int pageSize)
        {
            List<Post> posts = this.Session.Query<Post>().Where(p => p.Topic.Id == topicId).OrderBy(p => p.Created).ToList();
            Post post = posts.Single(p => p.Id == postId);

            int index = posts.IndexOf(post);

            int page = (index / pageSize) + 1;

            return page;
        }

        /// <summary>Gets the poll answer by id.</summary>
        /// <param name="answerId">The answer id.</param>
        /// <returns>The poll answer.</returns>
        public PollAnswer GetPollAnswerById(int answerId)
        {
            return this.Session.Get<PollAnswer>(answerId);
        }

        /// <summary>Gets a post by id.</summary>
        /// <param name="id">The post id.</param>
        /// <returns>The post with the id.</returns>
        public Post GetPostById(int id)
        {
            Post post = this.Session.Query<Post>().Fetch(p => p.Topic).ThenFetch(t => t.Forum).Single(p => p.Id == id);

            return post;
        }

        /// <summary>Gets the posts for the specified page of the topic.</summary>
        /// <param name="topicId">The topic ID.</param>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The posts.</returns>
        public IEnumerable<Post> GetPostsForTopic(int topicId, PagingParameters pagingParameters, MediaCommUser currentUser)
        {
            int postsToSkip = (pagingParameters.CurrentPage - 1) * pagingParameters.PageSize;

            List<Post> posts =
                this.Session.Query<Post>().Where(p => p.Topic.Id == topicId).OrderBy(p => p.Created).Skip(postsToSkip).Take(pagingParameters.PageSize)
                    .ToList();

            int lastPage = ((pagingParameters.TotalCount - 1) / pagingParameters.PageSize) + 1;
            bool isLastPage = pagingParameters.CurrentPage == lastPage;

            if (isLastPage)
            {
                TopicRead topicRead =
                    this.Session.Query<TopicRead>().SingleOrDefault(r => r.ReadByUser.Id == currentUser.Id && r.ReadTopic.Id == topicId) ??
                    new TopicRead { ReadByUser = currentUser, ReadTopic = this.Session.Load<Topic>(topicId) };

                topicRead.LastVisit = DateTime.Now;

                this.Session.SaveOrUpdate(topicRead);
            }

            return posts;
        }

        public Topic GetTopicById(int id)
        {
            Topic topic = this.Session.Query<Topic>().Fetch(t => t.Poll).Fetch(t => t.Forum).SingleOrDefault(t => t.Id == id);

#warning check excluded users

            return topic;
        }

        public IEnumerable<Topic> GetTopicsForForum(int forumId, PagingParameters pagingParameters, MediaCommUser currentUser)
        {
            List<Topic> topics =
                this.Session.Query<Topic>().Where(
                    t => t.Forum.Id == forumId && !t.ExcludedUsers.Contains(currentUser)).OrderByDescending(
                        t => t.DisplayPriority).ThenByDescending(t => t.LastPostTime).ThenByDescending(t => t.Id).Skip(
                            (pagingParameters.CurrentPage - 1) * pagingParameters.PageSize).Take(
                                pagingParameters.PageSize).ToList();

            List<int> topicIds = topics.Select(t => t.Id).ToList();

            var x = this.Session.Query<ForumTopicsExcludedUser>().ToList();

            ILookup<int, string> excludedUsernames =
                this.Session.Query<ForumTopicsExcludedUser>().Where(ex => topicIds.Contains(ex.Topic.Id)).ToLookup(
                    ex => ex.Topic.Id, ex => ex.MediaCommUser.UserName);

            foreach (Topic topic in topics)
            {
                topic.ExcludedUsernames = excludedUsernames.FirstOrDefault(ex => ex.Key == topic.Id);
            }


            this.UpdateTopicReadStatus(topics.Where(t => t.LastPostTime > DateTime.Now - this.topicUnreadValidity), currentUser);

            return topics;
        }

        public void SavePollUserAnswer(PollUserAnswer userAnswer)
        {
            this.Session.Save(userAnswer);
        }

        public void UpdateForum(Forum forum)
        {
            this.Session.Update(forum);
        }

        public void UpdatePost(Post post)
        {
            this.Session.Update(post);
        }

        public void UpdateTopic(Topic topic)
        {
            this.Session.Update(topic);
        }

        #endregion

        #endregion

        #region Methods

        private void UpdateLastPostInfo(Post post)
        {
            post.Topic.LastPostTime = post.Created;
            post.Topic.LastPostAuthor = post.Author.UserName;

            post.Topic.Forum.LastPostAuthor = post.Author.UserName;
            post.Topic.Forum.LastPostTime = post.Created;

            this.Session.Update(post);
        }

        private void DeleteTopic(Topic topic)
        {
            this.Session.Query<Post>().Where(p => p.Topic.Id == topic.Id).ToList().ForEach(this.Session.Delete);
            this.Session.Query<TopicRead>().Where(tr => tr.ReadTopic.Id == topic.Id).ForEach(this.Session.Delete);

            this.Session.Delete(topic);
        }

        private bool PostWasTheFirstInTopic(Post post)
        {
            bool topicHasOlderPosts = this.Session.Query<Post>().Where(p => p.Topic.Id == post.Topic.Id && p.Id < post.Id).Any();

            return !topicHasOlderPosts;
        }

        private void UpdateTopicReadStatus(IEnumerable<Topic> topics, MediaCommUser currentUser)
        {
            List<TopicRead> readTopics =
                this.Session.Query<TopicRead>().Fetch(tr => tr.ReadTopic).Fetch(tr => tr.ReadByUser).Where(
                    tr => tr.LastVisit > DateTime.Now - this.topicUnreadValidity && tr.ReadByUser.Id == currentUser.Id).ToList();

            foreach (Topic topic in topics)
            {
                topic.ReadByCurrentUser =
                    readTopics.Any(
                        r =>
                        r.ReadByUser.Id == currentUser.Id && r.ReadTopic.Id == topic.Id &&
                        topic.LastPostTime < r.LastVisit);
            }
        }

        #endregion
    }
}