#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using ICSharpCode.SharpZipLib.Zip;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Photos;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Data.NHInfrastructure;

using NHibernate;
using NHibernate.Linq;

#endregion

namespace MediaCommMVC.Data.Repositories
{
    /// <summary>Implements the IPhotoRepository using NHibernate.</summary>
    public class PhotoRepository : RepositoryBase, IPhotoRepository
    {
        #region Constants and Fields

        /// <summary>Folder containing unprocessed files.</summary>
        private static string unprocessedPhotosFolder = "unprocessed";

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="PhotoRepository"/> class.</summary>
        /// <param name="sessionManager">The session manager.</param>
        /// <param name="configAccessor">The config Accessor.</param>
        /// <param name="logger">The logger.</param>
        public PhotoRepository(ISessionManager sessionManager, IConfigAccessor configAccessor, ILogger logger)
            : base(sessionManager, configAccessor, logger)
        {
        }

        #endregion

        #region Implemented Interfaces

        #region IPhotoRepository

        /// <summary>Adds the album to the persistence layer.</summary>
        /// <param name="album">The album.</param>
        public void AddAlbum(PhotoAlbum album)
        {
            this.Logger.Debug("Adding photo album: " + album);

            this.InvokeTransaction(s => s.SaveOrUpdate(album));

            this.Logger.Debug("Finished adding photo album");
        }

        /// <summary>Adds the category to the persistence layer.</summary>
        /// <param name="category">The category.</param>
        public void AddCategory(PhotoCategory category)
        {
            this.Logger.Debug("Adding photo category: " + category);

            this.InvokeTransaction(s => s.SaveOrUpdate(category));

            this.Logger.Debug("Finished adding photo category");
        }

        /// <summary>Adds the photo to the persistence layer.</summary>
        /// <param name="photo">The photo.</param>
        public void AddPhoto(Photo photo)
        {
            this.Logger.Debug("Adding photo: " + photo);

            this.InvokeTransaction(s => s.Save(photo));

            this.Logger.Debug("Finished adding photo");
        }

        /// <summary>Extracts and deletes the zip file.</summary>
        /// <param name="zipFilename">The zip filename.</param>
        /// <param name="album">The album.</param>
        /// <param name="uploader">The uploader.</param>
        public void ExtractAndAddPhotos(string zipFilename, PhotoAlbum album, MediaCommUser uploader)
        {
#warning encapsulate
            this.Logger.Debug("Extracting and adding photos. ZipFilename: '{0}', Album: '{1}, Uploader: '{2}'", zipFilename, album, uploader);

            string targetPath = this.GetTargetPath(album);
            string supportedPhotoExt = this.ConfigAccessor.GetConfigValue("SupportedPhotoExt");
            string unprocessedPath = Path.Combine(targetPath, unprocessedPhotosFolder);

            this.ExtractAndDeleteZip(zipFilename, targetPath, supportedPhotoExt);
            this.AddPicturesToDB(targetPath, album, uploader);

            IEnumerable<string> files = Directory.GetFiles(unprocessedPath);

            this.Logger.Debug("Moving {2} photos from '{0}' to '{1}'", unprocessedPath, targetPath, files.Count());

            foreach (string file in files)
            {
                string newPath = file.Replace("\\" + unprocessedPhotosFolder, string.Empty);
                try
                {
                    File.Copy(file, newPath);
                }
                catch (IOException ex)
                {
                    this.Logger.Error(string.Format("Unable to copy file '{0}' to '{1}'", file, newPath), ex);
                }
            }

            this.GenerateSmallerPhotos(targetPath);

            this.Logger.Debug("Finished extracting and adding photos");
        }

        /// <summary>Gets the album by id.</summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>The album.</returns>
        public PhotoAlbum GetAlbumById(int albumId)
        {
            this.Logger.Debug("Getting album with id '{0}'", albumId);

            PhotoAlbum album = this.Session.Linq<PhotoAlbum>().Single(a => a.Id.Equals(albumId));

            this.Logger.Debug("Got album: " + album);

            return album;
        }

        /// <summary>Gets all categories.</summary>
        /// <returns>All photo categories.</returns>
        public IEnumerable<PhotoCategory> GetAllCategories()
        {
            this.Logger.Debug("Getting all photo catgories");

            IEnumerable<PhotoCategory> categories = this.Session.Linq<PhotoCategory>();

            this.Logger.Debug("Got {0} photo categories", categories.Count());
            return categories;
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
                    Path.Combine(this.GetValidDirectoryName(photo.Album.Category.Name), Path.Combine(this.GetValidDirectoryName(photo.Album.Name), fileName)));

            this.Logger.Debug("Getting image from '{0}'", imagePath);
            Image image = Image.FromFile(imagePath);

            this.Logger.Debug("Finished getting image");
            return image;
        }

