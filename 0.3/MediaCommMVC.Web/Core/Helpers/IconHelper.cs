#region Using Directives

using System.Web.Mvc;

using MediaCommMVC.Web.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Web.Core.Helpers
{
    /// <summary>Determines icon filenames.</summary>
    public static class IconHelper
    {
        #region Public Methods

        /// <summary>Gets the filename for a topic icon.</summary>
        /// <param name="helper">The url helper.</param>
        /// <param name="forum">The topic.</param>
        /// <returns>The filename of the topic icon.</returns>
        public static string ForumIcon(this UrlHelper helper, Forum forum)
        {
            string filename = "folder";

            if (forum.HasUnreadTopics)
            {
                filename = filename + "_new";
            }

            return filename + ".gif";
        }

        /// <summary>Gets the filename for a topic icon.</summary>
        /// <param name="helper">The url helper.</param>
        /// <param name="topic">The topic.</param>
        /// <returns>The filename of the topic icon.</returns>
        public static string TopicIcon(this UrlHelper helper, Topic topic)
        {
            string filename = "folder";

            if (topic.DisplayPriority == TopicDisplayPriority.Sticky)
            {
                filename = filename + "_sticky";
            }

            if (!topic.ReadByCurrentUser)
            {
                filename = filename + "_new";
            }

            return filename + ".gif";
        }

        #endregion
    }
}