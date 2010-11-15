﻿#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the Forum type.</summary>
    public class ForumMapper : IAutoMappingOverride<Forum>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Forum>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The Forum auto mapping.</param>
        public void Override(AutoMapping<Forum> mapping)
        {
            mapping.Table("Forums");
            mapping.IgnoreProperty(f => f.HasUnreadTopics);
            mapping.Map(f => f.Title).Not.Nullable();
            mapping.Map(f => f.DisplayOrderIndex).Default("0");
            mapping.Map(f => f.TopicCount).Formula("(SELECT COUNT(*) FROM forumTopics p where p.ForumID = Id)");
            mapping.Map(f => f.PostCount).Formula(
                "(select COUNT(*) from forumPosts p JOIN forumTopics t ON (t.Id = p.TopicID) WHERE t.ForumID = Id)");
        }

        #endregion

        #endregion
    }
}