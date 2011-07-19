namespace MediaCommMVC.Core.Helpers
{
    #region Using Directives

    using System;

    #endregion

    public class PagingParameters
    {
        #region Properties

        public int CurrentPage { get; set; }

        public int NumberOfPages
        {
            get
            {
                if (this.PageSize <= 0)
                {
                    return 0;
                }

                return (int)Math.Ceiling(this.TotalCount / (decimal)this.PageSize);
            }
        }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        #endregion
    }
}