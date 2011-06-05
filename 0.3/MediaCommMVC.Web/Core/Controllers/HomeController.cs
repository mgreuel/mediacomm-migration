#region Using Directives

using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.ViewModel;

#endregion

namespace MediaCommMVC.Web.Core.Controllers
{
    using MediaCommMVC.Web.Core.Infrastructure;

    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IForumRepository forumRepository;

        private readonly IPhotoRepository photoRepository;

        private readonly IUserRepository userRepository;

        private const int PostsPerTopicPage = 15;

        public HomeController(IForumRepository forumRepository, IPhotoRepository photoRepository, IUserRepository userRepository)
        {
            this.forumRepository = forumRepository;
            this.photoRepository = photoRepository;
            this.userRepository = userRepository;
        }

        #region Public Methods

        public ActionResult Error()
        {
            return this.View();
        }

        [NHibernateActionFilter]
        public ActionResult Index()
        {
            IEnumerable<Topic> topicsWithNewestPosts = this.forumRepository.Get10TopicsWithNewestPosts(this.userRepository.GetUserByName(this.User.Identity.Name));

            IEnumerable<PhotoAlbum> newestPhotoAlbums = this.photoRepository.Get4NewestAlbums();

            return this.View(new WhatsNewInfo { Topics = topicsWithNewestPosts, PostsPerTopicPage = PostsPerTopicPage, Albums = newestPhotoAlbums });
        }

        #endregion
    }
}