namespace MediaCommMVC.Core.ViewModels.Pages.Forums
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public class TopicPageViewModel
    {
        #region Properties

        public IEnumerable<PostViewModel> Posts { get; set; }

        #endregion
    }
}