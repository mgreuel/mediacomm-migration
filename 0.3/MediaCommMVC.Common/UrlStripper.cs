﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Common
{
    /// <summary>
    /// Provides helper functions for working with Urls.
    /// </summary>
    public static class UrlStripper
    {
        /// <summary>
        /// Removes the illegal characters from the provided Url.
        /// </summary>
        /// <param name="urlToEncode">The URL to encode.</param>
        /// <returns>A string only containing valid url chracters.</returns>
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
