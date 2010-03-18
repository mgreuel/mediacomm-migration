using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Testing;

using MbUnit.Framework;
using MediaCommMVC.Core.Model.Movies;
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

        [SetUp]
        public void Setup()
        {
            this.sessionManager = new SessionManager();
        }

        [Test]
        public void CanMapMovie()
        {
            this.sessionManager.CreateNewSession();
            new PersistenceSpecification<Movie>(this.sessionManager.Session)
                .CheckProperty(m => m.Id, 1)
                .CheckProperty(m => m.InfoLink, "http://test.de")
                .CheckProperty(m => m.Language, new MovieLanguage { Name = "DE" })
                .CheckProperty(m => m.Quality, new MovieQuality { Name = "Quality123" })
                .CheckProperty(m => m.Title, "movieTitle")
                .CheckProperty(m => m.Owner, new MediaCommUser("someUser", "test@host.local"))
                .VerifyTheMappings();
        }
    }
}
