﻿#region Using Directives

using MediaCommMVC.Core.Model.Users;

#endregion

namespace MediaCommMVC.Core.Model.Photos
{
    /// <summary>Represents a Photo.</summary>
    public class Photo
    {
        #region Properties

        /// <summary>Gets or sets the filename.</summary>
        /// <value>The Photo's filename.</value>
        public virtual string FileName { get; set; }

        /// <summary>Gets or sets the size of the file.</summary>
        /// <value>The size of the file.</value>
        public virtual long FileSize { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        public virtual int Height { get; set; }

        /// <summary>Gets or sets the id.</summary>
        /// <value>The Photo id.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the album the photo belongs to.</summary>
        /// <value>The album.</value>
        public virtual PhotoAlbum PhotoAlbum { get; set; }

        /// <summary>Gets or sets the uploader.</summary>
        /// <value>The uploader.</value>
        public virtual MediaCommUser Uploader { get; set; }

        /// <summary>Gets or sets the view count.</summary>
        /// <value>The view count.</value>
        public virtual int ViewCount { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        public virtual int Width { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Filename: '{1}', Album: '{2}'", this.Id, this.FileName, this.PhotoAlbum);
        }

        #endregion
    }
}
