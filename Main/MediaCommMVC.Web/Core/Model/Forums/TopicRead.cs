using System;

using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.Model.Forums
{
    public class TopicRead
    {
        public virtual int Id { get; protected set; }

        public virtual DateTime LastVisit { get; set; }

        public virtual MediaCommUser ReadByUser { get; set; }

        public virtual Topic ReadTopic { get; set; }
    }
}