using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Users;

using NHibernate;
using NHibernate.Linq;

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private const string UnprocessedPhotosFolder = "unprocessed";

        private readonly IConfigAccessor configAccessor;

        private readonly IImageGenerator imageGenerator;

        private readonly ILogger logger;

        private readonly ISessionContainer sessionContainer;

        public PhotoRepository(ISessionContainer sessionContainer, IImageGenerator imageGenerator, IConfigAccessor configAccessor, ILogger logger)
        {
            this.sessionContainer = sessionContainer;
            this.imageGenerator = imageGenerator;
            this.configAccessor = configAccessor;
            this.logger = logger;
        }

        protected ISession Session
        {
            get
            {
                return this.sessionContainer.CurrentSession;
            }
        }

        public void AddCategory(PhotoCategory category)
        {
            this.Session.SaveOrUpdate(category);
        }

        public void AddPhotos(PhotoAlbum album, MediaCommUser uploader)
        {
            string targetPath = this.GetTargetPath(album);
            string unprocessedPath = Path.Combine(targetPath, UnprocessedPhotosFolder);

            IEnumerable<FileInfo> newFiles = this.MovePhotos(targetPath, unprocessedPath);

            this.AddPicturesToDB(newFiles, album, uploader);

            this.imageGenerator.GenerateImages(targetPath, UnprocessedPhotosFolder);
        }

        public IEnumerable<PhotoAlbum> Get4NewestAlbums()
        {
            return this.Session.Query<PhotoAlbum>().OrderByDescending(a => a.LastPicturesAdded).Take(4).ToList();
        }

        public PhotoAlbum GetAlbumById(int albumId)
        {
            return this.Session.Query<PhotoAlbum>().FetchMany(a => a.Photos).Single(a => a.Id == albumId);
        }

        public IEnumerable<PhotoAlbum> GetAlbumsForCategoryIdStartingWith(int catId, string term)
        {
            return this.Session.Query<PhotoAlbum>().Where(a => a.PhotoCategory.Id == catId && a.Name.StartsWith(term)).ToList();
        }

        public IEnumerable<PhotoCategory> GetAllCategories()
        {
            return this.Session.Query<PhotoCategory>().ToList();
        }

        public PhotoCategory GetCategoryById(int id)
        {
            return this.Session.Query<PhotoCategory>().FetchMany(c => c.Albums).Single(c => c.Id == id);
        }

        public Image GetImage(int photoId, string size)
        {
            Photo photo = this.GetPhotoById(photoId);

            string fileName = photo.FileName.Insert(photo.FileName.LastIndexOf("."), size);

            string imagePath = Path.Combine(
                this.configAccessor.GetConfigValue("PhotoRootDir"), 
                Path.Combine(
                    this.GetValidDirectoryName(photo.PhotoAlbum.PhotoCategory.Name), 
                    Path.Combine(this.GetValidDirectoryName(photo.PhotoAlbum.Name), fileName)));

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
            return this.Session.Get<Photo>(id);
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
            PhotoAlbum photoAlbum = this.Session.Query<PhotoAlbum>().SingleOrDefault(a => a.Name.Equals(album.Name)) ?? album;

            photoAlbum.LastPicturesAdded = DateTime.Now;

            foreach (FileInfo file in filesToAdd)
            {
                Bitmap bmp = new Bitmap(file.FullName);
                int height = bmp.Height;
                int width = bmp.Width;
                bmp.Dispose();

                Photo photo = new Photo
                    {
                       PhotoAlbum = photoAlbum, FileName = file.Name, FileSize = file.Length, Height = height, Uploader = uploader, Width = width 
                    };

                this.Session.Save(photo);
            }
        }

        private string GetTargetPath(PhotoAlbum album)
        {
            string targetPath = Path.Combine(
                this.configAccessor.GetConfigValue("PhotoRootDir"), 
                Path.Combine(this.GetValidDirectoryName(album.PhotoCategory.Name), this.GetValidDirectoryName(album.Name)));

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            return targetPath;
        }

        private string GetValidDirectoryName(string directoryName)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).Distinct().ToArray();
            string invalidCharsRegexString = string.Format(@"[{0}]", Regex.Escape(new string(invalidChars) + " $.§ß%^&;=,'^´`#"));
            string validName = Regex.Replace(directoryName, invalidCharsRegexString, "_");

            return validName;
        }

        private IEnumerable<FileInfo> MovePhotos(string targetPath, string unprocessedPath)
        {
            IEnumerable<FileInfo> allFiles = GetFilesRecursive(new DirectoryInfo(unprocessedPath)).ToList();

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
                    this.logger.Error(string.Format("Unable to copy file '{0}' to '{1}'", file, newPath), ex);
                }
            }

            return newFiles;
        }
    }
}