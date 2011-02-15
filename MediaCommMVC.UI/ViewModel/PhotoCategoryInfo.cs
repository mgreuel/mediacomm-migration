using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    /// Contains diplay information about a photo category.
    /// </summary>
    public class PhotoCategoryInfo
    {
        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        /// <value>The category id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        /// <value>The category name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        /// <value>The albums.</value>
        public IEnumerable<PhotoAlbumInfo> Albums { get; set; }

        /// <summary>
        /// Gets or sets the album count.
        /// </summary>
        /// <value>The album count.</value>
        public int AlbumCount { get; set; }
    }
}
