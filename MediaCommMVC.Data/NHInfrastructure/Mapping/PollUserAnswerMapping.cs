using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the PollUserAnswer type.</summary>
    public class PollUserAnswerMapping : IAutoMappingOverride<PollUserAnswer>
    {
        public void Override(AutoMapping<PollUserAnswer> mapping)
        {
            mapping.Table("PollUserAnswers");
        }
    }
}
