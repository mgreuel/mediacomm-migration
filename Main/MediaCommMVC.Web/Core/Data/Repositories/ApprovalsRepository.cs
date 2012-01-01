using System;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model;

using NHibernate;
using NHibernate.Linq;

using System.Linq;

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    public class ApprovalsRepository : IApprovalsRepository
    {
        private readonly ISessionContainer sessionContainer;

        private ISession Session
        {
            get
            {
                return this.sessionContainer.CurrentSession;
            }
        }

        public ApprovalsRepository(ISessionContainer sessionContainer)
        {
            this.sessionContainer = sessionContainer;
        }

        public void AddAproval(Approval approval)
        {
            this.Session.Save(approval);
        }

        public Approval[] GetApprovalsForUrls(string[] approvalUrls)
        {
            Approval[] approvals =
                this.Session.Query<Approval>().Where(a => approvalUrls.Contains(a.ApprovedUrl)).ToArray();

            return approvals;
        }
    }
}