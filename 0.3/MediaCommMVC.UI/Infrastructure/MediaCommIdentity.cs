#region Using Directives

using System.Security.Principal;
using System.Web.Security;

#endregion

namespace MediaCommMVC.UI.Infrastructure
{
    /// <summary>
    /// Identity of a MediaComm user.
    /// </summary>
    public class MediaCommIdentity : IIdentity
    {
        #region Constants and Fields

        /// <summary>The authentication ticket.</summary>
        private readonly FormsAuthenticationTicket ticket;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="MediaCommIdentity"/> class.</summary>
        /// <param name="ticket">The ticket.</param>
        public MediaCommIdentity(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        #endregion

        #region Properties

        /// <summary>Gets the type of authentication used.</summary>
        /// <value>Hardcoded "MediaCommMVCUser".</value>
        /// <returns>"MediaCommMVCUser".</returns>
        public string AuthenticationType
        {
            get { return "MediaCommMVCUser"; }
        }

        /// <summary>Gets the name of the current user.</summary>
        /// <value>The username.</value>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string FriendlyName
        {
            get
            {
                return this.ticket.Name;
            }
        }

        /// <summary>Gets a value indicating whether the user has been authenticated.</summary>
        /// <value>always true.</value>
        /// <returns>always true.</returns>
        public bool IsAuthenticated
        {
            get { return true; }
        }

        /// <summary>Gets the name of the current user.</summary>
        /// <value>The username.</value>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string Name
        {
            get { return this.ticket.Name; }
        }

        #endregion
    }
}