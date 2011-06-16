using System.Collections.Generic;
using System.Drawing;

using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    public interface IPhotoRepository
    {
        void AddCategory(PhotoCategory category);

        void AddPhotos(PhotoAlbum album, MediaCommUser uploader);

        IEnumerable<PhotoAlbum> Get4NewestAlbums();

        PhotoAlbum GetAlbumById(int albumId);

        IEnumerable<PhotoAlbum> GetAlbumsForCategoryIdStartingWith(int catId, string term);

        IEnumerable<PhotoCategory> GetAllCategories();

        PhotoCategory GetCategoryById(int id);

        Image GetImage(int photoId, string size);

        Photo GetPhotoById(int id);

        string GetStoragePathForAlbum(PhotoAlbum album);
    }
}
