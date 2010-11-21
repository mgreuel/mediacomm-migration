#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Model.Photos;
using MediaCommMVC.UI.ViewModel;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>Controller for the welcome page.</summary>
    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>The forum repository.</summary>
        private readonly IForumRepository forumRepository;

        /// <summary>The photo repository.</summary>
        private readonly IPhotoRepository photoRepository;

        /// <summary>The user repository.</summary>
        private readonly IUserRepository userRepository;

        /// <summary>The number of posts displayed per page.</summary>
        private const int PostsPerTopicPage = 10;

        /// <summary>Initializes a new instance of the <see cref="HomeController"/> class.</summary>
        /// <param name="forumRepository">The forum repository.</param>
        /// <param name="photoRepository">The photo repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public HomeController(IForumRepository forumRepository, IPhotoRepository photoRepository, IUserRepository userRepository)
        {
            Contract.Requires(forumRepository != null);
            Contract.Requires(photoRepository != null);
            Contract.Requires(userRepository != null);

            this.forumRepository = forumRepository;
            this.photoRepository = photoRepository;
            this.userRepository = userRepository;
        }

        #region Public Methods

        /// <summary>Handles Errors.</summary>
        /// <returns>The error view.</returns>
        public ActionResult Error()
        {
            return this.View();
        }

        /// <summary>Displays the welcome page containing new content.</summary>
        /// <returns>The welcome/what's new view.</returns>
        public ActionResult Index()
        {
            throw new Exception("Test");

            IEnumerable<Topic> topicsWithNewestPosts = this.forumRepository.Get10TopicsWithNewestPosts(this.userRepository.GetUserByName(this.User.Identity.Name));

            IEnumerable<PhotoAlbum> newestPhotoAlbums = this.photoRepository.Get4NewestAlbums();

            return this.View(new WhatsNewInfo { Topics = topicsWithNewestPosts, PostsPerTopicPage = PostsPerTopicPage, Albums = newestPhotoAlbums });
        }

        #endregion
    }
}