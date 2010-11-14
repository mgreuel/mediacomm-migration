#region Using Directives

using System.Collections.Generic;

using MediaCommMVC.Core.Model.Photos;

#endregion

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    ///   ViewModel for uploading photos.
    /// </summary>
    public class PhotoUpload
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the album.
        /// </summary>
        /// <value>The album.</value>
        public PhotoAlbum Album { get; set; }

        /// <summary>
        ///   Gets or sets all available categories.
        /// </summary>
        /// <value>The photo categories.</value>
        public IEnumerable<PhotoCategory> Categories { get; set; }

        /// <summary>
        ///   Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public PhotoCategory Category { get; set; }

        #endregion
    }
}