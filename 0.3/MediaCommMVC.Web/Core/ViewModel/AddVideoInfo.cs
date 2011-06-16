using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Videos;

namespace MediaCommMVC.Web.Core.ViewModel
{
    public class AddVideoInfo
    {
        public IEnumerable<VideoCategory> AvailableCategories { get; set; }

        public IEnumerable<string> AvailablePosters { get; set; }

        public IEnumerable<string> AvailableThumbnails { get; set; }

        public IEnumerable<string> AvailableVideos { get; set; }
    }
}