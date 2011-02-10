using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Core.Model.Videos;
using MediaCommMVC.UI.Helpers;

namespace MediaCommMVC.UI.Controllers
{
    public class VideosController : Controller
    {
        public VideosController(/*IVideoRepository videoRepository*/)
        {
            //this.videoRepository = videoRepository;
        }

        public ActionResult Category(int id)
        {
            VideoCategory category = new VideoCategory
                {
                    Name = "category one",
                    Videos =
                        new List<Video>
                            {
                                new Video
                                    {
                                        Id = 0,
                                        FileName = "TestVideo.webm",
                                        FileSize = 12345,
                                        Title = "my nice Video no 1",
                                        Uploader = new MediaCommUser("video uploader", "wer@aef.sw", "321")
                                    }
                            }
                };
            
            //this.videosRepository.GetCategoryById(id);

            return this.View(category);
        }

        /// <summary>Gets all photo categories.</summary>
        /// <returns>The photo categories as Json string.</returns>
        [HttpGet]
        [Authorize]
        [OutputCache(Duration = 3600, VaryByParam = "")]
        public ActionResult GetCategories()
        {
            //IEnumerable<VideoCategory> categories = this.videosRepository.GetAllCategories();

            List<VideoCategory> categories = new List<VideoCategory>
                { new VideoCategory {Id = 0, Name = "category one" }, new VideoCategory { Id = 1, Name = "category 2" } };

            var categoryViewModels = categories.Select(c => new { c.Name, c.Id, c.VideoCount, EncodedName = this.Url.ToFriendlyUrl(c.Name) });

            return this.Json(categoryViewModels, JsonRequestBehavior.AllowGet);
        }
    }
}
