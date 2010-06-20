using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMVC.Core.Model.Forums;

namespace MediaCommMVC.UI.Helpers
{
    /// <summary>
    ///     Determines icon filenames.
    /// </summary>
    public static class IconHelper
    {
        /// <summary>
        /// Gets the filename for a topic icon.
        /// </summary>
        /// <param name="helper">The url helper.</param>
        /// <param name="topic">The topic.</param>
        /// <returns>The filename of the topic icon.</returns>
        public static string TopicIcon(this UrlHelper helper, Topic topic)
        {
            string filename = "folder";

            if (topic.DisplayPriority != 0)
            {
                filename = filename + "_sticky";
            }

            if (!topic.ReadByCurrentUser)
            {
                filename = filename + "_new";
            }

            return filename + ".gif";
        }

        /// <summary>
        /// Gets the filename for a topic icon.
        /// </summary>
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
    }
}