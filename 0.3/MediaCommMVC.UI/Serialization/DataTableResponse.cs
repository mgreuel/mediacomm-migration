#region Using Directives

using System.Diagnostics.CodeAnalysis;

#endregion

namespace MediaCommMVC.UI.Serialization
{
    /// <summary>
    ///   The response DataTables excpects from the server.
    /// </summary>
    public class DataTableResponse
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the data which should be displayed in DataTables.
        /// </summary>
        /// <value>The data which should be displayed in DataTables.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public virtual string[][] aaData { get; set; }

        /// <summary>
        ///   Gets or sets the Total number of records, after filtering.
        /// </summary>
        /// <value>The Total number of records, after filtering.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iTotalDisplayRecords { get; set; }

        /// <summary>
        ///   Gets or sets the Total number of records, before filtering.
        /// </summary>
        /// <value>The Total number of records, before filtering.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iTotalRecords { get; set; }

        /// <summary>
        ///   Gets or sets the string of column names, comma separated.
        /// </summary>
        /// <value>The string of column names, comma separated.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public string sColumns { get; set; }

        /// <summary>
        ///   Gets or sets Information for DataTables to use for rendering.
        /// </summary>
        /// <value>The Information for DataTables to use for rendering.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int sEcho { get; set; }

        #endregion
    }
}