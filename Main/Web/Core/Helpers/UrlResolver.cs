namespace MediaCommMVC.Core.Helpers
{
    #region Using Directives

    using System.Text.RegularExpressions;

    #endregion

    // credit goes to http://madskristensen.net/post/Resolve-and-shorten-URLs-in-Csharp.aspx
    public class UrlResolver
    {
        #region Constants and Fields

        private static readonly string link = "<a href=\"{0}{1}\">{2}</a>";

        private static readonly Regex regex = new Regex("((http://|https://|www\\.)([A-Z0-9.-:]{1,})\\.[0-9A-Z?;~&#=\\-_\\./]{2,})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Public Methods

        public static string ResolveLinks(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            foreach (Match match in regex.Matches(html))
            {
                string prefix = string.Empty;

                if (!match.Value.Contains("://"))
                {
                    prefix = "http://";
                }

                html = html.Replace(match.Value, string.Format(link, prefix, match.Value, ShortenUrl(match.Value, 50)));
            }

            return html;
        }

        #endregion

        #region Methods

        private static string ShortenUrl(string url, int max)
        {
            if (url.Length <= max)
            {
                return url;
            }

            // Remove the protocal
            int startIndex = url.IndexOf("://");
            if (startIndex > -1)
            {
                url = url.Substring(startIndex + 3);
            }

            if (url.Length <= max)
            {
                return url;
            }

            // Remove the folder structure
            int firstIndex = url.IndexOf("/") + 1;
            int lastIndex = url.LastIndexOf("/");
            if (firstIndex < lastIndex)
            {
                url = url.Replace(url.Substring(firstIndex, lastIndex - firstIndex), "...");
            }

            if (url.Length <= max)
            {
                return url;
            }

            // Remove URL parameters
            int queryIndex = url.IndexOf("?");
            if (queryIndex > -1)
            {
                url = url.Substring(0, queryIndex);
            }

            if (url.Length <= max)
            {
                return url;
            }

            // Remove URL fragment
            int fragmentIndex = url.IndexOf("#");
            if (fragmentIndex > -1)
            {
                url = url.Substring(0, fragmentIndex);
            }

            if (url.Length <= max)
            {
                return url;
            }

            // Shorten page
            firstIndex = url.LastIndexOf("/") + 1;
            lastIndex = url.LastIndexOf(".");
            if (lastIndex - firstIndex > 10)
            {
                string page = url.Substring(firstIndex, lastIndex - firstIndex);
                int length = url.Length - max + 3;
                url = url.Replace(page, "..." + page.Substring(length));
            }

            return url;
        }

        #endregion
    }
}