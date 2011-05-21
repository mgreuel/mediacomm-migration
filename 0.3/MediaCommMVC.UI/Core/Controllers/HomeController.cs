﻿#region Using Directives

using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.ViewModel;

#endregion

namespace MediaCommMVC.Web.Core.Controllers
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
        private const int PostsPerTopicPage = 15;

        public HomeController(IForumRepository forumRepository, IPhotoRepository photoRepository, IUserRepository userRepository)
        {
            this.forumRepository = forumRepository;
            this.photoRepository = photoRepository;
            this.userRepository = userRepository;
        }

        #region Public Methods

        /// <summary>Shows a message when an error occurs..</summary>
        /// <returns>The error view.</returns>
        public ActionResult Error()
        {
            return this.View();
        }

        /// <summary>Displays the welcome page containing new content.</summary>
        /// <returns>The welcome/what's new view.</returns>
        public ActionResult Index()
        {
            IEnumerable<Topic> topicsWithNewestPosts = this.forumRepository.Get10TopicsWithNewestPosts(this.userRepository.GetUserByName(this.User.Identity.Name));

            IEnumerable<PhotoAlbum> newestPhotoAlbums = this.photoRepository.Get4NewestAlbums();

            return this.View(new WhatsNewInfo { Topics = topicsWithNewestPosts, PostsPerTopicPage = PostsPerTopicPage, Albums = newestPhotoAlbums });
        }

        #endregion
    }
}