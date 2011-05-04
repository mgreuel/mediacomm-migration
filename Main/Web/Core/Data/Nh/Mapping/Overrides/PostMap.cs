namespace MediaCommMVC.Core.Data.Nh.Mapping.Overrides
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using MediaCommMVC.Core.Model.Forums;

    #endregion

    public class PostMap : IAutoMappingOverride<Post>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Post>

        public void Override(AutoMapping<Post> mapping)
        {
            mapping.Table("ForumPosts");
            mapping.References(p => p.Topic).Not.Nullable().Cascade.SaveUpdate();
            mapping.References(p => p.Author).Not.Nullable().Cascade.SaveUpdate();
            mapping.Map(p => p.Text).CustomSqlType("nvarchar(MAX)");
            mapping.Map(p => p.Created).CustomSqlType("datetime2");
        }

        #endregion

        #endregion
    }
}