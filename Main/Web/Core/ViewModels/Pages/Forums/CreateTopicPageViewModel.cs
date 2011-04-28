namespace MediaCommMVC.Core.ViewModels.Pages.Forums
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    #endregion

    public class CreateTopicViewModel
    {
        #region Properties

        [AllowHtml]
        public string PostText { get; set; }

        public string TopicSubject { get; set; }

        public IEnumerable<string> UserNames { get; set; }

        #endregion
    }
}