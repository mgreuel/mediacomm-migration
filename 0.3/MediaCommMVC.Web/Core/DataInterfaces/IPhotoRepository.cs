#region Using Directives

using System.Collections.Generic;
using System.Drawing;

using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Users;

#endregion

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    /// <summary>The interface for all photo repositories.</summary>
    public interface IPhotoRepository
    {
        #region Public Methods

        /// <summary>Adds the category to the persistence layer.</summary>
        /// <param name="category">The category.</param>
        void AddCategory(PhotoCategory category);

        /// <summary>Adds the photos in the specified folder to the persistens storage.</summary>
        /// <param name="album">The album.</param>
        /// <param name="uploader">The uploader.</param>
        void AddPhotos(PhotoAlbum album, MediaCommUser uploader);

        /// <summary>Gets the 4 newest albums.</summary>
        /// <returns>The 4 newest albums.</returns>
        IEnumerable<PhotoAlbum> Get4NewestAlbums();

        /// <summary>Gets the album by id.</summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>The album.</returns>
        PhotoAlbum GetAlbumById(int albumId);

        /// <summary>Gets the albums with the specified category id.</summary>
        /// <param name="catId">The category id.</param>
        /// <param name="term">The term the results should start with.</param>
        /// <returns>The albums.</returns>
        IEnumerable<PhotoAlbum> GetAlbumsForCategoryIdStartingWith(int catId, string term);

        /// <summary>Gets all categories.</summary>
        /// <returns>All photo categories.</returns>
        IEnumerable<PhotoCategory> GetAllCategories();

        /// <summary>Gets the category by id.</summary>
        /// <param name="id">The category id.</param>
        /// <returns>The photo category.</returns>
        PhotoCategory GetCategoryById(int id);

        /// <summary>Gets the image.</summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="size">The image size.</param>
        /// <returns>The image.</returns>
        Image GetImage(int photoId, string size);

        /// <summary>Gets the photo by ID.</summary>
        /// <param name="id">The photo id.</param>
        /// <returns>The photo.</returns>
        Photo GetPhotoById(int id);

        /// <summary>
        /// Gets the absolut path to store the photos for this album in.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>The absolut path to store the photos for this album in.</returns>
        string GetStoragePathForAlbum(PhotoAlbum album);

        #endregion
    }
}
