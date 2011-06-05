#region Using Directives

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Helpers;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Videos;
using MediaCommMVC.Web.Core.ViewModel;

#endregion

namespace MediaCommMVC.Web.Core.Controllers
{
    [Authorize]
    public class VideosController : Controller
    {
        #region Constants and Fields

        private readonly IUserRepository userRepository;

        private readonly IVideoRepository videoRepository;

        #endregion

        #region Constructors and Destructors

        public VideosController(IVideoRepository videoRepository, IUserRepository userRepository)
        {
            this.videoRepository = videoRepository;
            this.userRepository = userRepository;
        }

        #endregion

        #region Public Methods

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult AddVideo(Video video, VideoCategory category)
        {
            video.Uploader = this.userRepository.GetUserByName(this.User.Identity.Name);
            video.VideoCategory = this.videoRepository.GetCategoryById(category.Id);

            this.videoRepository.AddVideo(video);

            return this.RedirectToAction("UploadSuccessFull");
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult AddVideo()
        {
            IEnumerable<VideoCategory> categories = this.videoRepository.GetAllCategories();

            IEnumerable<string> thumbnails = this.videoRepository.GetUnmappedThumbnailFiles();
            IEnumerable<string> videos = this.videoRepository.GetUnmappedVideoFiles();
            IEnumerable<string> posters = this.videoRepository.GetUnmappedPosterFiles();

            AddVideoInfo addVideoInfo = new AddVideoInfo
                {
                    AvailableCategories = categories, 
                    AvailableThumbnails = thumbnails, 
                    AvailableVideos = videos, 
                    AvailablePosters = posters
                };

            return this.View(addVideoInfo);
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Category(int id)
        {
            VideoCategory category = this.videoRepository.GetCategoryById(id);

            return this.View(category);
        }

        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "")]
        [NHibernateActionFilter]
        public ActionResult GetCategories()
        {
            IEnumerable<VideoCategory> categories = this.videoRepository.GetAllCategories();

            var categoryViewModels = categories.Select(c => new { c.Name, c.Id, c.VideoCount, EncodedName = this.Url.ToFriendlyUrl(c.Name) });

            return this.Json(categoryViewModels, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Thumbnail(int id)
        {
            Image image = this.videoRepository.GetThumbnailImage(id);

            return new ImageResult { Image = image };
        }

        [HttpGet]
        [Authorize]
        public ActionResult UploadSuccessFull()
        {
            return this.View();
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Video(int id)
        {
            Video video = this.videoRepository.GetVideoById(id);

            return this.View(video);
        }

        #endregion
    }
}