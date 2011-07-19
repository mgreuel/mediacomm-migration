namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>
    ///   The i forms authentication service.
    /// </summary>
    public interface IFormsAuthenticationService
    {
        #region Public Methods

        /// <summary>
        ///   The sign in.
        /// </summary>
        /// <param name = "userName">The user name.</param>
        /// <param name = "createPersistentCookie">The create persistent cookie.</param>
        void SignIn(string userName, bool createPersistentCookie);

        /// <summary>
        ///   The sign out.
        /// </summary>
        void SignOut();

        #endregion
    }
}