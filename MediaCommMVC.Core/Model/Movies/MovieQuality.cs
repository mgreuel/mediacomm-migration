#region Using Directives

using System;
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

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            MovieQuality quality = obj as MovieQuality;

            return quality != null && quality.Name.Equals(this.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Name == null ? base.GetHashCode() : this.Name.GetHashCode();
        }

        #endregion
    }
}
