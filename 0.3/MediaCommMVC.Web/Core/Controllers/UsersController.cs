using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Helpers;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model;
using MediaCommMVC.Web.Core.Model.Users;

using Resources;

namespace MediaCommMVC.Web.Core.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [NHibernateActionFilter]
        public ActionResult Index()
        {
            IEnumerable<MediaCommUser> users = this.userRepository.GetAllUsers();
            return this.View(users);
        }

        [NHibernateActionFilter]
        public ActionResult MyProfile()
        {
            MediaCommUser currentUser = this.userRepository.GetUserByName(this.User.Identity.Name);

            IEnumerable<SelectListItem> notificationIntervalList = NotificationInterval.None.ToSelectList();
            ViewData["NotificationIntervals"] = notificationIntervalList;

            return this.View(currentUser);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NHibernateActionFilter]
        public ActionResult MyProfile(string username)
        {
            MediaCommUser user = this.userRepository.GetUserByName(username);

            this.UpdateModel(user, null, null, new[] { "Id", "LastVisit", "UserName" });

            this.ViewData["ChangesSaved"] = General.ChangesSaved;

            IEnumerable<SelectListItem> notificationIntervalList = NotificationInterval.None.ToSelectList();
            ViewData["NotificationIntervals"] = notificationIntervalList;

            this.userRepository.UpdateUser(user);
            return this.View(user);
        }

        [NHibernateActionFilter]
        public ActionResult Profile(string username)
        {
            MediaCommUser user = this.userRepository.GetUserByName(username);

            return this.View(user);
        }
    }
}