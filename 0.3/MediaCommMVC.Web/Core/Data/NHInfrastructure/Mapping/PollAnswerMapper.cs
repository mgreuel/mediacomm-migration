using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class PollAnswerMapper : IAutoMappingOverride<PollAnswer>
    {
        public void Override(AutoMapping<PollAnswer> mapping)
        {
            mapping.Table("PollAnswers");
        }
    }
}