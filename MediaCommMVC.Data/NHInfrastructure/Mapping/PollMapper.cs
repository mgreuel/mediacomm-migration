using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the Poll type.</summary>
    public class PollMapper : IAutoMappingOverride<Poll>
    {
        /// <summary>
        /// Overrides the specified mapping.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        public void Override(AutoMapping<Poll> mapping)
        {
            mapping.HasMany(p => p.PossibleAnswers).Cascade.All();
            mapping.HasMany(p => p.UserAnswers).Cascade.All();
        }
    }
}
