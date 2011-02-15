#region Using Directives

using System.Text;
using System.Text.RegularExpressions;
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
        /// <see cref="http://stackoverflow.com/questions/25259/how-do-you-include-a-webpage-title-as-part-of-a-webpage-url/25486#25486"/>
        /// <param name="helper">The helper.</param>
        /// <param name="urlToEncode">The URL to encode.</param>
        /// <returns>The friendly Url.</returns>
        public static string ToFriendlyUrl(this UrlHelper helper, string urlToEncode)
        {
            if (string.IsNullOrEmpty(urlToEncode))
            {
                return string.Empty;
            }

            urlToEncode = urlToEncode.ToLower().Trim();

            StringBuilder sb = new StringBuilder(urlToEncode.Length);
            bool prevdash = false;
            char c;

            for (int i = 0; i < urlToEncode.Length; i++)
            {
                c = urlToEncode[i];
                if (c == ' ' || c == ',' || c == '.' || c == '/' || c == '\\' || c == '-')
                {
                    if (!prevdash)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == 'ä' || c == 'ö' || c == 'ü')
                {
                    sb.Append(c);
                    prevdash = false;
                }

                if (i == 80)
                {
                    break;
                }
            }

            urlToEncode = sb.ToString();

            if (urlToEncode.EndsWith("-"))
            {
                urlToEncode = urlToEncode.Substring(0, urlToEncode.Length - 1);
            }

            return urlToEncode;
        }

        #endregion
    }
}