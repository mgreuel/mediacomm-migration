namespace MediaCommMVC.Core.Infrastructure
{
    #region Using Directives

    using System.Security.Principal;
    using System.Web.Security;

    #endregion

    public class MediaCommIdentity : IIdentity
    {
        #region Constants and Fields

        private readonly FormsAuthenticationTicket ticket;

        #endregion

        #region Constructors and Destructors

        public MediaCommIdentity(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        #endregion

        #region Properties

        public string AuthenticationType
        {
            get
            {
                return "MediaCommMVCUser";
            }
        }

        public string FriendlyName
        {
            get
            {
                return this.ticket.Name;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return this.ticket.Name;
            }
        }

        #endregion
    }
}