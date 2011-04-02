namespace MediaCommMVC.Core.Data.Nh.Mapping.Overrides
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using MediaCommMVC.Core.Model;

    #endregion

    public class MediaCommUserMap : IAutoMappingOverride<MediaCommUser>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<MediaCommUser>

        public void Override(AutoMapping<MediaCommUser> mapping)
        {
            mapping.Table("MediaCommUsers");
            mapping.Map(u => u.DateOfBirth).Default("null");
            mapping.Map(u => u.UserName).Not.Nullable().UniqueKey("UK_Username");
        }

        #endregion

        #endregion
    }
}