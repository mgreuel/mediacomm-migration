#region Using Directives

using System.Diagnostics.CodeAnalysis;

#endregion

namespace MediaCommMVC.Web.Core.Serialization
{
    /// <summary>
    ///   The parameters posted to the server by DataTables.
    /// </summary>
    public class DataTableParameters
    {
        #region Properties

        /// <summary>
        ///   Gets or sets a value indicating whether the [Global search is regex or not].
        /// </summary>
        /// <value><c>true</c> if [Global search is a regex]; otherwise, <c>false</c>.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public bool bEscapeRegex { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether [Individual column filter for the first column is regex or not].
        /// </summary>
        /// <value><c>true</c> if [Individual column filter for the first column is a regex]; otherwise, <c>false</c>.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public bool bEscapeRegex_0 { get; set; }

        /// <summary>
        ///   Gets or sets the Number of columns being displayed.
        /// </summary>
        /// <value>The Number of columns being displayed.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iColumns { get; set; }

        /// <summary>
        ///   Gets or sets the Number of records to display.
        /// </summary>
        /// <value>The Number of records to display.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iDisplayLength { get; set; }

        /// <summary>
        ///   Gets or sets the Display start point.
        /// </summary>
        /// <value>The Display start point.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iDisplayStart { get; set; }

        /// <summary>
        ///   Gets or sets the Column being sorted on.
        /// </summary>
        /// <value>The Column being sorted on.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iSortCol_0 { get; set; }

        /// <summary>
        ///   Gets or sets Direction to be sorted.
        /// </summary>
        /// <value>The Direction to be sorted.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public string iSortDir_0 { get; set; }

        /// <summary>
        ///   Gets or sets the Number of columns to sort on.
        /// </summary>
        /// <value>The Number of columns to sort on.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iSortingCols { get; set; }

        /// <summary>
        ///   Gets or sets the Information for DataTables to use for rendering.
        /// </summary>
        /// <value>The Information for DataTables to use for rendering.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int sEcho { get; set; }

        /// <summary>
        ///   Gets or sets the Global search field.
        /// </summary>
        /// <value>The Global search field.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public string sSearch { get; set; }

        /// <summary>
        ///   Gets or sets Individual column filter for the first column.
        /// </summary>
        /// <value>The Individual column filter for the first column.</value>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int sSearch_0 { get; set; }

        #endregion
    }
}