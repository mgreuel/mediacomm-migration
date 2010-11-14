#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Photos;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>
    ///   Makes customizations to the auto mapping of the PhotoAlbum type.
    /// </summary>
    public class PhotoAlbumMapper : IAutoMappingOverride<PhotoAlbum>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<PhotoAlbum>

        /// <summary>
        ///   Overrides the specified mapping.
        /// </summary>
        /// <param name = "mapping">The Photo auto mapping.</param>
        public void Override(AutoMapping<PhotoAlbum> mapping)
        {
            mapping.Table("PhotoAlbums");
            mapping.Map(a => a.Name).Not.Nullable().UniqueKey("uk_nameCat");
            mapping.References(a => a.PhotoCategory).Not.Nullable().UniqueKey("uk_nameCat").ForeignKey("PhotoCategoryID").Cascade.SaveUpdate();
            mapping.Map(a => a.PhotoCount).Formula("(SELECT COUNT(*) FROM photos WHERE photos.PhotoAlbumID = Id)");
            mapping.Map(a => a.CoverPhotoId).Formula(
                "(SELECT TOP 1 photos.Id FROM photos WHERE photos.PhotoAlbumID = Id ORDER BY photos.ViewCount)");
            mapping.HasMany(a => a.Photos).Inverse();
        }

        #endregion

        #endregion
    }
}