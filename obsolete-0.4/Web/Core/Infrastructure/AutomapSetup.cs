namespace MediaCommMVC.Core.Infrastructure
{
    #region Using Directives

    using System;

    using AutoMapper;

    using MediaCommMVC.Core.Model.Forums;
    using MediaCommMVC.Core.ViewModel;
    using MediaCommMVC.Core.ViewModels;
    using MediaCommMVC.Core.ViewModels.Pages.Forums;

    #endregion

    public static class AutomapperSetup
    {
        #region Public Methods

        public static void Initialize()
        {
            Mapper.CreateMap<Forum, ForumViewModel>().ForMember(
                v => v.LastPostTime,
                opt => opt.MapFrom(f => string.IsNullOrEmpty(f.LastPostAuthor) ? string.Empty : string.Format("{0:g}", f.LastPostTime)));
            Mapper.CreateMap<Forum[], ForumViewModel[]>();

            Mapper.CreateMap<Topic[], TopicViewModel[]>();
            Mapper.CreateMap<Topic, TopicViewModel>();

            Mapper.CreateMap<Post, PostViewModel>().ForMember(p => p.CurrentUserIsAllowedToEdit, opt => opt.Ignore());

            //Mapper.CreateMap<TopicViewModel, Topic>().ForMember(t => t.Forum, opt => opt.Ignore()).ForMember(t => t.Created, opt => opt.Ignore()).
            //    ForMember(t => t.Poll, opt => opt.UseValue(null)).ForMember(t => t.ExcludedUsers, opt => opt.Ignore()).ForMember(t => t.Id, opt => opt.Ignore()).ForMember(t => t.LastPostTime, opt => opt.);

            //Mapper.CreateMap<TopicViewModel, Topic>().ConvertUsing((vm) => { return new Topic { Created = DateTime.Now, LastPostTime = DateTime.Now, Title = vm.Title, P}; });

            Mapper.CreateMap<Forum, ForumPageViewModel>().ForMember(f => f.Topics, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }

        #endregion
    }
}