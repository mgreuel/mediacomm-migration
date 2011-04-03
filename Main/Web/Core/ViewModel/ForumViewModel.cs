namespace MediaCommMVC.Core.ViewModel
{
    using System;
    using System.Web.Mvc;
    using MediaCommMVC.Core.Helpers;

    public class ForumViewModel
    {
        public string Title { get; set; }

        public int DisplayOrderIndex { get; set; }

        public string IconUrl
        {
            get
            {
                string filename = "folder";

                if (this.HasUnreadTopics)
                {
                    filename = filename + "_new";
                }

                return filename + ".gif";
            }
        }

        public string TopicCount { get; set; }

        public string PostCount { get; set; }

        public bool HasUnreadTopics { get; set; }

        public string Id { get; set; }

        public string Description { get; set; }

        public string UrlFriendlyTitle
        {
            get
            {
                return UrlEncoder.ToFriendlyUrl(this.Title);
            }
        }
    }
}