using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class MediaCommUserMapper : IAutoMappingOverride<MediaCommUser>
    {
        public void Override(AutoMapping<MediaCommUser> mapping)
        {
            mapping.Table("MediaCommUsers");
            mapping.Map(u => u.DateOfBirth).Default("null");
            mapping.Map(u => u.UserName).Not.Nullable().UniqueKey("UK_Username");
        }
    }
}