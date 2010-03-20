#region Using Directives

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>Represents a topic in a forum.</summary>
    public class Topic
    {
        #region Properties

        /// <summary>Gets or sets the date and time the thread was created.</summary>
        /// <value>The date and time the thread was created.</value>
        public virtual DateTime Created { get; set; }

        /// <summary>Gets or sets the author of the topic.</summary>
        /// <value>The author of the topic.</value>
        public virtual string CreatedBy { get; set; }

        /// <summary>Gets or sets the topic's display priority within it's forum.</summary>
        /// <value>The topic's display priority within it's forum.</value>
        public virtual int DisplayPriority { get; set; }

        /// <summary>Gets or sets the forum the post belongs to.</summary>
        /// <value>The forum the post belongs to.</value>
        public virtual Forum Forum { get; set; }

        /// <summary>Gets or sets the id.
        /// Only the ORM should set the id.</summary>
        /// <value>The topic's id.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the name of the last post's author.</summary>
        /// <value>The name of the last post's author.</value>
        public virtual string LastPostAuthor { get; set; }

        /// <summary>Gets or sets the time of the last post.</summary>
        /// <value>The time of the last post.</value>
        public virtual DateTime LastPostTime { get; set; }

        /// <summary>Gets or sets the post count.</summary>
        /// <value>The post count.</value>
        public virtual int PostCount { get; protected set; }

        /// <summary>Gets or sets the topic's title.</summary>
        /// <value>The topic's title.</value>
        [Required]
        [StringLength(255)]
        public virtual string Title { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Title: '{1}", this.Id, this.Title);
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
            Topic topic = obj as Topic;

            return topic != null && topic.Id == this.Id;
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
