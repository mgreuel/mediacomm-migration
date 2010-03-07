#region Using Directives

using System.ComponentModel.DataAnnotations;

#endregion

namespace MediaCommMVC.Core.Model.Movies
{
    /// <summary>Represents a movie quality.</summary>
    public class MovieQuality
    {
        #region Properties

        /// <summary>Gets or sets the ID.</summary>
        /// <value>The quality ID.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The quality name.</value>
        [StringLength(50)]
        public virtual string Name { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return this.Name ?? string.Empty;
        }

        #endregion
    }
}
