#region Using Directives

using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.UI.ViewModel;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The Admin controller.</summary>
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        #region Constants and Fields

        /// <summary>The forum repository.</summary>
        private readonly IForumRepository forumRepository;

        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AdminController"/> class.</summary>
        /// <param name="forumRepository">The forum repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public AdminController(IForumRepository forumRepository, IUserRepository userRepository)
        {
            this.forumRepository = forumRepository;
            this.userRepository = userRepository;
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

            return this.RedirectToAction("Index");
        }

        /// <summary>Displays the create user page.</summary>
        /// <returns>The create forum view.</returns>
        [HttpGet]
        public ActionResult CreateUser()
        {
            return this.View();
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <returns>The user created view.</returns>
        [HttpPost]
        public ActionResult CreateUser(CreateUserInfo userInfo)
        {
            //this.userRepository.CreateUser();

            this.TempData["UserName"] = userInfo.UserName;
            return this.RedirectToAction("UserCreated");
        }

        /// <summary>Displays the admin index.</summary>
        /// <returns>The admin index view.</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>Displays the manage forums page.</summary>
        /// <returns>The manage forums view.</returns>
        public ActionResult ManageForums()
        {
            IEnumerable<Forum> forums = this.forumRepository.GetAllForums();

            return this.View(forums);
        }

        /// <summary>Saves changes made to the forums.</summary>
        /// <param name="forums">The forums to save.</param>
        /// <returns>The Manage forums view.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ManageForums([Bind(Include = "Id")] IList<Forum> forums)
        {
            return this.RedirectToAction("ManageForums");
        }

        [HttpGet]
        public ActionResult UserCreated()
        {
            return this.View();
        }

        #endregion
    }
}