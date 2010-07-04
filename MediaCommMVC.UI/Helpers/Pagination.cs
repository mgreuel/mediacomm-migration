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
        /// Creates a pager.
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
            int totalPages = (int)Math.Ceiling(pagingParameters.TotalCount / (decimal)pagingParameters.PageSize);

            if (totalPages == 1)
            {
                return MvcHtmlString.Empty;
            }

            StringBuilder pagerBuilder = new StringBuilder(Resources.General.GoToPage);

            const string FormatNormal = "<span> <a href='{0}/{1}'>{2}</a></span>";
            const string FormatSelected = "<span class='selected'> {0}</span>";

            if (pagingParameters.CurrentPage > 1)
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

            if (pagingParameters.CurrentPage < totalPages)
            {
                pagerBuilder.AppendFormat(
                    FormatNormal, baseUrl, pagingParameters.CurrentPage + 1, Resources.General.Next);
            }

            return MvcHtmlString.Create(pagerBuilder.ToString());
        }

        #endregion
    }
}