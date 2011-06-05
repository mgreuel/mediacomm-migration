using System.Text;

namespace MediaCommMVC.Web.Core.Common
{
    public static class UrlStripper
    {
        public static string RemoveIllegalCharactersFromUrl(string urlToEncode)
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
                if (c == ' ' || c == ',' /*|| c == '.'*/ || c == '/' || c == '\\' || c == '-')
                {
                    if (!prevdash)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == 'ä' || c == 'ö' || c == 'ü' || c == '.')
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
    }
}
