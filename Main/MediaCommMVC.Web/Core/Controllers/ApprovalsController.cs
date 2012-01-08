using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model;
using MediaCommMVC.Web.Core.Model.Users;
using MediaCommMVC.Web.Core.ViewModel;

namespace MediaCommMVC.Web.Core.Controllers
{
    [Authorize]
    public class ApprovalsController : Controller
    {
        private readonly IApprovalsRepository approvalsRepository;

        private readonly CurrentUserContainer currentUserContainer;

        private MediaCommUser MediaCommUser
        {
            get
            {
                return this.currentUserContainer.User;
            }
        }

        public ApprovalsController(IApprovalsRepository approvalsRepository, CurrentUserContainer currentUserContainer)
        {
            this.approvalsRepository = approvalsRepository;
            this.currentUserContainer = currentUserContainer;
        }

        [HttpPost]
        [NHibernateActionFilter]
        public void Approve(string approvedUrl)
        {
            if (string.IsNullOrWhiteSpace(approvedUrl))
            {
                return;
            }

            this.approvalsRepository.AddAproval(new Approval { ApprovedBy = this.MediaCommUser, ApprovedUrl = approvedUrl });
        }

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult GetApprovalsForUrls(string[] approvalUrls)
        {
            ApprovalViewModel[] approvals =
                this.approvalsRepository.GetApprovalsForUrls(approvalUrls)
                        .Select(a => new ApprovalViewModel { Url = a.ApprovedUrl, ApprovedByUsername = a.ApprovedBy.UserName })
                        .ToArray();

            return this.Json(approvals, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        [NHibernateActionFilter]
        public ActionResult NewestApprovals(int count)
        {
            ApprovalViewModel[] newestApprovals = this.approvalsRepository.GetNewestApprovals(count)
                .Select(a => new ApprovalViewModel { Url = a.ApprovedUrl, ApprovedByUsername = a.ApprovedBy.UserName })
                .ToArray();

            return this.View(newestApprovals);
        }

        [NHibernateActionFilter]
        [ChildActionOnly]
        public ActionResult MostApprovals(int count)
        {
           IEnumerable<KeyValuePair<string, int>> urlsWithMostApprovals = this.approvalsRepository.GetUrlsWithMostApprovals(count);

           return this.View(urlsWithMostApprovals);
        }

        [NHibernateActionFilter]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
