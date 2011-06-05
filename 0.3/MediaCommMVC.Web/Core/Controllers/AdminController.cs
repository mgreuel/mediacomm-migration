#region Using Directives

using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Videos;

#endregion

namespace MediaCommMVC.Web.Core.Controllers
{
    /// <summary>
    ///   The Admin controller.
    /// </summary>
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        #region Constants and Fields

        /// <summary>The forum repository.</summary>
        private readonly IForumRepository forumRepository;

        /// <summary>The photo repository.</summary>
        private readonly IPhotoRepository photoRepository;

        private readonly IVideoRepository videoRepository;

        /// <summary>The user repository.</summary>
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="forumRepository">The forum repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="photoRepository">The photo repository.</param>
        /// <param name="videoRepository">The video repository.</param>
        public AdminController(
            IForumRepository forumRepository, IUserRepository userRepository, IPhotoRepository photoRepository, IVideoRepository videoRepository)
        {
            this.forumRepository = forumRepository;
            this.userRepository = userRepository;
            this.photoRepository = photoRepository;
            this.videoRepository = videoRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>Displays the category created page.</summary>
        /// <returns>The category created view.</returns>
        public ActionResult CategoryCreated()
        {
            return this.View();
        }

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

        /// <summary>Shows the create photo category page.</summary>
        /// <returns>The create photo category view.</returns>
        [HttpGet]
        public ActionResult CreatePhotoCategory()
        {
            return this.View();
        }


        /// <summary>Shows the create video category page.</summary>
        /// <returns>The create video category view.</returns>
        [HttpGet]
        public ActionResult CreateVideoCategory()
        {
            return this.View();
        }

        /// <summary>Creates the video category.</summary>
        /// <param name="videoCategory">The video category.</param>
        /// <returns>Redirection to the category created page.</returns>
        [HttpPost]
        public ActionResult CreateVideoCategory(VideoCategory videoCategory)
        {
            this.videoRepository.AddCategory(videoCategory);

            this.ViewData["categoyName"] = videoCategory.Name;

            return this.RedirectToAction("CategoryCreated");
        }

        /// <summary>Creates the photo category.</summary>
        /// <param name="photoCategory">The photo category.</param>
        /// <returns>Redirection to the category created page.</returns>
        [HttpPost]
        public ActionResult CreatePhotoCategory(PhotoCategory photoCategory)
        {
            this.photoRepository.AddCategory(photoCategory);

            this.ViewData["categoyName"] = photoCategory.Name;

            return this.RedirectToAction("CategoryCreated");
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

        /// <summary>Shows the user created page.</summary>
        /// <returns>The user created view.</returns>
        [HttpGet]
        public ActionResult UserCreated()
        {
            return this.View();
        }

        #endregion
    }
}