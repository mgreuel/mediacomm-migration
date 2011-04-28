namespace MediaCommMVC.Core.ViewModels.Pages.Forums
{
    #region Using Directives

    using System.Collections.Generic;

    using MediaCommMVC.Core.ViewModel;

    #endregion

    public class ForumsIndexViewModel
    {
        #region Properties

        public IEnumerable<ForumViewModel> Forums { get; set; }

        #endregion
    }
}