using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;

using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.ViewModel;

namespace MediaCommMVC.Web.Core.Controllers
{

    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        private const int PostsPerTopicPage = 15;

        private readonly IForumRepository forumRepository;

        private readonly IPhotoRepository photoRepository;

        private readonly IUserRepository userRepository;

        public HomeController(IForumRepository forumRepository, IPhotoRepository photoRepository, IUserRepository userRepository)
        {
            this.forumRepository = forumRepository;
            this.photoRepository = photoRepository;
            this.userRepository = userRepository;
        }

        public ActionResult Error()
        {
            return this.View();
        }

        [NHibernateActionFilter]
        public ActionResult Index()
        {
            IEnumerable<Topic> topicsWithNewestPosts =
                this.forumRepository.Get10TopicsWithNewestPosts(this.userRepository.GetUserByName(this.User.Identity.Name));

            IEnumerable<PhotoAlbum> newestPhotoAlbums = this.photoRepository.Get4NewestAlbums();

            return this.View(new WhatsNewInfo { Topics = topicsWithNewestPosts, PostsPerTopicPage = PostsPerTopicPage, Albums = newestPhotoAlbums });
        }
    }
}