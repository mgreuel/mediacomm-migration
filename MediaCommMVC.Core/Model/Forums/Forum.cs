#region Using Directives

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>Represents a forum within the forums.</summary>
    public class Forum
    {
        #region Properties

        /// <summary>Gets or sets the forum's description.</summary>
        /// <value>The forum's description.</value>
        [StringLength(100)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the display order index with determines where the forum is displayed wihtin the forums.
        ///   The lower the index is, the higher the forum is displayed.</summary>
        /// <value>The display order index of the forum.</value>
        public virtual int DisplayOrderIndex { get; set; }

        /// <summary>Gets or sets a value indicating whether this forum has unread posts.</summary>
        /// <value><c>true</c> if this forum has unread posts; otherwise, <c>false</c>.</value>
        public virtual bool HasUnreadTopics { get; set; }

        /// <summary>Gets or sets the forum's id.
        ///   Only the ORM should set the id.</summary>
        /// <value>The forum's id.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the author of the last post.</summary>
        /// <value>The author of the last post.</value>
        public virtual string LastPostAuthor { get; set; }

        /// <summary>Gets or sets the time of the last post.</summary>
        /// <value>The last time of the last post.</value>
        public virtual DateTime? LastPostTime { get; set; }

        /// <summary>Gets or sets the post count.</summary>
        /// <value>The post count.</value>
        public virtual int PostCount { get; protected set; }

        /// <summary>Gets or sets the forum's title.</summary>
        /// <value>The forum's title.</value>
        [Required]
        [StringLength(50)]
        public virtual string Title { get; set; }

        /// <summary>Gets or sets the topic count.</summary>
        /// <value>The topic count.</value>
        public virtual int TopicCount { get; protected set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("ID: '{0}', Title: '{1}'", this.Id, this.Title);
        }

        #endregion
    }
}
