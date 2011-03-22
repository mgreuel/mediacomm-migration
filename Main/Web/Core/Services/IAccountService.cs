namespace MediaCommMVC.Core.Services
{
    #region Using Directives

    using MediaCommMVC.Core.ViewModel;

    #endregion

    public interface IAccountService
    {
        #region Public Methods

        bool LoginDataIsValid(LogOnViewModel logOnViewModel);

        #endregion
    }
}