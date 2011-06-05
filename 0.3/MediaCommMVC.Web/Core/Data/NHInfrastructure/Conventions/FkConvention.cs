#region Using Directives

using System;

using FluentNHibernate;
using FluentNHibernate.Conventions;

#endregion

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Conventions
{
    /// <summary>Overrides the FluentNHibernate ForeignKeyConvention.</summary>
    public class FKConvention : ForeignKeyConvention
    {
        #region Methods

        /// <summary>Gets the name of the key.</summary>
        /// <param name="property">The property.</param>
        /// <param name="type">The  type.</param>
        /// <returns>The FK Name.</returns>
        protected override string GetKeyName(Member property, Type type)
        {
            string fk = property == null ? type.Name + "ID" : property.Name + "ID";

            return fk;
        }

        #endregion
    }
}