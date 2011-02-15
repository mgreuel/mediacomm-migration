using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Core.Model.Videos;
using MediaCommMVC.UI.Helpers;

namespace MediaCommMVC.UI.Controllers
{
    public class VideosController : Controller
    {
        private IVideoRepository videoRepository;

        public VideosController(IVideoRepository videoRepository)
        {
            this.videoRepository = videoRepository;
        }

        public ActionResult Category(int id)
        {
            VideoCategory category = this.videoRepository.GetCategoryById(id);

            return this.View(category);
        }

        /// <summary>Gets all photo categories.</summary>
        /// <returns>The photo categories as Json string.</returns>
        [HttpGet]
        [Authorize]
        [OutputCache(Duration = 3600, VaryByParam = "")]
        public ActionResult GetCategories()
        {
            IEnumerable<VideoCategory> categories = this.videoRepository.GetAllCategories();

            var categoryViewModels = categories.Select(c => new { c.Name, c.Id, c.VideoCount, EncodedName = this.Url.ToFriendlyUrl(c.Name) });

            return this.Json(categoryViewModels, JsonRequestBehavior.AllowGet);
        }
    }
}
