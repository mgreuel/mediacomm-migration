using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class PollMapper : IAutoMappingOverride<Poll>
    {
        public void Override(AutoMapping<Poll> mapping)
        {
            mapping.Table("Polls");
            mapping.HasMany(p => p.PossibleAnswers).Not.LazyLoad().Cascade.All();
            mapping.HasMany(p => p.UserAnswers).Not.LazyLoad().Cascade.All();
            mapping.IgnoreProperty(p => p.UserAnswersWithCount);
        }
    }
}
