namespace MediaCommMVC.Core.Data.Nh.Mapping.Overrides
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using MediaCommMVC.Core.Model.Forums;

    #endregion

    public class PollAnswerMap : IAutoMappingOverride<PollAnswer>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<PollAnswer>

        public void Override(AutoMapping<PollAnswer> mapping)
        {
            mapping.Table("PollAnswers");
        }

        #endregion

        #endregion
    }
}
