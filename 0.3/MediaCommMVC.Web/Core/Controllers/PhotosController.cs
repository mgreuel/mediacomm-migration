using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Elmah;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Helpers;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Users;
using MediaCommMVC.Web.Core.ViewModel;

namespace MediaCommMVC.Web.Core.Controllers
{
    public class PhotosController : Controller
    {
        private readonly ILogger logger;

        private readonly IPhotoRepository photoRepository;

        private readonly IUserRepository userRepository;

        public PhotosController(IPhotoRepository photoRepository, IUserRepository userRepository, ILogger logger)
        {
            this.photoRepository = photoRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        [Authorize]
        [NHibernateActionFilter]
        public ActionResult Album(int id)
        {
            PhotoAlbum album = this.photoRepository.GetAlbumById(id);

            return this.View(album);
        }

        [HttpGet]
        [Authorize]
        [NHibernateActionFilter]
        public ActionResult Category(int id)
        {
            PhotoCategory category = this.photoRepository.GetCategoryById(id);

            return this.View(category);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [NHibernateActionFilter]
        public ActionResult CompleteUpload(PhotoCategory category, PhotoAlbum album)
        {
            album.Name = album.Name.Trim();
            album.PhotoCategory = this.photoRepository.GetCategoryById(category.Id);

            MediaCommUser uploader = this.userRepository.GetUserByName(this.User.Identity.Name);
            this.photoRepository.AddPhotos(album, uploader);

            return this.RedirectToAction("UploadSuccessFull");
        }

        [HttpGet]
        [NHibernateActionFilter]
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

        [HttpGet]
        [Authorize]
        [OutputCache(Duration = 3600, VaryByParam = "")]
        [NHibernateActionFilter]
        public ActionResult GetCategories()
        {
            IEnumerable<PhotoCategory> categories = this.photoRepository.GetAllCategories();

            var categoryViewModels = categories.Select(c => new { c.Name, c.Id, c.AlbumCount, EncodedName = this.Url.ToFriendlyUrl(c.Name) });

            return this.Json(categoryViewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Photo(int id, string size)
        {
            Image image = this.photoRepository.GetImage(id, size);

            return new ImageResult { Image = image };
        }

        [Authorize]
        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Upload()
        {
            IEnumerable<PhotoCategory> categories = this.photoRepository.GetAllCategories();

            return this.View(new PhotoUpload { Categories = categories });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NHibernateActionFilter]
        public string UploadFile(PhotoCategory category, PhotoAlbum album, string token)
        {
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
                    new Error(new FileNotFoundException("No file was send to the server on the PhotoUpload Action"), context));
                return "false";
            }

            return "true";
        }

        [HttpGet]
        [Authorize]
        public ActionResult UploadSuccessFull()
        {
            return this.View();
        }

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
    }
}