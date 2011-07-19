namespace MediaCommMVC.Core.ViewModels.Pages.Forums
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using MediaCommMVC.Core.Helpers;

    #endregion

    public class TopicPageViewModel
    {
        public const int PostsPerPage = 15;

        #region Properties

        public string TopicTitle { get; set; }

        public string ForumTitle { get; set; }

        public string UrlFriendlyForumTitle
        {
            get
            {
                return UrlEncoder.ToFriendlyUrl(this.ForumTitle);
            }
        }

        public string UrlFriendlyTopicTitle
        {
            get
            {
                return UrlEncoder.ToFriendlyUrl(this.TopicTitle);
            }
        }

        public IEnumerable<PostViewModel> Posts { get; set; }

        public int ForumId { get; set; }

        public int TopicId { get; set; }

        #endregion
    }
}