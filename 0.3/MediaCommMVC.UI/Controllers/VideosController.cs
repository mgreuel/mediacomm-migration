using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Core.Model.Videos;
using MediaCommMVC.UI.Helpers;
using MediaCommMVC.UI.ViewModel;

namespace MediaCommMVC.UI.Controllers
{
    [Authorize]
    public class VideosController : Controller
    {
        private IVideoRepository videoRepository;

        public VideosController(IVideoRepository videoRepository)
        {
            this.videoRepository = videoRepository;
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

        [HttpGet]
        public ActionResult AddVideo()
        {
            IEnumerable<VideoCategory> categories = this.videoRepository.GetAllCategories();

            IEnumerable<string> thumbnails = this.videoRepository.GetUnmappedThumbnailFiles();

            IEnumerable<string> videos = this.videoRepository.GetUnmappedVideoFiles();

            AddVideoInfo addVideoInfo = new AddVideoInfo
                { AvailableCategories = categories, AvailableThumbnails = thumbnails, AvailableVideos = videos };

            return this.View(addVideoInfo);
        }
    }
}
