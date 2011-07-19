using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.Model.Videos
{
    public class Video
    {
        public virtual string Description { get; set; }

        public virtual int Id { get; set; }

        public virtual string PosterFileName { get; set; }

        public virtual string ThumbnailFileName { get; set; }

        public virtual string Title { get; set; }

        public virtual MediaCommUser Uploader { get; set; }

        public virtual VideoCategory VideoCategory { get; set; }

        public virtual string VideoFileName { get; set; }
    }
}