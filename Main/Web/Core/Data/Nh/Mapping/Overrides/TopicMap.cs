using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Data.Nh.Mapping.Overrides
{
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using MediaCommMVC.Core.Model;

    public class TopicMap : IAutoMappingOverride<Topic>
    {
        public void Override(AutoMapping<Topic> mapping)
        {
            mapping.Table("ForumTopics");
            mapping.References(t => t.Forum).Not.Nullable().Cascade.SaveUpdate();
            mapping.Map(t => t.PostCount).Formula("(SELECT COUNT(*) FROM forumPosts p where p.TopicID = Id)");
            mapping.Map(t => t.CreatedBy).Not.Nullable();
            mapping.Map(t => t.Title).Not.Nullable();
            //mapping.References(t => t.Poll).Cascade.All();
            mapping.IgnoreProperty(t => t.HasUnreadPosts);
            mapping.HasManyToMany(t => t.ExcludedUsers).Table("ForumTopicsExcludedUsers").Cascade.None();
        }
    }
}