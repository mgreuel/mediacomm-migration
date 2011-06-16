using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class PollUserAnswerMapping : IAutoMappingOverride<PollUserAnswer>
    {
        public void Override(AutoMapping<PollUserAnswer> mapping)
        {
            mapping.Table("PollUserAnswers");
        }
    }
}