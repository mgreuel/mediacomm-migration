#region Using Directives

using System;
using System.Web.Security;

#endregion

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>
    ///   The account membership service.
    /// </summary>
    public class AccountMembershipService : IMembershipService
    {
        #region Constants and Fields

        /// <summary>
        ///   The _provider.
        /// </summary>
        private readonly MembershipProvider provider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "AccountMembershipService" /> class.
        /// </summary>
        public AccountMembershipService()
            : this(null)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "AccountMembershipService" /> class.
        /// </summary>
        /// <param name = "provider">The provider.</param>
        public AccountMembershipService(MembershipProvider provider)
        {
            this.provider = provider ?? Membership.Provider;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets MinPasswordLength.
        /// </summary>
        /// <value>The min password length.</value>
        public int MinPasswordLength
        {
            get
            {
                return this.provider.MinRequiredPasswordLength;
            }
        }

        #endregion

        #region Implemented Interfaces

        #region IMembershipService

        /// <summary>
        ///   The change password.
        /// </summary>
        /// <param name = "userName">The user name.</param>
        /// <param name = "oldPassword">The old password.</param>
        /// <param name = "newPassword">The new password.</param>
        /// <returns>The changed password.</returns>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            ValidationUtil.ValidateRequiredStringValue(userName, "userName");
            ValidationUtil.ValidateRequiredStringValue(oldPassword, "oldPassword");
            ValidationUtil.ValidateRequiredStringValue(newPassword, "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = this.provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        /// <summary>
        ///   The create user.
        /// </summary>
        /// <param name = "userName">The user name.</param>
        /// <param name = "password">The password.</param>
        /// <param name = "email">The email.</param>
        /// <returns>Generated Code.</returns>
        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            ValidationUtil.ValidateRequiredStringValue(userName, "userName");
            ValidationUtil.ValidateRequiredStringValue(password, "password");
            ValidationUtil.ValidateRequiredStringValue(email, "email");

            MembershipCreateStatus status;
            this.provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        /// <summary>
        ///   The validate user.
        /// </summary>
        /// <param name = "userName">The user name.</param>
        /// <param name = "password">The password.</param>
        /// <returns>The validated user.</returns>
        public bool ValidateUser(string userName, string password)
        {
            ValidationUtil.ValidateRequiredStringValue(userName, "userName");
            ValidationUtil.ValidateRequiredStringValue(password, "password");

            return this.provider.ValidateUser(userName, password);
        }

        #endregion

        #endregion
    }
}