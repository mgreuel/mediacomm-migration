#region Using Directives

using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Conventions
{
    /// <summary>Sets the DB type for enums to int.</summary>
    public class EnumConvention : IUserTypeConvention
    {
        #region Implemented Interfaces

        #region IConvention<IPropertyInspector,IPropertyInstance>

        /// <summary>Applies the convention to the specified target.</summary>
        /// <param name="target">The target.</param>
        public void Apply(IPropertyInstance target)
        {
            target.CustomType(target.Property.PropertyType);
        }

        #endregion

        #region IConventionAcceptance<IPropertyInspector>

        /// <summary>Accepts the specified criteria.</summary>
        /// <param name="criteria">The criteria.</param>
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType.IsEnum);
        }

        #endregion

        #endregion
    }
}
