#region Using Directives

using System;

using MediaCommMVC.Core.Model.Users;

#endregion

namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>Represents a forum post.</summary>
    public class Post
    {
        #region Properties

        /// <summary>Gets or sets the author of the post.</summary>
        /// <value>The author of the post.</value>
        public virtual MediaCommUser Author { get; set; }

        /// <summary>Gets or sets the date and time the post was created.</summary>
        /// <value>The date and time the post was created.</value>
        public virtual DateTime Created { get; set; }

        /// <summary>Gets or sets the post id.
        /// The id should only be set by the ORM.</summary>
        /// <value>The post id.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the post text.</summary>
        /// <value>The post text.</value>
        public virtual string Text { get; set; }

        /// <summary>Gets or sets the topic the post belongs to.</summary>
        /// <value>The topic the post belongs to.</value>
        public virtual Topic Topic { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string textStart = this.Text.Length > 20 ? this.Text.Substring(0, 20) : this.Text;

            return string.Format(
                "ID: '{0}', Author: '{1}, Text: '{2}'", this.Id, this.Author, textStart);
        }

        #endregion
    }
}
