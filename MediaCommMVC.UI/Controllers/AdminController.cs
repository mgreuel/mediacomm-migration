#region Using Directives

using System;
using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Model.Photos;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The Admin controller.</summary>
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        #region Constants and Fields

        /// <summary>
        ///   The forum repository.
        /// </summary>
        private readonly IForumRepository forumRepository;

        /// <summary>
        ///   The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// The photo repository.
        /// </summary>
        private readonly IPhotoRepository photoRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="forumRepository">The forum repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="photoRepository">The photo repository.</param>
        public AdminController(IForumRepository forumRepository, IUserRepository userRepository, IPhotoRepository photoRepository)
        {
            this.forumRepository = forumRepository;
            this.userRepository = userRepository;
            this.photoRepository = photoRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>Displays the create forum page.</summary>
        /// <returns>The create forum view.</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CreateForum()
        {
            return this.View();
        }

        /// <summary>Creates the forum.</summary>
        /// <param name="forum">The forum.</param>
        /// <returns>The admin index view.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateForum(Forum forum)
        {
            this.forumRepository.AddForum(forum);

            return this.RedirectToAction("Index", "Forums");
        }

        /// <summary>Displays the create user page.</summary>
        /// <returns>The create forum view.</returns>
        [HttpGet]
        public ActionResult CreateUser()
        {
            return this.View();
        }

        /// <summary>Creates a new user.</summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="mailAddress">The mail address.</param>
        /// <returns>The user created view.</returns>
        [HttpPost]
        public ActionResult CreateUser(string username, string password, string mailAddress)
        {
            this.userRepository.CreateUser(username, password, mailAddress);
            
            this.TempData["UserName"] = username;
            return this.RedirectToAction("UserCreated");
        }

        /// <summary>Displays the manage forums page.</summary>
        /// <returns>The manage forums view.</returns>
        public ActionResult ManageForums()
        {
            return this.View();
        }

        /// <summary>Saves changes made to the forums.</summary>
        /// <param name="forums">The forums to save.</param>
        /// <returns>The Manage forums view.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageForums([Bind(Include = "Id")] IList<Forum> forums)
        {
            return this.RedirectToAction("ManageForums");
        }

        /// <summary>
        /// Shows the create photo category page.
        /// </summary>
        /// <returns>The create photo category view.</returns>
        [HttpGet]
        public ActionResult CreatePhotoCategory()
        {
            return this.View();
        }

        /// <summary>
        /// Creates the photo category.
        /// </summary>
        /// <param name="photoCategory">The photo category.</param>
        /// <returns>Redirection to the category created page.</returns>
        [HttpPost]
        public ActionResult CreatePhotoCategory(PhotoCategory photoCategory)
        {
            this.photoRepository.AddCategory(photoCategory);

            this.ViewData["categoyName"] = photoCategory.Name;

            return this.RedirectToAction("CategoryCreated");
        }

        /// <summary>Shows the user created page.</summary>
        /// <returns>The user created view.</returns>
        [HttpGet]
        public ActionResult UserCreated()
        {
            return this.View();
        }

        #endregion

        /// <summary>
        /// Displays the category created page.
        /// </summary>
        /// <returns>The category created view.</returns>
        public ActionResult CategoryCreated()
        {
            return this.View();
        }
    }
}