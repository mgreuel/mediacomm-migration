namespace MediaCommMVC.Core.Services
{
    #region Using Directives

    using MediaCommMVC.Core.ViewModels.Pages.Forums;

    #endregion

    public interface IForumsService
    {
        #region Public Methods

        ForumPageViewModel GetForumPage(int id, int page);

        #endregion
    }
}