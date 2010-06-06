using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MediaCommMVC.Core.Model.Forums;

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    /// Display information for a forum page.
    /// </summary>
    public class ForumPage
    {
        /// <summary>
        /// Gets or sets the forum.
        /// </summary>
        /// <value>The forum.</value>
        public Forum Forum { get; set; }

        /// <summary>
        /// Gets or sets the topics of the page.
        /// </summary>
        /// <value>The topics on the page.</value>
        public IEnumerable<Topic> Topics { get; set; }
    }
}