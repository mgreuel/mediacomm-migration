#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the PollAnswer type.</summary>
    public class PollAnswerMapper : IAutoMappingOverride<PollAnswer>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<PollAnswer>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The mapping.</param>
        public void Override(AutoMapping<PollAnswer> mapping)
        {
            mapping.Table("PollAnswers");
        }

        #endregion

        #endregion
    }
}
