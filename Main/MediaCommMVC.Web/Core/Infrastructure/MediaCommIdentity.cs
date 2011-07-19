using System.Security.Principal;
using System.Web.Security;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public sealed class MediaCommIdentity : IIdentity
    {
        private readonly FormsAuthenticationTicket ticket;

        public MediaCommIdentity(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        public string AuthenticationType
        {
            get
            {
                return "MediaCommMVCUser";
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
    }
}