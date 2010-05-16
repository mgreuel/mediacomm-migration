#region Using Directives

using System.Collections.Generic;
using System.Linq;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Movies;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Data.NHInfrastructure;

using NHibernate;
using NHibernate.Linq;

#endregion

namespace MediaCommMVC.Data.Repositories
{
    /// <summary>Implements the IMovieRepository using NHibernate.</summary>
    public class MovieRepository : RepositoryBase, IMovieRepository
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="MovieRepository"/> class.</summary>
        /// <param name="sessionManager">The NHibernate session manager.</param>
        /// <param name="configAccessor">The config Accessor.</param>
        /// <param name="logger">The logger.</param>
        public MovieRepository(ISessionManager sessionManager, IConfigAccessor configAccessor, ILogger logger)
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
            this.Logger.Debug("Deleting movie with id: " + movieId);
         
            this.InvokeTransaction(delegate(ISession session)
                {
                    Movie movie = session.Get<Movie>(movieId);
                    
                    this.Logger.Debug("Deleting movie: " + movie);
                    session.Delete(movie);
                });

            this.Logger.Debug("Finished deleting movie");
        }

        /// <summary>Gets the movie languages.</summary>
        /// <returns>The movie languages.</returns>
        public IEnumerable<MovieLanguage> GetAllLanguages()
        {
            this.Logger.Debug("Getting all movie languages");

            IEnumerable<MovieLanguage> languages = this.Session.Linq<MovieLanguage>().ToList();

            this.Logger.Debug("Got {0} movie languages", languages.Count());

            return languages;
        }

        /// <summary>Gets all movies from the persistence store.</summary>
        /// <returns>The movies.</returns>
        public IEnumerable<Movie> GetAllMovies()
        {
            this.Logger.Debug("Getting all movies");

            IEnumerable<Movie> movies = this.Session.Linq<Movie>().ToList();

            this.Logger.Debug("Got {0} movies", movies.Count());

            return movies;
        }

        /// <summary>Gets the movie qualities.</summary>
        /// <returns>The movie qualities.</returns>
        public IEnumerable<MovieQuality> GetAllQualities()
        {
            this.Logger.Debug("Getting all movie qualities");

            IEnumerable<MovieQuality> qualities = this.Session.Linq<MovieQuality>().ToList();

            this.Logger.Debug("Got {0} movie qualities", qualities.Count());

            return qualities;
        }

        /// <summary>Gets the language by id.</summary>
        /// <param name="id">The language id.</param>
        /// <returns>The movie language.</returns>
        public MovieLanguage GetLanguageById(int id)
        {
            this.Logger.Debug("Getting movie language with the id: " + id);

            MovieLanguage language = this.Session.Get<MovieLanguage>(id);

            this.Logger.Debug("Got the movie language: " + language);

            return language;
        }

        /// <summary>Gets the quality by id.</summary>
        /// <param name="id">The quality id.</param>
        /// <returns>The movie quality.</returns>
        public MovieQuality GetQualityById(int id)
        {
            this.Logger.Debug("Getting movie quality with the id: " + id);

            MovieQuality quality = this.Session.Get<MovieQuality>(id);

            this.Logger.Debug("Got the movie quality: " + quality);

            return quality;
        }

        /// <summary>Adds the movie to the persistence store.</summary>
        /// <param name="movie">The movie.</param>
        public void Save(Movie movie)
        {
            this.Logger.Debug("Saving movie: " + movie);

            this.InvokeTransaction(s => s.SaveOrUpdate(movie));

            this.Logger.Debug("Finished saving movie");
        }

        #endregion

        #endregion
    }
}
