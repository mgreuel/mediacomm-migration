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
        public PhotoRepository(ISessionManager sessionManager, IConfigAccessor configAccessor, ILogger logger, IImageGenerator imageGenerator)
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
            this.Logger.Debug("Adding photo category: " + category);

            this.InvokeTransaction(s => s.SaveOrUpdate(category));
        }

        /// <summary>Adds the photos in the specified folder to the DB.</summary>
        /// <param name="album">The album.</param>
        /// <param name="uploader">The uploader.</param>
        public void AddPhotos(PhotoAlbum album, MediaCommUser uploader)
        {
            this.Logger.Debug("Extracting and adding photos.Album: '{0}, Uploader: '{1}'", album, uploader);

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
            List<PhotoAlbum> albums =
                this.Session.Query<PhotoAlbum>().OrderByDescending(a => a.LastPicturesAdded).Take(4).ToList();

            return albums;
        }

        /// <summary>Gets the album by id.</summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>The album.</returns>
        public PhotoAlbum GetAlbumById(int albumId)
        {
            this.Logger.Debug("Getting album with id '{0}'", albumId);

            PhotoAlbum album = this.Session.Query<PhotoAlbum>().Single(a => a.Id == albumId);

            this.Logger.Debug("Got album: " + album);

            return album;
        }

        /// <summary>Gets the albums with the specified category id.</summary>
        /// <param name="catId">The category id.</param>
        /// <param name="term">The term the results should start with.</param>
        /// <returns>The albums.</returns>
        public IEnumerable<PhotoAlbum> GetAlbumsForCategoryIdStartingWith(int catId, string term)
        {
            this.Logger.Debug("Getting albums for category id '{0}'", catId);
            IEnumerable<PhotoAlbum> albums =
                this.Session.Query<PhotoAlbum>().Where(
                    a => a.PhotoCategory.Id == catId && a.Name.StartsWith(term)).ToList();

            this.Logger.Debug("Got {0} Albums", albums.Count());
            return albums;
        }

        /// <summary>Gets all categories.</summary>
        /// <returns>All photo categories.</returns>
        public IEnumerable<PhotoCategory> GetAllCategories()
        {
            this.Logger.Debug("Getting all photo catgories");

            IEnumerable<PhotoCategory> categories = Enumerable.ToList<PhotoCategory>(this.Session.Query<PhotoCategory>());

            this.Logger.Debug("Got {0} photo categories", categories.Count());
            return categories;
        }

        /// <summary>Gets the category by id.</summary>
        /// <param name="id">The category id.</param>
        /// <returns>The photo category.</returns>
        public PhotoCategory GetCategoryById(int id)
        {
            PhotoCategory category = this.Session.Get<PhotoCategory>(id);

            this.Logger.Debug("Got category {0} for category id '{1}'", category, id);

            return category;
        }

        /// <summary>Gets the image path.</summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="size">The photo size.</param>
        /// <returns>The path to the image file.</returns>
        public Image GetImage(int photoId, string size)
        {
            this.Logger.Debug("Getting image with id '{0}' and size '{1}'", photoId, size);
            Photo photo = this.GetPhotoById(photoId);

            string fileName = photo.FileName.Insert(photo.FileName.LastIndexOf("."), size);

            string imagePath =
                Path.Combine(
                    this.ConfigAccessor.GetConfigValue("PhotoRootDir"), 
                    Path.Combine(this.GetValidDirectoryName(photo.PhotoAlbum.PhotoCategory.Name), Path.Combine(this.GetValidDirectoryName(photo.PhotoAlbum.Name), fileName)));

            this.Logger.Debug("Getting image from '{0}'", imagePath);
            Image image = Image.FromFile(imagePath);

            // Increase viewcount if the image was not loaded as thumbnail
            if (!size.Equals("small", StringComparison.OrdinalIgnoreCase))
            {
                this.InvokeTransaction(
                    s =>
                    {
                        photo.ViewCount++;
                        s.Update(photo);
                    });
            }

            return image;
        }

        /// <summary>Gets the photo by ID.</summary>
        /// <param name="id">The photo id.</param>
        /// <returns>The photo.</returns>
        public Photo GetPhotoById(int id)
        {
            this.Logger.Debug("Getting photo with id '{0}'", id);
            Photo photo = this.Session.Get<Photo>(id);

            return photo;
        }

        /// <summary>
        /// Gets the absolut path to store the photos for this album in.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>
        /// The absolut path to store the photos for this album in.
        /// </returns>
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

        /// <summary>Gets all files in the directory recursively.</summary>
        /// <param name="directory">The dirrecory info.</param>
        /// <returns>All files.</returns>
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

        /// <summary>Adds the pictures to DB.</summary>
        /// <param name="filesToAdd">The files to add.</param>
        /// <param name="album">The album.</param>
        /// <param name="uploader">The uploader.</param>
        private void AddPicturesToDB(IEnumerable<FileInfo> filesToAdd, PhotoAlbum album, MediaCommUser uploader)
        {
            this.Logger.Debug("Adding {0} photos from the folder to the database. Album: '{1}', Uploader: '{2}'", filesToAdd.Count(), album, uploader);

            PhotoAlbum photoAlbum =
                Queryable.SingleOrDefault<PhotoAlbum>(this.Session.Query<PhotoAlbum>(), a => a.Name.Equals(album.Name)) ?? album;

            photoAlbum.LastPicturesAdded = DateTime.Now;

            this.InvokeTransaction(delegate(ISession session)
                {
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
   
                            this.Logger.Debug("Adding photo '{0}' to the database");
                            session.Save(photo);
                    }
                });

            this.Logger.Debug("Finished adding photos to the database.");
        }

        /// <summary>Gets the path where the photos will be stored.</summary>
        /// <param name="album">The album.</param>
        /// <returns>The target path.</returns>
        private string GetTargetPath(PhotoAlbum album)
        {
            this.Logger.Debug("Getting file system path for album: " + album);

            string targetPath = Path.Combine(
                this.ConfigAccessor.GetConfigValue("PhotoRootDir"), 
                Path.Combine(this.GetValidDirectoryName(album.PhotoCategory.Name), this.GetValidDirectoryName(album.Name)));

            this.Logger.Debug("Photos will be stored in the directory '{0}'", targetPath);

            if (!Directory.Exists(targetPath))
            {
                this.Logger.Debug("Creating directory '{0}'", targetPath);
                Directory.CreateDirectory(targetPath);
            }

            return targetPath;
        }

        /// <summary>Gets a valid directory and file path.</summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>The valid path.</returns>
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
