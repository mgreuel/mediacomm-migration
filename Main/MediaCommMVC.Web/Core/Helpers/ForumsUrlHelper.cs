using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Helpers
{
    public static class ForumsUrlHelper
    {
        public static string GetPostUrl(this UrlHelper helper, Post post, int page)
        {
            string postAnker = String.Format("#{0}", post.Id);

            return helper.RouteUrl("ViewTopic", new { id = post.Topic.Id, page, name = helper.ToFriendlyUrl(post.Topic.Title) }) + postAnker;
        }
    }
}