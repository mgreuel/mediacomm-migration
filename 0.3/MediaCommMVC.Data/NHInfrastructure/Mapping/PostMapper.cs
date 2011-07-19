#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the Post type.</summary>
    public class PostMapper : IAutoMappingOverride<Post>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Post>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The Post auto mapping.</param>
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