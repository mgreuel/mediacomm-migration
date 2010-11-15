#region Using Directives

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace MediaCommMVC.Core.Model.Movies
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

        /// <summary>Determines whether the specified <see cref="System.Object"/> is equal to this instance.</summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            MovieLanguage language = obj as MovieLanguage;

            return language != null && language.Name.Equals(this.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return this.Name ?? string.Empty;
        }

        #endregion
    }
}
