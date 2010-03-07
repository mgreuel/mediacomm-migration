#region Using Directives

using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Forums;

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

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AdminController"/> class.</summary>
        /// <param name="forumRepository">The forum repository.</param>
        public AdminController(IForumRepository forumRepository)
        {
            this.forumRepository = forumRepository;
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

        #endregion
    }
}