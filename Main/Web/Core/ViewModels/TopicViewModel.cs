namespace MediaCommMVC.Core.ViewModels
{
    using MediaCommMVC.Core.Helpers;
    using MediaCommMVC.Core.Model;

    public class TopicViewModel
    {
        public TopicDisplayPriority DisplayPriority { get; set; }

        public bool HasUnreadPosts { get; set; }

        public string Title { get; set; }

        public string Id { get; set; }

        public string CreatedBy { get; set; }

        public string LastPostAuthor { get; set; }

        public string UrlFriendlyTitle
        {
            get
            {
                return UrlEncoder.ToFriendlyUrl(this.Title);
            }
        }

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


        public string PostCount { get; set; }

        public string LastPostTime { get; set; }
    }
}