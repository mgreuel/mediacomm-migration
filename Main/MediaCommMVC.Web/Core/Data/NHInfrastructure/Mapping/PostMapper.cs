using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class PostMapper : IAutoMappingOverride<Post>
    {
        public void Override(AutoMapping<Post> mapping)
        {
            mapping.Table("ForumPosts");
            mapping.References(p => p.Topic).Not.Nullable().Cascade.SaveUpdate();
            mapping.References(p => p.Author).Not.LazyLoad().Not.Nullable().Cascade.SaveUpdate();
            mapping.Map(p => p.Text).CustomSqlType("nvarchar(MAX)");
            mapping.Map(p => p.Created).CustomSqlType("datetime2");
        }
    }
}