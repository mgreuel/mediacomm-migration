using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Testing;

using MbUnit.Framework;
using MediaCommMVC.Core.Model.Movies;
using MediaCommMVC.Core.Model.Photos;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Data.NHInfrastructure;
using MediaCommMVC.Tests.TestImplementations;

using NHibernate;

namespace MediaCommMVC.Tests.DataAccess
{
    [TestFixture]
    public class Mapping
    {
        private SessionManager sessionManager;

        [FixtureSetUp]
        public void InitSessionManager()
        {
            this.sessionManager = new SessionManager();
        }

        [FixtureTearDown]
        public void CleanUp()
        {
            this.sessionManager.Session.Close();
        }

        [SetUp]
        public void RecreateSession()
        {
            this.sessionManager.CreateNewSession();
        }

        [Test]
        public void CanMapMovieLanguage()
        {
            new PersistenceSpecification<MovieLanguage>(this.sessionManager.Session)
                .CheckProperty(ml => ml.Name, "someLanguage")
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapMovieQuality()
        {
            new PersistenceSpecification<MovieQuality>(this.sessionManager.Session)
                .CheckProperty(mq => mq.Name, "myMovieQuality")
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapMovie()
        {
            new PersistenceSpecification<Movie>(this.sessionManager.Session)
                .CheckProperty(m => m.InfoLink, "http://test.de")
                .CheckProperty(m => m.Language, new MovieLanguage { Name = "DE" })
                .CheckProperty(m => m.Quality, new MovieQuality { Name = "Quality123" })
                .CheckProperty(m => m.Title, "movieTitle")
                .CheckProperty(m => m.Owner, new MediaCommUser("someUser", "test@host.local"))
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapMediaCommUser()
        {
            new PersistenceSpecification<MediaCommUser>(this.sessionManager.Session)
                .CheckProperty(u => u.City, "Mülheim")
                .CheckProperty(u => u.DateOfBirth, DateTime.Today.Subtract(new TimeSpan(12345, 0, 0, 0)))
                .CheckProperty(u => u.FirstName, "my first name")
                .CheckProperty(u => u.IcqUin, "654-321")
                .CheckProperty(u => u.LastName, "my last name")
                .CheckProperty(u => u.LastVisit, DateTime.Today)
                .CheckProperty(u => u.MobilePhoneNumber, "01234 654987")
                .CheckProperty(u => u.PhoneNumber, "0234 654987")
                .CheckProperty(u => u.SkypeNick, "some skype nick")
                .CheckProperty(u => u.UserName, "mappingUser")
                .CheckProperty(u => u.ZipCode, "12345")
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapPhotoCategory()
        {
            new PersistenceSpecification<PhotoCategory>(this.sessionManager.Session)
                .CheckProperty(c => c.Name, "my map cat")
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapPhotoAlbum()
        {
            new PersistenceSpecification<PhotoAlbum>(this.sessionManager.Session)
                .CheckProperty(a => a.Category, new PhotoCategory { Name = "Album cat" })
                .CheckProperty(a => a.Name, "an album")
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapPhoto()
        {
            PhotoCategory photoCategory = new PhotoCategory { Name = "my cat" };

            new PersistenceSpecification<Photo>(this.sessionManager.Session)
                .CheckProperty(p => p.Album, new PhotoAlbum { Category = photoCategory, Name = "my album" })
                .CheckProperty(p => p.FileName, @"\path\mypic.jpg")
                .CheckProperty(p => p.FileSize, 12345L)
                .CheckProperty(p => p.Height, 1000)
                .CheckProperty(p => p.Uploader, new MediaCommUser("someUploader", "test@host.local"))
                .CheckProperty(p => p.Width, 2000)
                .VerifyTheMappings();
        }


    }
}
