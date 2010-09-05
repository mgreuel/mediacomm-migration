﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Model.Photos;

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    /// Contains all information neede for rendering the whats new view.
    /// </summary>
    public class WhatsNewInfo
    {
        /// <summary>
        /// Gets or sets the 10 topics with the newest posts.
        /// </summary>
        /// <value>The 10 topics with the newest posts.</value>
        public IEnumerable<Topic> Topics { get; set; }

        /// <summary>
        /// Gets or sets the number of posts per topic.
        /// </summary>
        /// <value>The number of posts per topic.</value>
        public int PostsPerTopicPage { get; set; }

        /// <summary>
        /// Gets or sets the 4 newest albums.
        /// </summary>
        /// <value>The 4 newest albums.</value>
        public IEnumerable<PhotoAlbum> Albums { get; set; }
    }
}