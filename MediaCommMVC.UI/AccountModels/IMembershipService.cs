#region Using Directives

using System.Web.Security;

#endregion

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>
    ///   The i membership service.
    /// </summary>
    public interface IMembershipService
    {
        #region Properties

        /// <summary>
        ///   Gets MinPasswordLength.
        /// </summary>
        /// <value>The min password length.</value>
        int MinPasswordLength { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///   The change password.
        /// </summary>
        /// <param name = "userName">The user name.</param>
        /// <param name = "oldPassword">The old password.</param>
        /// <param name = "newPassword">The new password.</param>
        /// <returns>The changed password.</returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        /// <summary>
        ///   The create user.
        /// </summary>
        /// <param name = "userName">The user name.</param>
        /// <param name = "password">The password.</param>
        /// <param name = "email">The email.</param>
        /// <returns>Generated Code.</returns>
        MembershipCreateStatus CreateUser(string userName, string password, string email);

        /// <summary>
        ///   The validate user.
        /// </summary>
        /// <param name = "userName">The user name.</param>
        /// <param name = "password">The password.</param>
        /// <returns>The validated user.</returns>
        bool ValidateUser(string userName, string password);

        #endregion
    }
}