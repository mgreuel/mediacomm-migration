#region Using Directives

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Videos;
using MediaCommMVC.UI.Helpers;
using MediaCommMVC.UI.Infrastructure;
using MediaCommMVC.UI.ViewModel;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The videos controller.</summary>
    [Authorize]
    public class VideosController : Controller
    {
        #region Constants and Fields

        /// <summary>The user repository.</summary>
        private readonly IUserRepository userRepository;

        /// <summary>The video repository.</summary>
        private readonly IVideoRepository videoRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VideosController"/> class.</summary>
        /// <param name="videoRepository">The video repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public VideosController(IVideoRepository videoRepository, IUserRepository userRepository)
        {
            this.videoRepository = videoRepository;
            this.userRepository = userRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>Adds the provided video.</summary>
        /// <param name="video">The video.</param>
        /// <param name="category">The category the video belongs to.</param>
        /// <returns>A redirect to the UploadSuccessFull page.</returns>
        [HttpPost]
        public ActionResult AddVideo(Video video, VideoCategory category)
        {
            video.Uploader = this.userRepository.GetUserByName(this.User.Identity.Name);
            video.VideoCategory = this.videoRepository.GetCategoryById(category.Id);

            this.videoRepository.AddVideo(video);

            return this.RedirectToAction("UploadSuccessFull");
        }

        /// <summary>Shows the AddVideo page.</summary>
        /// <returns>The AddVideo view.</returns>
        [HttpGet]
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

        /// <summary>Displays the category with the specified id.</summary>
        /// <param name="id">The category id.</param>
        /// <returns>THe category view.</returns>
        [HttpGet]
        public ActionResult Category(int id)
        {
            VideoCategory category = this.videoRepository.GetCategoryById(id);

            return this.View(category);
        }

        /// <summary>Gets all video categories.</summary>
        /// <returns>The video categories as Json string.</returns>
        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "")]
        public ActionResult GetCategories()
        {
            IEnumerable<VideoCategory> categories = this.videoRepository.GetAllCategories();

            var categoryViewModels = categories.Select(c => new { c.Name, c.Id, c.VideoCount, EncodedName = this.Url.ToFriendlyUrl(c.Name) });

            return this.Json(categoryViewModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Displays the thumbnail of a video.</summary>
        /// <param name="id">The video id.</param>
        /// <returns>The thumbnail image.</returns>
        [HttpGet]
        public ActionResult Thumbnail(int id)
        {
            Image image = this.videoRepository.GetThumbnailImage(id);

            return new ImageResult { Image = image };
        }

        /// <summary>Shows the uploads success full page.</summary>
        /// <returns>The upload successfull view.</returns>
        [HttpGet]
        [Authorize]
        public ActionResult UploadSuccessFull()
        {
            return this.View();
        }

        /// <summary>Displays the video with the specified id.</summary>
        /// <param name="id">The video id.</param>
        /// <returns>The video view.</returns>
        [HttpGet]
        public ActionResult Video(int id)
        {
            Video video = this.videoRepository.GetVideoById(id);

            return this.View(video);
        }

        #endregion
    }
}