#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Photos;

#endregion

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the PhotoCategory type.</summary>
    public class PhotoCategoryMapper : IAutoMappingOverride<PhotoCategory>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<PhotoCategory>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The photo category auto mapping.</param>
        public void Override(AutoMapping<PhotoCategory> mapping)
        {
            mapping.Table("PhotoCategories");
            mapping.Map(c => c.Name).Not.Nullable().Unique();
            mapping.Map(c => c.PhotoCount).Formula("(SELECT COUNT(*) FROM photos p JOIN photoAlbums a ON (p.PhotoAlbumID = a.Id) WHERE a.PhotoCategoryId = Id)");
            mapping.Map(c => c.AlbumCount).Formula("(SELECT COUNT(*) FROM photoAlbums a WHERE a.PhotoCategoryId = Id)");
            mapping.HasMany(c => c.Albums).Inverse();
        }

        #endregion

        #endregion
    }
}