using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Services
{
    using AutoMapper;

    using FluentNHibernate.Automapping;

    using NHibernate;
    using NHibernate.Mapping;

    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Model;
    using MediaCommMVC.Core.ViewModels;
    using MediaCommMVC.Core.ViewModels.Pages.Forums;

    public class ForumsService : IForumsService
    {
        private readonly IForumsRepository forumsRepository;

        private readonly MediaCommUser currentUser;

        private readonly ISession session;

        public ForumsService(IForumsRepository forumsRepository, MediaCommUser currentUser, ISession session)
        {
            this.forumsRepository = forumsRepository;
            this.currentUser = currentUser;
            this.session = session;
        }

        #region Implementation of IForumsService

        public ForumPageViewModel GetForumPage(int id, int page)
        {
            Forum forum = this.forumsRepository.GetById(id);

            ForumPageViewModel forumPageViewModel = Mapper.Map<Forum, ForumPageViewModel>(forum);

            IEnumerable<Topic> topics = forum.Topics.Where(t => !t.ExcludedUsers.Contains(this.currentUser)).OrderByDescending(t => t.DisplayPriority).ThenByDescending(
                t => t.LastPostTime).Skip((page - 1) * ForumPageViewModel.TopicsPerPage).Take(ForumPageViewModel.TopicsPerPage).ToList();
                
#warning add read status

            forumPageViewModel.Topics = Mapper.Map<IEnumerable<Topic>, IEnumerable<TopicViewModel>>(topics);

            return forumPageViewModel;
        }


        #endregion
    }
}