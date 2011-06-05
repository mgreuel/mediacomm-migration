#region Using Directives

using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Users;

using Resources;

#endregion

namespace MediaCommMVC.Web.Core.Controllers
{
    using MediaCommMVC.Web.Core.Infrastructure;

    [Authorize]
    public class UsersController : Controller
    {
        #region Constants and Fields

        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion

        #region Public Methods

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
            return this.View(currentUser);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NHibernateActionFilter]
        public ActionResult MyProfile(string username)
        {
            MediaCommUser user = this.userRepository.GetUserByName(username);

            this.UpdateModel(user, "user", null, new[] { "Id", "LastVisit", "UserName", "DateOfBirth" });

            this.ViewData["ChangesSaved"] = General.ChangesSaved;

            this.userRepository.UpdateUser(user);
            return this.View(user);
        }

        [NHibernateActionFilter]
        public ActionResult Profile(string username)
        {
            MediaCommUser user = this.userRepository.GetUserByName(username);
            return this.View(user);
        }

        #endregion
    }
}