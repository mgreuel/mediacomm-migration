#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Photos;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the Photo type.</summary>
    public class PhotoMapper : IAutoMappingOverride<Photo>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Photo>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The Photo auto mapping.</param>
        public void Override(AutoMapping<Photo> mapping)
        {
            mapping.Table("Photos");
            mapping.Map(p => p.FileName).Not.Nullable().UniqueKey("uk_nameAlbum");
            mapping.References(p => p.PhotoAlbum).Not.Nullable().UniqueKey("uk_nameAlbum").Cascade.SaveUpdate();
            mapping.References(p => p.Uploader).Not.Nullable().Cascade.SaveUpdate();
        }

        #endregion

        #endregion
    }
}