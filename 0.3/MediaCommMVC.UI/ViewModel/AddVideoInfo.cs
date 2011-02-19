using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MediaCommMVC.Core.Model.Videos;

namespace MediaCommMVC.UI.ViewModel
{
    public class AddVideoInfo
    {
        public IEnumerable<string> AvailableVideos { get; set; }

        public IEnumerable<string> AvailableThumbnails { get; set; }

        public IEnumerable<VideoCategory> AvailableCategories { get; set; }
    }
}