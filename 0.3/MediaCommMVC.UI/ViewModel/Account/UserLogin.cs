namespace MediaCommMVC.UI.ViewModel.Account
{
    /// <summary>The login data for a user.</summary>
    public class UserLogin
    {
        #region Properties

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>Gets or sets a value indicating whether the user should be logged in permanantely.</summary>
        /// <value><c>true</c> if the user should be logged in permanantely; otherwise, <c>false</c>.</value>
        public bool RememberMe { get; set; }

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        public string UserName { get; set; }

        #endregion
    }
}