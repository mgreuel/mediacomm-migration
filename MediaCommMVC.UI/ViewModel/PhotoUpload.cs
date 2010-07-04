using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaCommMVC.Core.Model.Photos;

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    /// ViewModel for uploading photos.
    /// </summary>
    public class PhotoUpload
    {
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public PhotoCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public PhotoAlbum Album { get; set; }

        /// <summary>
        /// Gets or sets all available categories.
        /// </summary>
        /// <value>The photo categories.</value>
        public IEnumerable<PhotoCategory> Categories { get; set; }
    }
}