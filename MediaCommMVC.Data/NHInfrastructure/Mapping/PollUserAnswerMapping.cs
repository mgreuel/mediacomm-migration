#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>
    ///   Makes customizations to the auto mapping of the PollUserAnswer type.
    /// </summary>
    public class PollUserAnswerMapping : IAutoMappingOverride<PollUserAnswer>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<PollUserAnswer>

        /// <summary>
        ///   Overrides the specified mapping.
        /// </summary>
        /// <param name = "mapping">The mapping.</param>
        public void Override(AutoMapping<PollUserAnswer> mapping)
        {
            mapping.Table("PollUserAnswers");
        }

        #endregion

        #endregion
    }
}
