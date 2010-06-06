using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MediaCommMVC.Core.Model.Forums;

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    /// Display information for a topic page.
    /// </summary>
    public class TopicPage
    {
        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        /// <value>The topic.</value>
        public Topic Topic { get; set; }

        /// <summary>
        /// Gets or sets the posts on the page.
        /// </summary>
        /// <value>The posts on the page.</value>
        public IEnumerable<Post> Posts { get; set; }
    }
}