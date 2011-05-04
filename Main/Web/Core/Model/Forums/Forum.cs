namespace MediaCommMVC.Core.Model.Forums
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class Forum
    {
        #region Properties

        public virtual string Description { get; set; }

        public virtual int DisplayOrderIndex { get; set; }

        public virtual bool HasUnreadTopics { get; set; }

        public virtual int Id { get; protected set; }

        public virtual string LastPostAuthor { get; set; }

        public virtual DateTime? LastPostTime { get; set; }

        public virtual int PostCount { get; protected set; }

        public virtual string Title { get; set; }

        public virtual int TopicCount { get; protected set; }

        public virtual IEnumerable<Topic> Topics { get; set; }

        #endregion
    }
}