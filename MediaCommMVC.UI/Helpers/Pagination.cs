#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using MediaCommMVC.Core.Parameters;

#endregion

namespace MediaCommMVC.UI.Helpers
{
    /// <summary>
    /// Html Helpers for pagination.
    /// </summary>
    public static class Pagination
    {
        #region Public Methods

        /// <summary>
        /// Creates a pager with text and page numbers.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns>The paging html code.</returns>
        public static MvcHtmlString Pager(
            this HtmlHelper helper,
            PagingParameters pagingParameters,
            string baseUrl)
        {
            return BuildPager(pagingParameters, baseUrl, false);
        }

        /// <summary>
        /// Creates a pager displaying only the page numbers.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns>The paging html code.</returns>
        public static MvcHtmlString NumbersOnlyPager(
            this HtmlHelper helper,
            PagingParameters pagingParameters,
            string baseUrl)
        {
            return BuildPager(pagingParameters, baseUrl, true);
        }

        /// <summary>
        /// Builds the pager html.
        /// </summary>
        /// <param name="pagingParameters">The paging parameters.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="numbersOnly">if set to <c>true</c> [only the numbers are created].</param>
        /// <returns>The pager html code.</returns>
        private static MvcHtmlString BuildPager(PagingParameters pagingParameters, string baseUrl, bool numbersOnly)
        {
            int totalPages = pagingParameters.NumberOfPages;

            if (totalPages <= 1)
            {
                return MvcHtmlString.Empty;
            }

            StringBuilder pagerBuilder = new StringBuilder("[ "  + Resources.General.GoToPage);

            const string FormatNormal = "<span> <a href='{0}/{1}'>{2}</a></span>";
            const string FormatSelected = "<span class='selected'> {0}</span>";

            if (!numbersOnly && pagingParameters.CurrentPage > 1)
            {
                pagerBuilder.AppendFormat(
                    FormatNormal, baseUrl, pagingParameters.CurrentPage - 1, Resources.General.Previous);
            }

            for (int i = 1; i <= totalPages; i++)
            {
                if (i == pagingParameters.CurrentPage)
                {
                    pagerBuilder.AppendFormat(FormatSelected, i);
                }
                else
                {
                    pagerBuilder.AppendFormat(FormatNormal, baseUrl, i, i);
                }
            }

            if (!numbersOnly && pagingParameters.CurrentPage < totalPages)
            {
                pagerBuilder.AppendFormat(
                    FormatNormal, baseUrl, pagingParameters.CurrentPage + 1, Resources.General.Next);
            }

            return MvcHtmlString.Create(pagerBuilder + " ]");
        }

        #endregion
    }
}