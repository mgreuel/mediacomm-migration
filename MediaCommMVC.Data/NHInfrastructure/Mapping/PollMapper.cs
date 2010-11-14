#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>
    ///   Makes customizations to the auto mapping of the Poll type.
    /// </summary>
    public class PollMapper : IAutoMappingOverride<Poll>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Poll>

        /// <summary>
        ///   Overrides the specified mapping.
        /// </summary>
        /// <param name = "mapping">The mapping.</param>
        public void Override(AutoMapping<Poll> mapping)
        {
            mapping.Table("Polls");
            mapping.HasMany(p => p.PossibleAnswers).Cascade.All();
            mapping.HasMany(p => p.UserAnswers).Cascade.All();
            mapping.IgnoreProperty(p => p.UserAnswersWithCount);
        }

        #endregion

        #endregion
    }
}
