using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class TopicReadMapper : IAutoMappingOverride<TopicRead>
    {
        public void Override(AutoMapping<TopicRead> mapping)
        {
            mapping.Table("ForumTopicsRead");
            mapping.References(tr => tr.ReadByUser).Not.LazyLoad();
            mapping.References(tr => tr.ReadTopic).Not.LazyLoad();
        }
    }
}