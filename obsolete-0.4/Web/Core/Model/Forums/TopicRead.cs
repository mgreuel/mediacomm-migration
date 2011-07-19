namespace MediaCommMVC.Core.Model.Forums
{
    #region Using Directives

    using System;

    #endregion

    public class TopicRead
    {
        #region Properties

        public virtual int Id { get; protected set; }

        public virtual DateTime LastVisit { get; set; }

        public virtual MediaCommUser ReadByUser { get; set; }

        public virtual Topic ReadTopic { get; set; }

        #endregion
    }
}