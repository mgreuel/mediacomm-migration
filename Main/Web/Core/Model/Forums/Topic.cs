﻿namespace MediaCommMVC.Core.Model.Forums
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class Topic
    {
        #region Constructors and Destructors

        public Topic()
        {
            this.HasUnreadPosts = false;
        }

        #endregion

        #region Properties

        public virtual DateTime Created { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual TopicDisplayPriority DisplayPriority { get; set; }

        public virtual IEnumerable<MediaCommUser> ExcludedUsers { get; set; }

        public virtual Forum Forum { get; set; }

        public virtual bool HasUnreadPosts { get; set; }

        public virtual int Id { get; set; }

        public virtual string LastPostAuthor { get; set; }

        public virtual DateTime LastPostTime { get; set; }

        public virtual Poll Poll { get; set; }

        public virtual int PostCount { get; set; }

        public virtual string Title { get; set; }

        #endregion
    }
}