        /// <summary>Gets the photo by ID.</summary>
        /// <param name="id">The photo id.</param>
        /// <returns>The photo.</returns>
        public Photo GetPhotoById(int id)
        {
            this.Logger.Debug("Getting photo with id '{0}'", id);
            Photo photo = this.Session.Get<Photo>(id);

            this.Logger.Debug("Finished getting photo");
            return photo;
        }

        /// <summary>Gets all photos in the album.</summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>The photos.</returns>
        public IEnumerable<Photo> GetPhotosForAlbum(int albumId)
        {
            this.Logger.Debug("Getting photos for album with id '{0}'", albumId);
            IEnumerable<Photo> photos = this.Session.Linq<Photo>().Where(p => p.Album.Id.Equals(albumId)).ToList();

            this.Logger.Debug("Got {0} photos", photos.Count());
            return photos;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>Adds the pictures to DB.</summary>
        /// <param name="path">The target path.</param>
        /// <param name="album">The album.</param>
        /// <param name="uploader">The uploader.</param>
        private void AddPicturesToDB(string path, PhotoAlbum album, MediaCommUser uploader)
        {
            this.Logger.Debug("Adding photos from the folder '{0} to the database. Album: '{1}', Uploader: '{2}'", path, album, uploader);

            string unprocessedFullPath = Path.Combine(path, unprocessedPhotosFolder);
            DirectoryInfo dir = new DirectoryInfo(unprocessedFullPath);

            this.InvokeTransaction(delegate(ISession session)
                {
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        Bitmap bmp = new Bitmap(file.FullName);
                        string fileName = file.Name;

                        while (File.Exists(Path.Combine(path, fileName)))
                        {
                            string newFileName = fileName + DateTime.Now.Millisecond;

                            this.Logger.Debug("File '{0}' exists already, renaming it to '{1}'", Path.Combine(path, fileName), newFileName);
                            File.Move(
                                Path.Combine(unprocessedFullPath, fileName), Path.Combine(unprocessedFullPath, newFileName));
                        }

                        Photo photo = new Photo
                            {
                                Album = album, 
                                FileName = fileName, 
                                FileSize = file.Length, 
                                Height = bmp.Height, 
                                Uploader = uploader, 
                                Width = bmp.Width
                            };

                        this.Logger.Debug("Adding photo '{0}' to the database");
                        session.Save(photo);
                    }
                });

            this.Logger.Debug("Finished adding photos to the database.");
        }

        /// <summary>Extracts deletes the zip file.</summary>
        /// <param name="filename">The filename.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param example=".jpg|.jpeg" name="extensionsToExtract">The extensions to extract.</param>
        private void ExtractAndDeleteZip(string filename, string targetPath, string extensionsToExtract)
        {
            this.Logger.Debug("Extracting files with the extensions '{0}' from '{1}' to '{2}'", extensionsToExtract, filename, targetPath);

            FastZip zip = new FastZip();
            string unprocessedTargetPath = Path.Combine(targetPath, unprocessedPhotosFolder);
            zip.ExtractZip(filename, unprocessedTargetPath, extensionsToExtract);

            this.Logger.Debug("Deleting file '{0}'", filename);
            File.Delete(filename);

            this.Logger.Debug("Finished extracting and deleting zip file");
        }

        /// <summary>Generates the smaller photos.</summary>
        /// <param name="pathToPhotos">The path to photos.</param>
        private void GenerateSmallerPhotos(string pathToPhotos)
        {
            this.Logger.Debug("Generating smaller resolutions for photos in '{0}'", pathToPhotos);

            string sourcePath = Path.Combine(pathToPhotos, unprocessedPhotosFolder);

            sourcePath = string.Format("{0}\\*", sourcePath.TrimEnd('\\'));
            pathToPhotos = string.Format("{0}\\", pathToPhotos.TrimEnd('\\'));

            string param = sourcePath + " " + pathToPhotos;

            string photoCreatorBatchPath = this.ConfigAccessor.GetConfigValue("PathPhotoCreatorBatch");

            this.Logger.Debug("Executing '{0}' with parameters '{1}'", photoCreatorBatchPath, param);

            Process p = Process.Start(photoCreatorBatchPath, param);
        }

        /// <summary>Gets the path where the photos will be stored.</summary>
        /// <param name="album">The album.</param>
        /// <returns>The target path.</returns>
        private string GetTargetPath(PhotoAlbum album)
        {
            this.Logger.Debug("Getting file system path for album: " + album);

            string targetPath = Path.Combine(
                this.ConfigAccessor.GetConfigValue("PhotoRootDir"), 
                Path.Combine(this.GetValidDirectoryName(album.Category.Name), this.GetValidDirectoryName(album.Name)));

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

            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            invalidChars += " ";
            string invalidReStr = string.Format(@"[{0}]", invalidChars);
            string validName = Regex.Replace(directoryName, invalidReStr, "_");

            this.Logger.Debug("Got '{0}' as valid directory name", validName);

            return validName;
        }

        #endregion
    }
}
