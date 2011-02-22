using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Core.Model.Videos;
using MediaCommMVC.UI.Helpers;
using MediaCommMVC.UI.Infrastructure;
using MediaCommMVC.UI.ViewModel;

namespace MediaCommMVC.UI.Controllers
{
    [Authorize]
    public class VideosController : Controller
    {
        private readonly IVideoRepository videoRepository;

        private readonly IUserRepository userRepository;

        public VideosController(IVideoRepository videoRepository, IUserRepository userRepository)
        {
            this.videoRepository = videoRepository;
            this.userRepository = userRepository;
        }

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

        [HttpPost]
        public ActionResult AddVideo(Video video, VideoCategory category)
        {
            video.Uploader = this.userRepository.GetUserByName(this.User.Identity.Name);
            video.VideoCategory = this.videoRepository.GetCategoryById(category.Id);

            this.videoRepository.AddVideo(video);

            return null;
        }

        [HttpGet]
        public ActionResult Cover(int id)
        {
            Image image = this.videoRepository.GetCoverImage(id);

            return new ImageResult { Image = image };
        }

        [HttpGet]
        public ActionResult AddVideo()
        {
            IEnumerable<VideoCategory> categories = this.videoRepository.GetAllCategories();

            IEnumerable<string> thumbnails = this.videoRepository.GetUnmappedThumbnailFiles();

            IEnumerable<string> videos = this.videoRepository.GetUnmappedVideoFiles();

            AddVideoInfo addVideoInfo = new AddVideoInfo { AvailableCategories = categories, AvailableThumbnails = thumbnails, AvailableVideos = videos };

            return this.View(addVideoInfo);
        }
    }
}
