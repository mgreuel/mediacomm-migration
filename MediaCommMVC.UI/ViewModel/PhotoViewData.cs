using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    /// Contains information needed to display photo pages.
    /// </summary>
    public class PhotoViewData
    {
        /// <summary>
        /// Gets or sets the PhotoAlbums which can be viewed.
        /// </summary>
        /// <value>The list of photo albums.</value>
        public IEnumerable<PhotoAlbumInfo> PhotoAlbums { get; set; }
    }
}