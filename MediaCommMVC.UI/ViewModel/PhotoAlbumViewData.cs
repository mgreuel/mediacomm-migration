using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaCommMVC.Core.Model.Photos;

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    /// Contains the information needed to display a photo album.
    /// </summary>
    public class PhotoAlbumViewData : PhotoViewData
    {
        /// <summary>
        /// Gets or sets the photo album.
        /// </summary>
        /// <value>The photo album.</value>
        public PhotoAlbum Album { get; set; }
    }
}