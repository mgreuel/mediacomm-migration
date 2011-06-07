using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class TopicMapper : IAutoMappingOverride<Topic>
    {
        public void Override(AutoMapping<Topic> mapping)
        {
            mapping.Table("ForumTopics");
            mapping.References(t => t.Forum).Not.Nullable().Cascade.SaveUpdate();
            mapping.Map(t => t.PostCount).Formula("(SELECT COUNT(*) FROM forumPosts p where p.TopicID = Id)");
            mapping.Map(t => t.CreatedBy).Not.Nullable();
            mapping.Map(t => t.Title).Not.Nullable();
            mapping.References(t => t.Poll).Cascade.All();
            mapping.IgnoreProperty(t => t.ReadByCurrentUser);
            mapping.IgnoreProperty(t => t.ExcludedUsernames);
            mapping.HasManyToMany(t => t.ExcludedUsers).Table("ForumTopicsExcludedUsers").Cascade.None();
            //mapping.Map(t => t.ExcludedUserNames).Formula("(SELECT u.UserName FROM MediaCommUsers u JOIN ForumTopicsExcludedUsers ex ON (u.Id = ex.MediaCommUserID) WHERE ex.TopicID = Id)");
        }
    }
}