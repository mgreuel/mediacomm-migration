#region Using Directives

using System;
using System.Linq;
using System.Web.Mvc;

#endregion

namespace MediaCommMVC.UI.Helpers
{
    /// <summary>Contains helper methods for enums.</summary>
    public static class EnumHelper
    {
        #region Public Methods

        /// <summary>Creates a select list from an enum.</summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumObj">The enum to create the selectlist from.</param>
        /// <returns>A selectlist with the enum values.</returns>
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { ID = e, Name = e.ToString() };

            return new SelectList(values, "Id", "Name", enumObj);
        }

        #endregion
    }
}