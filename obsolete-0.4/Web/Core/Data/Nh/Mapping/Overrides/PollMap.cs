namespace MediaCommMVC.Core.Data.Nh.Mapping.Overrides
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using MediaCommMVC.Core.Model.Forums;

    #endregion

    public class PollMap : IAutoMappingOverride<Poll>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Poll>

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
