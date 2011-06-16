using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Photos;

namespace MediaCommMVC.Web.Core.ViewModel
{
    public class PhotoUpload
    {
        public PhotoAlbum Album { get; set; }

        public IEnumerable<PhotoCategory> Categories { get; set; }

        public PhotoCategory Category { get; set; }
    }
}