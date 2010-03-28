#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Photos;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.UI.Infrastructure;
using MediaCommMVC.UI.ViewModel;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The photos controller.</summary>
    [Authorize]
    public class PhotosController : Controller
    {
        #region Constants and Fields

        /// <summary>The config accessor.</summary>
        private readonly IConfigAccessor configAccessor;

        /// <summary>The logger.</summary>
        private readonly ILogger logger;

        /// <summary>The photo repsository.</summary>
        private readonly IPhotoRepository photoRepository;

        /// <summary>The user repository.</summary>
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="PhotosController"/> class.</summary>
        /// <param name="configAccessor">The config accessor.</param>
        /// <param name="photoRepository">The photo repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="logger">The logger.</param>
        public PhotosController(IConfigAccessor configAccessor, IPhotoRepository photoRepository, IUserRepository userRepository, ILogger logger)
        {
            this.configAccessor = configAccessor;
            this.photoRepository = photoRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>Displays a photo gallery.</summary>
        /// <param name="id">The album id.</param>
        /// <returns>The album view.</returns>
        public ActionResult Album(int id)
        {
            this.logger.Debug("Displaying photo album with id " + id);

            PhotoAlbum album = this.photoRepository.GetAlbumById(id);

            this.logger.Debug("Displaying view with photo album " + album);

            return this.View(new PhotoAlbumViewData { Album = album, PhotoCategories = this.GetCategoriesViewData() });
        }

        /// <summary>The index page.</summary>
        /// <returns>The index view.</returns>
        public ActionResult Index()
        {
            this.logger.Debug("Displaying photos index");
            return this.View(new PhotoNavigationViewData { PhotoCategories = this.GetCategoriesViewData() });

        }

        /// <summary>Displays a single photo.</summary>
        /// <param name="id">The photo id.</param>
        /// <param name="size">The photo size.</param>
        /// <returns>The photo.</returns>
        public ActionResult Photo(int id, string size)
        {
            this.logger.Debug("Displaying photo with id '{0}' and size '{1}'", id, size);

            Image image = this.photoRepository.GetImage(id, size);

            return new ImageResult { Image = image };
        }

        /// <summary>The upload page.</summary>
        /// <returns>The upload view.</returns>
        public ActionResult Upload()
        {
            this.logger.Debug("Displaying photo upload page");
            return this.View();
        }

        /// <summary>Uploads the zip file containing the photos.</summary>
        /// <param name="category">The category.</param>
        /// <param name="album">The album.</param>
        /// <returns>The uploaded view.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(PhotoCategory category, PhotoAlbum album)
        {
            this.logger.Debug("Uploading file with category '{0}' and album '{1}'", category, album);

            if (this.Request.Files.Count > 0)
            {
                category.Name = category.Name.Trim();
                album.Name = album.Name.Trim();
                this.photoRepository.AddCategory(category);
                album.Category = category;
                this.photoRepository.AddAlbum(album);

                HttpPostedFileBase file = this.Request.Files[0];
                string targetPath = Path.Combine(this.configAccessor.GetConfigValue("PhotoRootDir"), file.FileName);

                this.logger.Debug("Saving file '{0}'", targetPath);
                file.SaveAs(targetPath);

                MediaCommUser uploader = this.userRepository.GetUserByName(this.User.Identity.Name);

                this.photoRepository.ExtractAndAddPhotos(targetPath, album, uploader);
            }
            else
            {
                this.logger.Warn("No file was sent to the server");
            }

            return this.View(new PhotoNavigationViewData { PhotoCategories = this.GetCategoriesViewData() });
        }




        #endregion

        #region helper methods

        /// <summary>
        /// Gets the categories view data.
        /// </summary>
        /// <returns>The view data for displaying photo categories.</returns>
        private IEnumerable<PhotoCategoryInfo> GetCategoriesViewData()
        {
            return this.photoRepository.GetAllCategories().Select(cat => new PhotoCategoryInfo
                {
                    AlbumCount = cat.AlbumCount,
                    Id = cat.Id,
                    Name = cat.Name,
                    Albums = cat.Albums.Select(a => new PhotoAlbumInfo { Id = a.Id, Name = a.Name, PictureCount = a.PhotoCount })
                }).ToList();
        }

        #endregion
    }
}