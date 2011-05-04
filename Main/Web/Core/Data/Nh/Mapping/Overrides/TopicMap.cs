namespace MediaCommMVC.Core.Data.Nh.Mapping.Overrides
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using MediaCommMVC.Core.Model.Forums;

    #endregion

    public class TopicMap : IAutoMappingOverride<Topic>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Topic>

        public void Override(AutoMapping<Topic> mapping)
        {
            mapping.Table("ForumTopics");
            mapping.References(t => t.Forum).Not.Nullable().Cascade.SaveUpdate();
            mapping.Map(t => t.PostCount).Formula("(SELECT COUNT(*) FROM forumPosts p where p.TopicID = Id)");
            mapping.Map(t => t.CreatedBy).Not.Nullable();
            mapping.Map(t => t.Title).Not.Nullable();

            // mapping.References(t => t.Poll).Cascade.All();
            mapping.IgnoreProperty(t => t.HasUnreadPosts);
            mapping.HasManyToMany(t => t.ExcludedUsers).Table("ForumTopicsExcludedUsers").Cascade.None();
        }

        #endregion

        #endregion
    }
}