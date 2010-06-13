namespace MediaCommMVC.Core.Parameters
{
    /// <summary>Contains information needed for paging.</summary>
    public class PagingParameters
    {
        #region Properties

        /// <summary>Gets or sets the current page.</summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; set; }

        /// <summary>Gets or sets the number of items per page.</summary>
        /// <value>The number of items per page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        public int TotalCount { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("CurrentPage: '{0}', PageSize: '{1}', TotalCount: '{2}'", this.CurrentPage, this.PageSize, this.TotalCount);
        }

        #endregion
    }
}
