namespace MediaCommMVC.Core.Infrastructure
{
    #region Using Directives

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

            Mapper.CreateMap<Forum, ForumPageViewModel>().ForMember(f => f.Topics, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }

        #endregion
    }
}