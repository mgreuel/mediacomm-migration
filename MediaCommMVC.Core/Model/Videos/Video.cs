using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MediaCommMVC.Core.Model.Users;

namespace MediaCommMVC.Core.Model.Videos
{
    /// <summary>
    /// A video uploaded by an user.
    /// </summary>
    public class Video
    {
        /// <summary>
        /// Gets or sets the video id.
        /// </summary>
        /// <value>The video id.</value>
        public virtual int Id { get; set; }

        /// <summary>Gets or sets the filename.</summary>
        /// <value>The Photo's filename.</value>
        public virtual string FileName { get; set; }

        /// <summary>Gets or sets the size of the file.</summary>
        /// <value>The size of the file.</value>
        public virtual long FileSize { get; set; }

        /// <summary>Gets or sets the uploader.</summary>
        /// <value>The uploader.</value>
        public virtual MediaCommUser Uploader { get; set; }

        public virtual string Title { get; set; }
    }
}
