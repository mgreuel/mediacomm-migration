#region Using Directives

using System;
using System.Reflection;

using FluentNHibernate.Conventions;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Conventions
{
    /// <summary>Overrides the FluentNHibernate ForeignKeyConvention.</summary>
    public class FKConvention : ForeignKeyConvention
    {
        #region Methods

        /// <summary>Gets the name of the key.</summary>
        /// <param name="property">The property.</param>
        /// <param name="type">The type used for naming.</param>
        /// <returns>The get key name.</returns>
        protected override string GetKeyName(PropertyInfo property, Type type)
        {
            string fk = property == null ? type.Name + "ID" : property.Name + "ID";

            return fk;
        }

        #endregion
    }
}