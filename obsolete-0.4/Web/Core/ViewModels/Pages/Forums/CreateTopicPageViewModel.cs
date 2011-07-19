namespace MediaCommMVC.Core.ViewModels.Pages.Forums
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using MediaCommMVC.Core.Helpers;
    using MediaCommMVC.Core.ViewModel;

    #endregion

    public class CreateTopicViewModel
    {
        #region Properties

        //public string ForumTitle
        //{
        //    get
        //    {
        //        return this.Forum.Title;
        //    }
        //}

        [AllowHtml]
        [Required]
        [StringLength(65555, MinimumLength = 2)]
        public string PostText { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string TopicSubject { get; set; }

        //public string UrlFriendlyForumTitle
        //{
        //    get
        //    {
        //        return UrlEncoder.ToFriendlyUrl(this.ForumTitle);
        //    }
        //}

        public IEnumerable<string> UserNames { get; set; }

        public ForumViewModel Forum { get; set; }

        #endregion
    }
}