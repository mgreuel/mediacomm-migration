#region Using Directives

using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using MediaCommMVC.Common;

using Resources;

#endregion

namespace MediaCommMVC.UI.Helpers
{
    /// <summary>Encodes urls.</summary>
    public static class UrlEncoder
    {
        #region Public Methods

        /// <summary>Encodes an url to an friendly Url.</summary>
        /// <see cref="http://stackoverflow.com/questions/25259/how-do-you-include-a-webpage-title-as-part-of-a-webpage-url/25486#25486"/>
        /// <param name="helper">The helper.</param>
        /// <param name="urlToEncode">The URL to encode.</param>
        /// <returns>The friendly Url.</returns>
        public static string ToFriendlyUrl(this UrlHelper helper, string urlToEncode)
        {
            return UrlStripper.RemoveIllegalCharactersFromUrl(urlToEncode);
        }



        #endregion
    }
}