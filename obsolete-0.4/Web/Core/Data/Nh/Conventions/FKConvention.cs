namespace MediaCommMVC.Core.Data.Nh.Conventions
{
    #region Using Directives

    using System;

    using FluentNHibernate;
    using FluentNHibernate.Conventions;

    #endregion

    public class FKConvention : ForeignKeyConvention
    {
        #region Methods

        protected override string GetKeyName(Member property, Type type)
        {
            string fk = property == null ? type.Name + "ID" : property.Name + "ID";

            return fk;
        }

        #endregion
    }
}