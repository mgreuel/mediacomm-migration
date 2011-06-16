using System.Web.Mvc;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Helpers
{
    public static class IconHelper
    {
        public static string ForumIcon(this UrlHelper helper, Forum forum)
        {
            string filename = "folder";

            if (forum.HasUnreadTopics)
            {
                filename = filename + "_new";
            }

            return filename + ".gif";
        }

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
    }
}