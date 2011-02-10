using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Core.Model.Videos
{
    /// <summary>Represents a category containing videos.</summary>
    public class VideoCategory
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The category name.</value>
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the video count.</summary>
        /// <value>The video count.</value>
        public virtual int VideoCount { get; protected set; }

        public virtual IEnumerable<Video> Videos { get; set; }
    }
}
