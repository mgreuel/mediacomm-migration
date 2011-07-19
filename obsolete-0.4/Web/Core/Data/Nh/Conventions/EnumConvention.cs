namespace MediaCommMVC.Core.Data.Nh.Conventions
{
    #region Using Directives

    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;

    #endregion

    public class EnumConvention : IUserTypeConvention
    {
        #region Implemented Interfaces

        #region IConvention<IPropertyInspector,IPropertyInstance>

        public void Apply(IPropertyInstance target)
        {
            target.CustomType(target.Property.PropertyType);
        }

        #endregion

        #region IConventionAcceptance<IPropertyInspector>

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Property.PropertyType.IsEnum);
        }

        #endregion

        #endregion
    }
}