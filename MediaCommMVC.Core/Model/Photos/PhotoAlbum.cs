#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace MediaCommMVC.Core.Model.Photos
{
    /// <summary>Represents a photo album containing related photos.</summary>
    public class PhotoAlbum
    {
        #region Properties

        /// <summary>Gets or sets the cover photo.</summary>
        /// <value>The cover photo.</value>
        public virtual int CoverPhotoId { get; protected set; }

        /// <summary>Gets or sets the id.</summary>
        /// <value>The album id.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the date when the album was modifed.</summary>
        /// <value>The last modified date.</value>
        public virtual DateTime LastPicturesAdded { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The album name.</value>
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        public virtual PhotoCategory PhotoCategory { get; set; }

        /// <summary>Gets or sets the number of photos in the album.</summary>
        /// <value>The number of photos in the albu.</value>
        public virtual int PhotoCount { get; protected set; }

        /// <summary>Gets or sets the photos.</summary>
        /// <value>The photos.</value>
        public virtual IEnumerable<Photo> Photos { get; protected set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string categoryName = this.PhotoCategory == null ? string.Empty : this.PhotoCategory.Name;

            return string.Format("Id: '{0}, Title: '{1}', Category: '{2}'", this.Id, this.Name, categoryName);
        }

        #endregion
    }
}
