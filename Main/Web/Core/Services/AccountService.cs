namespace MediaCommMVC.Core.Services
{
    #region Using Directives

    using System;

    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Model;
    using MediaCommMVC.Core.ViewModel;

    #endregion

    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;

        public AccountService(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            this.userRepository = userRepository;
        }

        #region Implemented Interfaces

        #region IAccountService

        public bool LoginDataIsValid(LogOnViewModel logOnViewModel)
        {
            MediaCommUser user = this.userRepository.GetByName(logOnViewModel.UserName);

            return user != null && user.Password.Equals(logOnViewModel.Password);
        }

        #endregion

        #endregion
    }
}