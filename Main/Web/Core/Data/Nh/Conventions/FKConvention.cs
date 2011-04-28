using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Data.Nh.Conventions
{
    using FluentNHibernate;
    using FluentNHibernate.Conventions;

    public class FKConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            string fk = property == null ? type.Name + "ID" : property.Name + "ID";

            return fk;
        }
    }
}