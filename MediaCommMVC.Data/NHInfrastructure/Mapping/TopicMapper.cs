#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the Topic type.</summary>
    public class TopicMapper : IAutoMappingOverride<Topic>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Topic>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The Topic auto mapping.</param>
        public void Override(AutoMapping<Topic> mapping)
        {
            mapping.Table("ForumTopics");
            mapping.References(t => t.Forum).Not.Nullable().Cascade.SaveUpdate();
            mapping.Map(t => t.PostCount).Formula("(SELECT COUNT(*) FROM forumPosts p where p.TopicID = Id)");
            mapping.Map(t => t.CreatedBy).Not.Nullable();
            mapping.Map(t => t.Title).Not.Nullable();
            mapping.IgnoreProperty(t => t.ReadByCurrentUser);
        }

        #endregion

        #endregion
    }
}