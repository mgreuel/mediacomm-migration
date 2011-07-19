namespace MediaCommMVC.Core.ViewModels
{
    #region Using Directives

    using MediaCommMVC.Core.Helpers;
    using MediaCommMVC.Core.Model.Forums;

    #endregion

    public class TopicViewModel
    {
        #region Constants and Fields

        private const int PostsPerPage = 10;

        private PagingParameters pagingParameters;

        #endregion

        #region Properties

        public string CreatedBy { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        public string ExcludedUsers { get; set; }

        public bool HasExcludedUsers
        {
            get
            {
                return !string.IsNullOrEmpty(this.ExcludedUsers);
            }
        }

        public bool HasUnreadPosts { get; set; }

        public string IconClass
        {
            get
            {
                if (this.DisplayPriority == TopicDisplayPriority.Sticky)
                {
                    return this.HasUnreadPosts ? "ui-icon-alert" : "ui-icon ui-icon-info";
                }

                return this.HasUnreadPosts ? "ui-icon-mail-closed" : "ui-icon ui-icon-mail-open";
            }
        }

        public string Id { get; set; }

        public string LastPostAuthor { get; set; }

        public string LastPostTime { get; set; }

        public PagingParameters PagingParameters
        {
            get
            {
                return this.pagingParameters ??
                       (this.pagingParameters = new PagingParameters { CurrentPage = 1, PageSize = PostsPerPage, TotalCount = this.PostCount });
            }
        }

        public int PostCount { get; set; }

        public string Title { get; set; }

        public string UrlFriendlyTitle
        {
            get
            {
                return UrlEncoder.ToFriendlyUrl(this.Title);
            }
        }

        #endregion
    }
}