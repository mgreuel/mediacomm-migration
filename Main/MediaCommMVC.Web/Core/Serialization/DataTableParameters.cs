using System.Diagnostics.CodeAnalysis;

namespace MediaCommMVC.Web.Core.Serialization
{
    public class DataTableParameters
    {
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public bool bEscapeRegex { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public bool bEscapeRegex_0 { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iColumns { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iDisplayLength { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iDisplayStart { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iSortCol_0 { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public string iSortDir_0 { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iSortingCols { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int sEcho { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public string sSearch { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int sSearch_0 { get; set; }
    }
}