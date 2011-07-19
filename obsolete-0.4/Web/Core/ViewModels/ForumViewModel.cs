namespace MediaCommMVC.Core.ViewModel
{
    #region Using Directives

    using MediaCommMVC.Core.Helpers;

    #endregion

    public class ForumViewModel
    {
        #region Properties

        public string Description { get; set; }

        public int DisplayOrderIndex { get; set; }

        public bool HasPosts
        {
            get
            {
                return !string.IsNullOrEmpty(this.LastPostAuthor) && !string.IsNullOrEmpty(this.LastPostTime);
            }
        }

        public bool HasUnreadTopics { get; set; }

        public string IconClass
        {
            get
            {
                return this.HasUnreadTopics ? "ui-icon-mail-closed" : "ui-icon ui-icon-mail-open";
            }
        }

        public string Id { get; set; }

        public string LastPostAuthor { get; set; }

        public string LastPostTime { get; set; }

        public string PostCount { get; set; }

        public string Title { get; set; }

        public string TopicCount { get; set; }

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