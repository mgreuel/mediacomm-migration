#region Using Directives

using System.ComponentModel.DataAnnotations;

#endregion

namespace MediaCommMVC.Web.Core.Model.Movies
{
    /// <summary>Represents a movie language.</summary>
    public class MovieLanguage
    {
        #region Properties

        /// <summary>Gets or sets the ID.</summary>
        /// <value>The language ID.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The language name.</value>
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
