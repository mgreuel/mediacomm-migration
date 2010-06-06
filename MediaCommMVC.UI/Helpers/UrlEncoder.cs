using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MediaCommMVC.UI.Helpers
{
    /// <summary>
    /// Encodes urls.
    /// </summary>
    public static class UrlEncoder
    {
        /// <summary>
        /// Encodes an url to an friendly Url.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="urlToEncode">The URL to encode.</param>
        /// <returns>The friendly Url.</returns>
        public static string ToFriendlyUrl(this UrlHelper helper, string urlToEncode)
        {
            if (string.IsNullOrEmpty(urlToEncode))
            {
                return string.Empty;
            }

            return urlToEncode.Replace(" ", "_").Replace("&", Resources.General.And);
        }
    }
}