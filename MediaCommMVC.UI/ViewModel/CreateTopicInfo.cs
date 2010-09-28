using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.UI.ViewModel
{
    using MediaCommMVC.Core.Model.Forums;

    /// <summary>
    /// The View Data for creating a new topic.
    /// </summary>
    public class CreateTopicInfo
    {
        /// <summary>
        /// Gets or sets the forum.
        /// </summary>
        /// <value>The forum.</value>
        public Forum Forum { get; set; }

        /// <summary>
        /// Gets or sets the names of all users.
        /// </summary>
        /// <value>The list of all user names.</value>
        public IEnumerable<string> UserNames { get; set; }


    }
}