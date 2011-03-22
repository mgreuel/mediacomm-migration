namespace MediaCommMVC.Core.Services
{
    #region Using Directives

    using System;

    using MediaCommMVC.Core.ViewModel;

    #endregion

    public class AccountService : IAccountService
    {
        #region Implemented Interfaces

        #region IAccountService

        public bool LoginDataIsValid(LogOnViewModel logOnViewModel)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}