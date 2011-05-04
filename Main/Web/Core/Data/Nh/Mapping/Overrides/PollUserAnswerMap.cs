namespace MediaCommMVC.Core.Data.Nh.Mapping.Overrides
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using MediaCommMVC.Core.Model.Forums;

    #endregion

    /// <summary>Makes customizations to the auto mapping of the PollUserAnswer type.</summary>
    public class PollUserAnswerMap : IAutoMappingOverride<PollUserAnswer>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<PollUserAnswer>

        public void Override(AutoMapping<PollUserAnswer> mapping)
        {
            mapping.Table("PollUserAnswers");
        }

        #endregion

        #endregion
    }
}
