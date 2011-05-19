#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Users;

#endregion

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the MediaCommUser type.</summary>
    public class MediaCommUserMapper : IAutoMappingOverride<MediaCommUser>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<MediaCommUser>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The mapping.</param>
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