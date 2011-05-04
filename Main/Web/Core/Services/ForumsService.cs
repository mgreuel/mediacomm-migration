namespace MediaCommMVC.Core.Services
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using MediaCommMVC.Core.Data;
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

        private readonly IForumsRepository forumsRepository;

        private readonly ISessionContainer sessionContainer;

        #endregion

        #region Constructors and Destructors

        public ForumsService(IForumsRepository forumsRepository, MediaCommUser currentUser, ISessionContainer sessionContainer)
        {
            this.forumsRepository = forumsRepository;
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

            IEnumerable<Topic> topics = forum.Topics.Where(t => !t.ExcludedUsers.Contains(this.currentUser)).OrderByDescending(t => t.DisplayPriority).ThenByDescending(
                t => t.LastPostTime).Skip((page - 1) * ForumPageViewModel.TopicsPerPage).Take(ForumPageViewModel.TopicsPerPage).ToList();

            IEnumerable<TopicRead> topicsReadInfo =
                this.sessionContainer.CurrentSession.Query<TopicRead>().Where(
                    tr => tr.ReadByUser == this.currentUser && topics.Contains(tr.ReadTopic)).ToList();


            forumPageViewModel.Topics = Mapper.Map<IEnumerable<Topic>, IEnumerable<TopicViewModel>>(topics);

            return forumPageViewModel;
        }

        #endregion

        #endregion
    }
}