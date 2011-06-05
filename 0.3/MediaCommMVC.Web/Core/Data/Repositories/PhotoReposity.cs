#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data.NHInfrastructure;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Users;

using NHibernate;
using NHibernate.Linq;

using Enumerable = System.Linq.Enumerable;
using Queryable = System.Linq.Queryable;

#endregion

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    using MediaCommMVC.Web.Core.Infrastructure;

    /// <summary>Implements the IPhotoRepository using NHibernate.</summary>
    public class PhotoRepository : RepositoryBase, IPhotoRepository
    {
        #region Constants and Fields

        /// <summary>Folder containing unprocessed files.</summary>
        private const string UnprocessedPhotosFolder = "unprocessed";

        /// <summary>The image generator.</summary>
        private readonly IImageGenerator imageGenerator;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="PhotoRepository"/> class.</summary>
        /// <param name="sessionManager">The session manager.</param>
        /// <param name="configAccessor">The config Accessor.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="imageGenerator">The image generator.</param>
        public PhotoRepository(ISessionContainer sessionManager, IConfigAccessor configAccessor, ILogger logger, IImageGenerator imageGenerator)
            : base(sessionManager, configAccessor, logger)
        {
            this.imageGenerator = imageGenerator;
        }

        #endregion

        #region Implemented Interfaces

        #region IPhotoRepository

        /// <summary>Adds the category to the persistence layer.</summary>
        /// <param name="category">The category.</param>
        public void AddCategory(PhotoCategory category)
        {
            this.Session.SaveOrUpdate(category);
        }

        /// <summary>Adds the photos in the specified folder to the DB.</summary>
        /// <param name="album">The album.</param>
        /// <param name="uploader">The uploader.</param>
        public void AddPhotos(PhotoAlbum album, MediaCommUser uploader)
        {
            string targetPath = this.GetTargetPath(album);
            string unprocessedPath = Path.Combine(targetPath, UnprocessedPhotosFolder);

            IEnumerable<FileInfo> newFiles = this.MovePhotos(targetPath, unprocessedPath);

            this.AddPicturesToDB(newFiles, album, uploader);

            this.imageGenerator.GenerateImages(targetPath, UnprocessedPhotosFolder);
        }

        /// <summary>Gets the 4 newest albums.</summary>
        /// <returns>The 4 newest albums.</returns>
        public IEnumerable<PhotoAlbum> Get4NewestAlbums()
        {
            List<PhotoAlbum> albums = this.Session.Query<PhotoAlbum>().OrderByDescending(a => a.LastPicturesAdded).Take(4).ToList();

            return albums;
        }

        public PhotoAlbum GetAlbumById(int albumId)
        {
            PhotoAlbum album = this.Session.Get<PhotoAlbum>(albumId);

            return album;
        }

        public IEnumerable<PhotoAlbum> GetAlbumsForCategoryIdStartingWith(int catId, string term)
        {
            IEnumerable<PhotoAlbum> albums =
                this.Session.Query<PhotoAlbum>().Where(
                    a => a.PhotoCategory.Id == catId && a.Name.StartsWith(term)).ToList();

            return albums;
        }

        public IEnumerable<PhotoCategory> GetAllCategories()
        {
            IEnumerable<PhotoCategory> categories = this.Session.Query<PhotoCategory>().ToList();

            return categories;
        }

        public PhotoCategory GetCategoryById(int id)
        {
            PhotoCategory category = this.Session.Query<PhotoCategory>().FetchMany(c => c.Albums).Single(c => c.Id == id);

            return category;
        }

        public Image GetImage(int photoId, string size)
        {
            Photo photo = this.GetPhotoById(photoId);

            string fileName = photo.FileName.Insert(photo.FileName.LastIndexOf("."), size);

            string imagePath =
                Path.Combine(
                    this.ConfigAccessor.GetConfigValue("PhotoRootDir"),
                    Path.Combine(this.GetValidDirectoryName(photo.PhotoAlbum.PhotoCategory.Name), Path.Combine(this.GetValidDirectoryName(photo.PhotoAlbum.Name), fileName)));

            Image image = Image.FromFile(imagePath);

            // Increase viewcount if the image was not loaded as thumbnail
            if (!size.Equals("small", StringComparison.OrdinalIgnoreCase))
            {
                photo.ViewCount++;
                this.Session.Update(photo);
            }

            return image;
        }

        public Photo GetPhotoById(int id)
        {
            Photo photo = this.Session.Get<Photo>(id);

            return photo;
        }

        public string GetStoragePathForAlbum(PhotoAlbum album)
        {
            string storagePathForAlbum = Path.Combine(this.GetTargetPath(album), UnprocessedPhotosFolder);

            if (!Directory.Exists(storagePathForAlbum))
            {
                Directory.CreateDirectory(storagePathForAlbum);
            }

            return storagePathForAlbum;
        }

        #endregion

        #endregion

        #region Methods

        private static IEnumerable<FileInfo> GetFilesRecursive(DirectoryInfo directory)
        {
            foreach (DirectoryInfo di in directory.GetDirectories())
            {
                foreach (FileInfo fi in GetFilesRecursive(di))
                {
                    yield return fi;
                }
            }

            foreach (FileInfo fi in directory.GetFiles())
            {
                yield return fi;
            }
        }

        private void AddPicturesToDB(IEnumerable<FileInfo> filesToAdd, PhotoAlbum album, MediaCommUser uploader)
        {
            this.Logger.Debug("Adding {0} photos from the folder to the database. Album: '{1}', Uploader: '{2}'", filesToAdd.Count(), album, uploader);

            PhotoAlbum photoAlbum =
                Queryable.SingleOrDefault<PhotoAlbum>(this.Session.Query<PhotoAlbum>(), a => a.Name.Equals(album.Name)) ?? album;

            photoAlbum.LastPicturesAdded = DateTime.Now;

            foreach (FileInfo file in filesToAdd)
            {
                Bitmap bmp = new Bitmap(file.FullName);
                int height = bmp.Height;
                int width = bmp.Width;
                bmp.Dispose();

                Photo photo = new Photo
                    {
                        PhotoAlbum = photoAlbum,
                        FileName = file.Name,
                        FileSize = file.Length,
                        Height = height,
                        Uploader = uploader,
                        Width = width
                    };

                this.Session.Save(photo);
            }
        }

        private string GetTargetPath(PhotoAlbum album)
        {
            string targetPath = Path.Combine(
                this.ConfigAccessor.GetConfigValue("PhotoRootDir"),
                Path.Combine(this.GetValidDirectoryName(album.PhotoCategory.Name), this.GetValidDirectoryName(album.Name)));

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            return targetPath;
        }

        private string GetValidDirectoryName(string directoryName)
        {
            this.Logger.Debug("Getting valid directory name for '{0}'", directoryName);

            char[] invalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).Distinct().ToArray();
            string invalidCharsRegexString = string.Format(@"[{0}]", Regex.Escape(new string(invalidChars) + " $.§ß%^&;=,'^´`#"));
            string validName = Regex.Replace(directoryName, invalidCharsRegexString, "_");

            this.Logger.Debug("Got '{0}' as valid directory name", validName);

            return validName;
        }

        /// <summary>Moves the photos to the final folder and renames duplicates.</summary>
        /// <param name="targetPath">The target path.</param>
        /// <param name="unprocessedPath">The path containing the unprocessed photos.</param>
        /// <returns>A list of all file path.</returns>
        private IEnumerable<FileInfo> MovePhotos(string targetPath, string unprocessedPath)
        {
            IEnumerable<FileInfo> allFiles = GetFilesRecursive(new DirectoryInfo(unprocessedPath)).ToList();

            this.Logger.Debug("Moving {2} photos from '{0}' to '{1}'", unprocessedPath, targetPath, allFiles.Count());

            List<FileInfo> newFiles = new List<FileInfo>();

            foreach (FileInfo file in allFiles)
            {
                string newPath = Path.Combine(targetPath, file.Name);
                string newFilename = file.Name;

                try
                {
                    // Copy files from subdirectories to the unprocessed folder
                    if (!file.FullName.Equals(Path.Combine(unprocessedPath, file.Name)))
                    {
                        if (File.Exists(Path.Combine(unprocessedPath, file.Name)))
                        {
                            newFilename = file.Name.Replace(file.Extension, file.Directory.Name) + file.Extension;
                            newPath = Path.Combine(targetPath, newFilename);
                        }

                        File.Copy(file.FullName, Path.Combine(unprocessedPath, newFilename));
                    }

                    File.Copy(file.FullName, newPath);
                    newFiles.Add(new FileInfo(newPath));
                }
                catch (IOException ex)
                {
                    this.Logger.Error(string.Format("Unable to copy file '{0}' to '{1}'", file, newPath), ex);
                }
            }

            return newFiles;
        }

        #endregion
    }
}
