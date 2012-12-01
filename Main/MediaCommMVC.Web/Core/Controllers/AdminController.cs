using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Videos;

namespace MediaCommMVC.Web.Core.Controllers
{
    using MediaCommMVC.Web.Core.Infrastructure;

    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        private readonly IForumRepository forumRepository;

        private readonly IPhotoRepository photoRepository;

        private readonly IUserRepository userRepository;

        private readonly IVideoRepository videoRepository;

        public AdminController(
            IForumRepository forumRepository, IUserRepository userRepository, IPhotoRepository photoRepository, IVideoRepository videoRepository)
        {
            this.forumRepository = forumRepository;
            this.userRepository = userRepository;
            this.photoRepository = photoRepository;
            this.videoRepository = videoRepository;
        }

        public ActionResult CategoryCreated()
        {
            return this.View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CreateForum()
        {
            return this.View();
        }

        [NHibernateActionFilter]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateForum(Forum forum)
        {
            this.forumRepository.AddForum(forum);

            return this.RedirectToAction("Index", "Forums");
        }

        [HttpGet]
        public ActionResult CreatePhotoCategory()
        {
            return this.View();
        }

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult CreatePhotoCategory(PhotoCategory photoCategory)
        {
            this.photoRepository.AddCategory(photoCategory);

            this.ViewData["categoyName"] = photoCategory.Name;

            return this.RedirectToAction("CategoryCreated");
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return this.View();
        }

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult CreateUser(string username, string password, string mailAddress)
        {
            this.userRepository.CreateUser(username, password, mailAddress);

            this.TempData["UserName"] = username;
            return this.RedirectToAction("UserCreated");
        }

        [HttpGet]
        public ActionResult CreateVideoCategory()
        {
            return this.View();
        }

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult CreateVideoCategory(VideoCategory videoCategory)
        {
            this.videoRepository.AddCategory(videoCategory);

            this.ViewData["categoyName"] = videoCategory.Name;

            return this.RedirectToAction("CategoryCreated");
        }

        [HttpGet]
        public ActionResult UserCreated()
        {
            return this.View();
        }

        [HttpGet]
        [NHibernateActionFilter]
        public void ProcessImagesForAlbum(int id)
        {
            PhotoAlbum album = this.photoRepository.GetAlbumById(id);
            this.photoRepository.GenerateImagesForUnprocessedUploads(album);
        }
    }
}