﻿using System;
using System.Linq;
using System.Web.Mvc;

namespace MediaCommMVC.Web.Core.Helpers
{
    public static class EnumHelper
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum)) select new { ID = e, Name = e.ToString() };

            return new SelectList(values, "Id", "Name", enumObj);
        }
    }
}