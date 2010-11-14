namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    ///   Contains information needed for creating an user.
    /// </summary>
    public class CreateUserInfo
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the mail address.
        /// </summary>
        /// <value>The mail address.</value>
        public string MailAddress { get; set; }

        /// <summary>
        ///   Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        ///   Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        #endregion
    }
}