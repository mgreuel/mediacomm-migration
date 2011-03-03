#region Using Directives

using MediaCommMVC.Core.Model.Users;

#endregion

namespace MediaCommMVC.Core.Model.Videos
{
    /// <summary>A video uploaded by an user.</summary>
    public class Video
    {
        #region Properties

        /// <summary>Gets or sets the description of the video.</summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the video id.</summary>
        /// <value>The video id.</value>
        public virtual int Id { get; set; }

        /// <summary>Gets or sets the filename of the poster belonging to the video.</summary>
        /// <value>The poster filename.</value>
        public virtual string PosterFileName { get; set; }

        /// <summary>Gets or sets the thumbnail filename.</summary>
        /// <value>The videos's filename.</value>
        public virtual string ThumbnailFileName { get; set; }

        /// <summary>Gets or sets the title of the video.</summary>
        /// <value>The title.</value>
        public virtual string Title { get; set; }

        /// <summary>Gets or sets the uploader.</summary>
        /// <value>The uploader.</value>
        public virtual MediaCommUser Uploader { get; set; }

        /// <summary>Gets or sets the category the video belongs to.</summary>
        /// <value>The video category.</value>
        public virtual VideoCategory VideoCategory { get; set; }

        /// <summary>Gets or sets the video filename.</summary>
        /// <value>The videos's filename.</value>
        public virtual string VideoFileName { get; set; }

        #endregion
    }
}
