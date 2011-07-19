using System.Collections.Generic;

namespace MediaCommMVC.Web.Core.Model.Videos
{
    public class VideoCategory
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int VideoCount { get; protected set; }

        public virtual IEnumerable<Video> Videos { get; set; }
    }
}