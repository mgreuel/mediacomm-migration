using System;
using System.Collections.Generic;

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

        public Approval[] GetNewestApprovals(int count)
        {
            Approval[] approvals = this.Session.Query<Approval>().OrderByDescending(a => a.Id).Take(count).ToArray();

            return approvals;
        }

        public IEnumerable<KeyValuePair<string, int>> GetUrlsWithMostApprovals(int count)
        {
            IEnumerable<KeyValuePair<string, int>> urlsWithMostApprovals =
                this.Session.Query<Approval>()
                    .GroupBy(a => a.ApprovedUrl)
                    .OrderByDescending(g => g.Count())
                    .ToDictionary(g => g.Key, g => g.Count());

            return urlsWithMostApprovals;
        }
    }
}