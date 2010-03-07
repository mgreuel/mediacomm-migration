namespace MediaCommMVC.Core.Model.Photos
{
    /// <summary>Represents a category containing photo albums.</summary>
    public class PhotoCategory
    {
        #region Properties

        /// <summary>Gets or sets the number of albums.</summary>
        /// <value>The number of albums.</value>
        public virtual int AlbumCount { get; protected set; }

        /// <summary>Gets or sets the id.</summary>
        /// <value>The category id.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The category name.</value>
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the photo count.</summary>
        /// <value>The photo count.</value>
        public virtual int PhotoCount { get; protected set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Name: '{1}'", this.Id, this.Name);
        }

        #endregion
    }
}
