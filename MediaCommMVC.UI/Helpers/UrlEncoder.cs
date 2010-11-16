#region Using Directives

using System.Web.Mvc;

using Resources;

#endregion

namespace MediaCommMVC.UI.Helpers
{
    /// <summary>Encodes urls.</summary>
    public static class UrlEncoder
    {
        #region Public Methods

        /// <summary>Encodes an url to an friendly Url.</summary>
        /// <param name="helper">The helper.</param>
        /// <param name="urlToEncode">The URL to encode.</param>
        /// <returns>The friendly Url.</returns>
        public static string ToFriendlyUrl(this UrlHelper helper, string urlToEncode)
        {
            return string.IsNullOrEmpty(urlToEncode) ? string.Empty : urlToEncode.Replace(" ", "_").Replace("&", General.And);
        }

        #endregion
    }
}