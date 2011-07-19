using System.Diagnostics.CodeAnalysis;

namespace MediaCommMVC.Web.Core.Serialization
{
    public class DataTableResponse
    {
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public virtual string[][] aaData { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iTotalDisplayRecords { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int iTotalRecords { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public string sColumns { get; set; }

        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", 
            Justification = "Named like datatable parameters")]
        public int sEcho { get; set; }
    }
}