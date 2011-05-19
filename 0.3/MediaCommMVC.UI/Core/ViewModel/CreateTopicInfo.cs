#region Using Directives

using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Web.Core.ViewModel
{
    /// <summary>
    ///   The View Data for creating a new topic.
    /// </summary>
    public class CreateTopicInfo
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the forum.
        /// </summary>
        /// <value>The forum.</value>
        public Forum Forum { get; set; }

        /// <summary>
        ///   Gets or sets the names of all users.
        /// </summary>
        /// <value>The list of all user names.</value>
        public IEnumerable<string> UserNames { get; set; }

        #endregion
    }
}