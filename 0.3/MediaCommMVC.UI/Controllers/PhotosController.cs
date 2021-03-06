#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Elmah;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Photos;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.UI.Helpers;
using MediaCommMVC.UI.Infrastructure;
using MediaCommMVC.UI.ViewModel;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The photos controller.</summary>
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
        [Authorize]
        public ActionResult Album(int id)
        {
            this.logger.Debug("Displaying photo album with id " + id);

            PhotoAlbum album = this.photoRepository.GetAlbumById(id);

            this.logger.Debug("Displaying view with photo album " + album);

            return this.View(album);
        }

        /// <summary>Shows the photo category.</summary>
        /// <param name="id">The category id.</param>
        /// <returns>The category view.</returns>
        [HttpGet]
        [Authorize]
        public ActionResult Category(int id)
        {
            PhotoCategory category = this.photoRepository.GetCategoryById(id);

            return this.View(category);
        }

        /// <summary>Gets the albums for the specified category id.</summary>
        /// <param name="id">The cat id.</param>
        /// <param name="term">The term the album starts with.</param>
        /// <returns>All matching albums.</returns>
        [HttpGet]   
        [Authorize]
        public ActionResult GetAlbumsForCategoryId(int id, string term)
        {
            if (id <= 0)
            {
                this.logger.Error("CatId '{0}' is invalid", id);
                return null;
            }

            IEnumerable<PhotoAlbum> albums = this.photoRepository.GetAlbumsForCategoryIdStartingWith(id, term);

            return this.Json(albums.Select(a => a.Name), JsonRequestBehavior.AllowGet);
        }

        /// <summary>Gets all photo categories.</summary>
        /// <returns>The photo categories as Json string.</returns>
        [HttpGet]
        [Authorize]
        [OutputCache(Duration = 3600, VaryByParam = "")]
        public ActionResult GetCategories()
        {
            IEnumerable<PhotoCategory> categories = this.photoRepository.GetAllCategories();

            var categoryViewModels = categories.Select(c => new { c.Name, c.Id, c.AlbumCount, EncodedName = this.Url.ToFriendlyUrl(c.Name) });

            return this.Json(categoryViewModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>The index page.</summary>
        /// <returns>The index view.</returns>
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>Displays a single photo.</summary>
        /// <param name="id">The photo id.</param>
        /// <param name="size">The photo size.</param>
        /// <returns>The photo.</returns>
        [Authorize]
        [HttpGet]
        public ActionResult Photo(int id, string size)
        {
            this.logger.Debug("Displaying photo with id '{0}' and size '{1}'", id, size);

            Image image = this.photoRepository.GetImage(id, size);

            return new ImageResult { Image = image };
        }

        /// <summary>The upload page.</summary>
        /// <returns>The upload view.</returns>
        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            IEnumerable<PhotoCategory> categories = this.photoRepository.GetAllCategories();

            return this.View(new PhotoUpload { Categories = categories });
        }

        /// <summary>
        /// Uploads the zip file containing the photos.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="album">The album.</param>
        /// <param name="token">The forms authentication token.</param>
        /// <returns>true or false, depending on the success.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public string UploadFile(PhotoCategory category, PhotoAlbum album, string token)
        {
            this.logger.Debug("Uploading file with category '{0}' and album '{1}'", category, album);

            if (this.Request.Files.Count > 0)
            {
                try
                {
                    if (string.IsNullOrEmpty(token) || !GetUploaderIdentity(token).IsAuthenticated)
                    {
                        throw new UnauthorizedAccessException("Anonymous upload is not allowed");
                    }

                    category.Name = category.Name.Trim();
                    album.Name = album.Name.Trim();
                    album.PhotoCategory = category;

                    HttpPostedFileBase file = this.Request.Files[0];
                    string directoryPath = this.photoRepository.GetStoragePathForAlbum(album);
                    string targetPath = Path.Combine(directoryPath, file.FileName);

                    this.logger.Debug("Saving file '{0}'", targetPath);
                    file.SaveAs(targetPath);
                }
                catch (Exception ex)
                {
                    this.logger.Error("Error uploading photo archive", ex);
                    return "false";
                }
            }
            else
            {
                HttpContext context = System.Web.HttpContext.Current;
                ErrorLog.GetDefault(context).Log(
                    new Error(
                        new FileNotFoundException("No file was send to the server on the PhotoUpload Action"), context));
                return "false";
            }

            return "true";
        }

        /// <summary>
        /// Uploads the zip file containing the photos.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="album">The album.</param>
        /// <returns>A redirect to the UploadSuccessfull action.</returns>
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CompleteUpload(PhotoCategory category, PhotoAlbum album)
        {
            album.Name = album.Name.Trim();
            album.PhotoCategory = this.photoRepository.GetCategoryById(category.Id);

            MediaCommUser uploader = this.userRepository.GetUserByName(this.User.Identity.Name);
            this.photoRepository.AddPhotos(album, uploader);

            return this.RedirectToAction("UploadSuccessFull");
        }

        /// <summary>Shows the uploads success full page.</summary>
        /// <returns>The upload successfull view.</returns>
        [HttpGet]
        [Authorize]
        public ActionResult UploadSuccessFull()
        {
            return this.View();
        }

        #endregion

        #region Methods

        /// <summary>Gets the uploader identity from the specified authentication token.</summary>
        /// <param name="token">The token.</param>
        /// <returns>The identity.</returns>
        private static FormsIdentity GetUploaderIdentity(string token)
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(token);

            if (ticket == null)
            {
                throw new UnauthorizedAccessException("Only authenticated users can upload files.");
            }

            FormsIdentity formsIdentity = new FormsIdentity(ticket);

            if (!formsIdentity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Only authenticated users can upload files.");
            }

            return formsIdentity;
        }

        #endregion
    }
}