#region Using Directives

using System.Collections.Generic;

#endregion

namespace MediaCommMVC.Core.Model.Photos
{
    /// <summary>
    /// Represents a category containing photo albums.
    /// </summary>
    public class PhotoCategory
    {
        #region Constants and Fields

        /// <summary>
        /// The albums in this category.
        /// </summary>
        private IEnumerable<PhotoAlbum> albums = new List<PhotoAlbum>();

        #endregion

        #region Properties

        /// <summary>Gets or sets the number of albums.</summary>
        /// <value>The number of albums.</value>
        public virtual int AlbumCount { get; protected set; }

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        /// <value>The albums.</value>
        public virtual IEnumerable<PhotoAlbum> Albums
        {
            get
            {
                return this.albums;
            }

            protected set
            {
                this.albums = value;
            }
        }

        /// <summary>Gets or sets the id.</summary>
        /// <value>The category id.</value>
        public virtual int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The category name.</value>
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the photo count.</summary>
        /// <value>The photo count.</value>
        public virtual int PhotoCount { get; protected set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            PhotoCategory photoCategory = obj as PhotoCategory;

            return photoCategory != null && photoCategory.Id == this.Id;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Name: '{1}'", this.Id, this.Name);
        }

        #endregion
    }
}
