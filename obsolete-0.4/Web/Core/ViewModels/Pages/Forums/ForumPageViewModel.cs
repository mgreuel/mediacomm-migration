namespace MediaCommMVC.Core.ViewModels.Pages.Forums
{
    #region Using Directives

    using System.Collections.Generic;

    using MediaCommMVC.Core.Helpers;

    #endregion

    public class ForumPageViewModel
    {
        #region Constants and Fields

        public const int TopicsPerPage = 25;

        private PagingParameters pagingParameters;

        #endregion

        #region Properties

        public string Id { get; set; }

        public PagingParameters PagingParameters { get
        {
            return this.pagingParameters ??
                   (this.pagingParameters = new PagingParameters { CurrentPage = 1, PageSize = TopicsPerPage, TotalCount = this.TopicCount });
        }}

        public string Title { get; set; }

        public int TopicCount  { get; set; }

        public IEnumerable<TopicViewModel> Topics { get; set; }

        public string UrlFriendlyForumTitle
        {
            get
            {
                return UrlEncoder.ToFriendlyUrl(this.Title);
            }
        }

        #endregion
    }
}