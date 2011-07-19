namespace MediaCommMVC.Core.Data.Nh.Mapping.Overrides
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using MediaCommMVC.Core.Model.Forums;

    #endregion

    public class TopicReadMap : IAutoMappingOverride<TopicRead>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<TopicRead>

        public void Override(AutoMapping<TopicRead> mapping)
        {
            mapping.Table("ForumTopicsRead");
        }

        #endregion

        #endregion
    }
}
