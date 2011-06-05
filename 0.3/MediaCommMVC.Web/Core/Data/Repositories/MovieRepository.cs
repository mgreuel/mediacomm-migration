#region Using Directives

using System.Collections.Generic;
using System.Linq;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data.NHInfrastructure;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Movies;

using NHibernate;
using NHibernate.Linq;

using Enumerable = System.Linq.Enumerable;

#endregion

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    using MediaCommMVC.Web.Core.Infrastructure;

    /// <summary>Implements the IMovieRepository using NHibernate.</summary>
    public class MovieRepository : RepositoryBase, IMovieRepository
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="MovieRepository"/> class.</summary>
        /// <param name="sessionManager">The NHibernate session manager.</param>
        /// <param name="configAccessor">The config Accessor.</param>
        /// <param name="logger">The logger.</param>
        public MovieRepository(ISessionContainer sessionManager, IConfigAccessor configAccessor, ILogger logger)
            : base(sessionManager, configAccessor, logger)
        {
        }

        #endregion

        #region Implemented Interfaces

        #region IMovieRepository

        /// <summary>Deletes the movie with the id.</summary>
        /// <param name="movieId">The movie id.</param>
        public void DeleteMovieWithId(int movieId)
        {
            Movie movie = this.Session.Get<Movie>(movieId);

            this.Logger.Debug("Deleting movie: " + movie);
            this.Session.Delete(movie);


            this.Logger.Debug("Finished deleting movie");
        }

        public IEnumerable<MovieLanguage> GetAllLanguages()
        {
            this.Logger.Debug("Getting all movie languages");

            IEnumerable<MovieLanguage> languages = this.Session.Query<MovieLanguage>().ToList();

            this.Logger.Debug("Got {0} movie languages", languages.Count());

            return languages;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            this.Logger.Debug("Getting all movies");

            IEnumerable<Movie> movies = this.Session.Query<Movie>().ToList();

            this.Logger.Debug("Got {0} movies", movies.Count());

            return movies;
        }

        public IEnumerable<MovieQuality> GetAllQualities()
        {
            IEnumerable<MovieQuality> qualities = this.Session.Query<MovieQuality>().ToList();
            return qualities;
        }

        public MovieLanguage GetLanguageById(int id)
        {
            MovieLanguage language = this.Session.Get<MovieLanguage>(id);
            return language;
        }

        public MovieQuality GetQualityById(int id)
        {
            MovieQuality quality = this.Session.Get<MovieQuality>(id);
            return quality;
        }

        /// <summary>Adds the movie to the persistence store.</summary>
        /// <param name="movie">The movie.</param>
        public void Save(Movie movie)
        {
            this.Session.SaveOrUpdate(movie);
        }

        #endregion

        #endregion
    }
}
