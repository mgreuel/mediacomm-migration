#region Using Directives

using System.Web.Security;

#endregion

namespace MediaCommMVC.UI.AccountModels
{
    #region Services

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    /// <summary>
    ///   The forms authentication service.
    /// </summary>
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        #region Implemented Interfaces

        #region IFormsAuthenticationService

        /// <summary>
        ///   The sign in.
        /// </summary>
        /// <param name = "userName">The user name.</param>
        /// <param name = "createPersistentCookie">The create persistent cookie.</param>
        public void SignIn(string userName, bool createPersistentCookie)
        {
            ValidationUtil.ValidateRequiredStringValue(userName, "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        /// <summary>
        ///   The sign out.
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        #endregion

        #endregion
    }

    #endregion
}