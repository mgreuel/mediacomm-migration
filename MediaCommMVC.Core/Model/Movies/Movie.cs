#region Using Directives

using System.ComponentModel.DataAnnotations;

using MediaCommMVC.Core.Model.Users;

#endregion

namespace MediaCommMVC.Core.Model.Movies
{
    /// <summary>
    ///   Represent a movie.
    /// </summary>
    public class Movie
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the ID.
        /// </summary>
        /// <value>The movie ID.</value>
        public virtual int Id { get; protected set; }

        /// <summary>
        ///   Gets or sets the info link.
        /// </summary>
        /// <value>The info link.</value>
        public virtual string InfoLink { get; set; }

        /// <summary>
        ///   Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [Required]
        public virtual MovieLanguage Language { get; set; }

        /// <summary>
        ///   Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public virtual MediaCommUser Owner { get; set; }

        /// <summary>
        ///   Gets or sets the quality.
        /// </summary>
        /// <value>The quality.</value>
        [Required]
        public virtual MovieQuality Quality { get; set; }

        /// <summary>
        ///   Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        [StringLength(255)]
        public virtual string Title { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Returns a <see cref = "System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref = "System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Title: '{1}, Owner: '{2}", this.Id, this.Title, this.Owner);
        }

        #endregion
    }
}
