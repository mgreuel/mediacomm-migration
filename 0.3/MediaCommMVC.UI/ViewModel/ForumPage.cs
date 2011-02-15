#region Using Directives

using System.Collections.Generic;

using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Parameters;

#endregion

namespace MediaCommMVC.UI.ViewModel
{
    /// <summary>
    ///   Display information for a forum page.
    /// </summary>
    public class ForumPage
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the forum.
        /// </summary>
        /// <value>The forum.</value>
        public Forum Forum { get; set; }

        /// <summary>
        ///   Gets or sets the paging parameters.
        /// </summary>
        /// <value>The paging parameters.</value>
        public PagingParameters PagingParameters { get; set; }

        /// <summary>
        ///   Gets or sets the number of posts per topic.
        /// </summary>
        /// <value>The number of posts per topic.</value>
        public int PostsPerTopicPage { get; set; }

        /// <summary>
        ///   Gets or sets the topics of the page.
        /// </summary>
        /// <value>The topics on the page.</value>
        public IEnumerable<Topic> Topics { get; set; }

        #endregion
    }
}