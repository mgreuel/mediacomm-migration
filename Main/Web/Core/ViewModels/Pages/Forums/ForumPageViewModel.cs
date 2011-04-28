namespace MediaCommMVC.Core.ViewModels.Pages.Forums
{
    #region Using Directives

    using System.Collections.Generic;

    using MediaCommMVC.Core.Helpers;

    #endregion

    public class ForumPageViewModel
    {
        private PagingParameters pagingParameters;

        public const int TopicsPerPage = 25;

        #region Properties

        public string Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<TopicViewModel> Topics { get; set; }

        public string UrlFriendlyForumTitle
        {
            get
            {
                return UrlEncoder.ToFriendlyUrl(this.Title);
            }
        }

        public int TopicCount  { get; set; }

        #endregion

        public PagingParameters PagingParameters { get
        {
            return this.pagingParameters ??
                   (this.pagingParameters = new PagingParameters { CurrentPage = 1, PageSize = TopicsPerPage, TotalCount = this.TopicCount });
        }}
    }
}