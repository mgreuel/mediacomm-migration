namespace MediaCommMVC.Core.Services
{
    #region Using Directives

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Helpers;
    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Model;
    using MediaCommMVC.Core.Model.Forums;
    using MediaCommMVC.Core.ViewModels;
    using MediaCommMVC.Core.ViewModels.Pages.Forums;

    using NHibernate.Linq;

    #endregion

    public class ForumsService : IForumsService
    {
        #region Constants and Fields

        private readonly MediaCommUser currentUser;

        private readonly ISessionContainer sessionContainer;

        private readonly TimeSpan topicUnreadValidity = new TimeSpan(days: 30, hours: 0, minutes: 0, seconds: 0);

        #endregion

        #region Constructors and Destructors

        public ForumsService(MediaCommUser currentUser, ISessionContainer sessionContainer)
        {
            this.currentUser = currentUser;
            this.sessionContainer = sessionContainer;
        }

        #endregion

        #region Implemented Interfaces

        #region IForumsService

        public ForumPageViewModel GetForumPage(int id, int page)
        {
            Forum forum = this.sessionContainer.CurrentSession.Get<Forum>(id);

            ForumPageViewModel forumPageViewModel = Mapper.Map<Forum, ForumPageViewModel>(forum);

            IEnumerable<Topic> topics =
                forum.Topics.Where(t => !t.ExcludedUsers.Contains(this.currentUser)).OrderByDescending(t => t.DisplayPriority).ThenByDescending(
                    t => t.LastPostTime).Skip((page - 1) * ForumPageViewModel.TopicsPerPage).Take(ForumPageViewModel.TopicsPerPage).ToList();

            IEnumerable<TopicRead> topicsReadInfo =
                this.sessionContainer.CurrentSession.Query<TopicRead>().Where(
                    tr => tr.ReadByUser == this.currentUser && topics.Contains(tr.ReadTopic) && tr.LastVisit > DateTime.Now - this.topicUnreadValidity)
                    .ToList();

            topics.Where(t => t.LastPostTime > DateTime.Now - this.topicUnreadValidity).ForEach(
                t =>
                { t.HasUnreadPosts = topicsReadInfo.Count(tr => tr.ReadTopic.Id == t.Id) > 0; });

            forumPageViewModel.Topics = Mapper.Map<IEnumerable<Topic>, IEnumerable<TopicViewModel>>(topics);

            return forumPageViewModel;
        }

        public TopicViewModel AddNewTopic(CreateTopicViewModel topicViewModel, int forumId)
        {
            topicViewModel.PostText = UrlResolver.ResolveLinks(topicViewModel.PostText);

            DateTime creationTime = DateTime.Now;

            Forum forum = this.sessionContainer.CurrentSession.Load<Forum>(forumId);
            Topic topic = new Topic
                {
                    Created = creationTime,
                    CreatedBy = this.currentUser.UserName,
                    Forum = forum,
                    LastPostTime = creationTime,
                    LastPostAuthor = this.currentUser.UserName,
                    Title = topicViewModel.TopicSubject
                };
            Post post = new Post { Author = this.currentUser, Created = creationTime, Text = topicViewModel.PostText, Topic = topic };

            this.sessionContainer.CurrentSession.Save(post);

            return Mapper.Map<Topic, TopicViewModel>(topic);
        }

        public TopicPageViewModel GetTopicPage(int topicId, int page)
        {
            IEnumerable<Post> posts =
                this.sessionContainer.CurrentSession.Query<Post>().Where(p => p.Topic.Id == topicId).OrderBy(p => p.Created).Skip(
                    (page - 1) * TopicPageViewModel.PostsPerPage).Take(TopicPageViewModel.PostsPerPage).ToList();

            Topic topic = this.sessionContainer.CurrentSession.Get<Topic>(topicId);

            IEnumerable<PostViewModel> postViewModels = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts);

            TopicPageViewModel topicPageViewModel = new TopicPageViewModel
                { Posts = postViewModels, ForumTitle = topic.Forum.Title, ForumId = topic.Forum.Id, TopicTitle = topic.Title, TopicId = topic.Id };

            return topicPageViewModel;
        }

        #endregion

        #endregion
    }
}