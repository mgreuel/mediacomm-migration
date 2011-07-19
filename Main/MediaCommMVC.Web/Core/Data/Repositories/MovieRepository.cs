using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Movies;
using MediaCommMVC.Web.Core.Model.Users;

using NHibernate;
using NHibernate.Linq;

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ISessionContainer sessionContainer;

        private readonly CurrentUserContainer currentUserContainer;

        public MovieRepository(ISessionContainer sessionContainer, CurrentUserContainer currentUserContainer)
        {
            this.sessionContainer = sessionContainer;
            this.currentUserContainer = currentUserContainer;
        }

        private ISession Session
        {
            get
            {
                return this.sessionContainer.CurrentSession;
            }
        }

        public void DeleteMovieWithId(int movieId)
        {
            Movie movie = this.Session.Get<Movie>(movieId);

            if (movie.Owner != this.currentUserContainer.User && this.currentUserContainer.User.IsAdmin)
            {
                throw new UnauthorizedAccessException("Only Administrator can delete movies added by other users");
            }

            this.Session.Delete(movie);
        }

        public IEnumerable<MovieLanguage> GetAllLanguages()
        {
            return this.Session.Query<MovieLanguage>().ToList();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return this.Session.Query<Movie>().ToList();
        }

        public IEnumerable<MovieQuality> GetAllQualities()
        {
            return this.Session.Query<MovieQuality>().ToList();
        }

        public MovieLanguage GetLanguageById(int id)
        {
            return this.Session.Get<MovieLanguage>(id);
        }

        public MovieQuality GetQualityById(int id)
        {
            return this.Session.Get<MovieQuality>(id);
        }

        public void Save(Movie movie)
        {
            this.Session.SaveOrUpdate(movie);
        }
    }
}