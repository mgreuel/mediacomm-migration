using System.Security.Principal;
using System.Web.Security;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class MediaCommIdentity : IIdentity
    {
        private readonly FormsAuthenticationTicket ticket;

        public MediaCommIdentity(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        public string AuthenticationType
        {
            get { return "MediaCommMVCUser"; }
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
            get { return true; }
        }

        public string Name
        {
            get { return this.ticket.Name; }
        }
    }
}