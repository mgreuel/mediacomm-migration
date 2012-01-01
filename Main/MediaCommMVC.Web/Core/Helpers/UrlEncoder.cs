using System.Web.Mvc;

using MediaCommMVC.Web.Core.Common;
using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Helpers
{
    public static class UrlEncoder
    {
        /// <see cref="http://stackoverflow.com/questions/25259/how-do-you-include-a-webpage-title-as-part-of-a-webpage-url/25486#25486"/>
        public static string ToFriendlyUrl(this UrlHelper helper, string urlToEncode)
        {
            return UrlStripper.RemoveIllegalCharactersFromUrl(urlToEncode);
        }
    }
}