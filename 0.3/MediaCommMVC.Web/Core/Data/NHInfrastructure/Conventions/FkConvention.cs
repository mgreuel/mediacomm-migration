using System;

using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Conventions
{
    public class FKConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            string fk = property == null ? type.Name + "ID" : property.Name + "ID";

            return fk;
        }
    }
}