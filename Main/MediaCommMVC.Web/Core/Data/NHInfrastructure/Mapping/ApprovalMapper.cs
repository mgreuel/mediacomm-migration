using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class ApprovalMapper : IAutoMappingOverride<Approval>
    {
        public void Override(AutoMapping<Approval> mapping)
        {
            mapping.Table("Approvals");
            mapping.References(a => a.ApprovedBy).Not.LazyLoad();
        }
    }
}