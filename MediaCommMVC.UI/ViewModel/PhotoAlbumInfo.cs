using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    /// Contains diplay information about a photo album.
    /// </summary>
    public class PhotoAlbumInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The album name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        /// <value>The album id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number of pictures in the album.
        /// </summary>
        /// <value>The number of pictures in the album..</value>
        public int PictureCount { get; set; }
    }
}