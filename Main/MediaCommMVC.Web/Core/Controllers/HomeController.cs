using System;
using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Users;
using MediaCommMVC.Web.Core.ViewModel;

namespace MediaCommMVC.Web.Core.Controllers
{
    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        private const int PostsPerTopicPage = 25;

        private readonly IForumRepository forumRepository;

        private readonly IPhotoRepository photoRepository;

        private readonly IUserRepository userRepository;

        private readonly CurrentUserContainer currentUserContainer;

        public HomeController(IForumRepository forumRepository, IPhotoRepository photoRepository, IUserRepository userRepository, CurrentUserContainer currentUserContainer)
        {
            this.forumRepository = forumRepository;
            this.photoRepository = photoRepository;
            this.userRepository = userRepository;
            this.currentUserContainer = currentUserContainer;
        }

        public ActionResult Error()
        {
            return this.View();
        }

        [NHibernateActionFilter]
        public ActionResult Index()
        {
            IEnumerable<Topic> topicsWithNewestPosts =
                this.forumRepository.Get10TopicsWithNewestPosts();

            IEnumerable<PhotoAlbum> newestPhotoAlbums = this.photoRepository.Get4NewestAlbums();

            MediaCommUser currentUser = this.currentUserContainer.User;
            currentUser.LastVisit = DateTime.Now;

            this.userRepository.UpdateUser(currentUser);

            return this.View(new WhatsNewInfo { Topics = topicsWithNewestPosts, PostsPerTopicPage = PostsPerTopicPage, Albums = newestPhotoAlbums });
        }
    }
}