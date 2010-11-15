#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the TopicRead type.</summary>
    public class TopicReadMapper : IAutoMappingOverride<TopicRead>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<TopicRead>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The mapping.</param>
        public void Override(AutoMapping<TopicRead> mapping)
        {
            mapping.Table("ForumTopicsRead");
        }

        #endregion

        #endregion
    }
}
