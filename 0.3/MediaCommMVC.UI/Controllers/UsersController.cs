#region Using Directives

using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Users;

using Resources;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The users controller.</summary>
    [Authorize]
    public class UsersController : Controller
    {
        #region Constants and Fields

        /// <summary>The logger.</summary>
        private readonly ILogger logger;

        /// <summary>The user repository.</summary>
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="UsersController"/> class.</summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="logger">The logger.</param>
        public UsersController(IUserRepository userRepository, ILogger logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>Shows the users index.</summary>
        /// <returns>The users list view.</returns>
        public ActionResult Index()
        {
            IEnumerable<MediaCommUser> users = this.userRepository.GetAllUsers();
            return this.View(users);
        }

        /// <summary>Shows the current user's profile.</summary>
        /// <returns>The my profile view.</returns>
        public ActionResult MyProfile()
        {
            MediaCommUser currentUser = this.userRepository.GetUserByName(this.User.Identity.Name);
            return this.View(currentUser);
        }

        /// <summary>Saves the updated profile.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The profile updated view.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MyProfile(string username)
        {
            this.logger.Debug("Saving profile changes for user '{0}'", username);

            MediaCommUser user = this.userRepository.GetUserByName(username);

            this.UpdateModel(user, "user", null, new[] { "Id", "LastVisit", "UserName", "DateOfBirth" });

            this.ViewData["ChangesSaved"] = General.ChangesSaved;

            this.userRepository.UpdateUser(user);
            return this.View(user);
        }

        /// <summary>Shows an user profile.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The user profile view.</returns>
        public ActionResult Profile(string username)
        {
            MediaCommUser user = this.userRepository.GetUserByName(username);
            return this.View(user);
        }

        #endregion
    }
}