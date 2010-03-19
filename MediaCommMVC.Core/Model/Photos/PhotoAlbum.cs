#region Using Directives

using System.Collections.Generic;

#endregion

namespace MediaCommMVC.Core.Model.Photos
{
    /// <summary>Represents a photo album containing related photos.</summary>
    public class PhotoAlbum
    {
        #region Properties

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        public virtual PhotoCategory Category { get; set; }

        /// <summary>Gets or sets the id.</summary>
        /// <value>The album id.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The album name.</value>
        public virtual string Name { get; set; }

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
            return string.Format("Id: '{0}, Title: '{1}', Category: '{2}'", this.Id, this.Name, this.Category.Name);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            PhotoAlbum photoAlbum = obj as PhotoAlbum;

            return photoAlbum != null && photoAlbum.Id == this.Id;
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

        #endregion
    }
}
