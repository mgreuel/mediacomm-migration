#region Using Directives

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace MediaCommMVC.Core.Model.Forums
{
    using System.Collections.Generic;

    using MediaCommMVC.Core.Model.Users;

    /// <summary>Represents a topic in a forum.</summary>
    public class Topic
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Topic" /> class.
        /// </summary>
        public Topic()
        {
            this.ReadByCurrentUser = true;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets the date and time the thread was created.
        /// </summary>
        /// <value>The date and time the thread was created.</value>
        public virtual DateTime Created { get; set; }

        /// <summary>
        ///   Gets or sets the author of the topic.
        /// </summary>
        /// <value>The author of the topic.</value>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        ///   Gets or sets the topic's display priority within it's forum.
        /// </summary>
        /// <value>The topic's display priority within it's forum.</value>
        public virtual TopicDisplayPriority DisplayPriority { get; set; }

        /// <summary>
        ///   Gets or sets the forum the post belongs to.
        /// </summary>
        /// <value>The forum the post belongs to.</value>
        public virtual Forum Forum { get; set; }

        /// <summary>
        ///   Gets or sets the id.
        /// </summary>
        /// <value>The topic's id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        ///   Gets or sets the name of the last post's author.
        /// </summary>
        /// <value>The name of the last post's author.</value>
        public virtual string LastPostAuthor { get; set; }

        /// <summary>
        ///   Gets or sets the time of the last post.
        /// </summary>
        /// <value>The time of the last post.</value>
        public virtual DateTime LastPostTime { get; set; }

        /// <summary>
        ///   Gets or sets the post count.
        /// </summary>
        /// <value>The post count.</value>
        public virtual int PostCount { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether the thread was already [read by the current user].
        /// </summary>
        /// <value><c>true</c> if the thread was[read by current user]; otherwise, <c>false</c>.</value>
        public virtual bool ReadByCurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the list of users beeing excluded from this topic.
        /// </summary>
        /// <value>The users exluded from this topic.</value>
        public virtual IEnumerable<MediaCommUser> ExcludedUsers { get; set; }

            /// <summary>
        ///   Gets or sets the topic's title.
        /// </summary>
        /// <value>The topic's title.</value>
        [Required]
        [StringLength(255)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the poll.
        /// </summary>
        /// <value>The poll belonging to this topic.</value>
        public virtual Poll Poll { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Determines whether the specified <see cref="System.Object"/> is equal to this instance.</summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            Topic topic = obj as Topic;

            return topic != null && topic.Id == this.Id;
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Title: '{1}", this.Id, this.Title);
        }

        #endregion
    }
}
