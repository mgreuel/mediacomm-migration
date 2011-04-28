using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Data.Nh.Conventions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.AcceptanceCriteria;
    using FluentNHibernate.Conventions.Inspections;
    using FluentNHibernate.Conventions.Instances;

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