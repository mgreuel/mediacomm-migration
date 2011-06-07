using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class ForumTopicsExcludedUserMapper : IAutoMappingOverride<ForumTopicsExcludedUser>
    {
        public void Override(AutoMapping<ForumTopicsExcludedUser> mapping)
        {
            mapping.Table("ForumTopicsExcludedUsers");
            mapping.CompositeId().KeyReference(ex => ex.MediaCommUser, "MediaCommUserID").KeyReference(ex => ex.Topic, "TopicID");
            mapping.References(ex => ex.Topic);
            mapping.References(ex => ex.MediaCommUser);
        }
    }
}