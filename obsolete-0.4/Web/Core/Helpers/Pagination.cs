namespace MediaCommMVC.Core.Helpers
{
    #region Using Directives

    using System.Text;
    using System.Web.Mvc;

    using Resources;

    #endregion

    public static class Pagination
    {
        #region Public Methods

        public static MvcHtmlString NumbersOnlyPager(this HtmlHelper helper, PagingParameters pagingParameters, string baseUrl)
        {
            return BuildPager(pagingParameters, baseUrl, true);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, PagingParameters pagingParameters, string baseUrl)
        {
            return BuildPager(pagingParameters, baseUrl, false);
        }

        #endregion

        #region Methods

        private static MvcHtmlString BuildPager(PagingParameters pagingParameters, string baseUrl, bool numbersOnly)
        {
            int totalPages = pagingParameters.NumberOfPages;

            if (totalPages <= 1)
            {
                return MvcHtmlString.Empty;
            }

            StringBuilder pagerBuilder = new StringBuilder("[ " + General.GoToPage);

            const string FormatNormal = "<span> <a href='{0}/{1}'>{2}</a></span>";
            const string FormatSelected = "<span class='selected'> {0}</span>";

            if (!numbersOnly && pagingParameters.CurrentPage > 1)
            {
                pagerBuilder.AppendFormat(FormatNormal, baseUrl, pagingParameters.CurrentPage - 1, General.Previous);
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
                pagerBuilder.AppendFormat(FormatNormal, baseUrl, pagingParameters.CurrentPage + 1, General.Next);
            }

            return MvcHtmlString.Create(pagerBuilder + " ]");
        }

        #endregion
    }
}