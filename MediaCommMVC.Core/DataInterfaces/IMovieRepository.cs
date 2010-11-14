#region Using Directives

using System.Collections.Generic;

using MediaCommMVC.Core.Model.Movies;

#endregion

namespace MediaCommMVC.Core.DataInterfaces
{
    /// <summary>
    ///   The interface for all movie repositories.
    /// </summary>
    public interface IMovieRepository
    {
        #region Public Methods

        /// <summary>
        ///   Deletes the movie with the id.
        /// </summary>
        /// <param name = "movieId">The movie id.</param>
        void DeleteMovieWithId(int movieId);

        /// <summary>
        ///   Gets the movie languages.
        /// </summary>
        /// <returns>The movie languages.</returns>
        IEnumerable<MovieLanguage> GetAllLanguages();

        /// <summary>
        ///   Gets all movies from the persistence store.
        /// </summary>
        /// <returns>The movies.</returns>
        IEnumerable<Movie> GetAllMovies();

        /// <summary>
        ///   Gets the movie qualities.
        /// </summary>
        /// <returns>The movie qualities.</returns>
        IEnumerable<MovieQuality> GetAllQualities();

        /// <summary>
        ///   Gets the language by id.
        /// </summary>
        /// <param name = "id">The language id.</param>
        /// <returns>The movie language.</returns>
        MovieLanguage GetLanguageById(int id);

        /// <summary>
        ///   Gets the quality by id.
        /// </summary>
        /// <param name = "id">The quality id.</param>
        /// <returns>The movie quality.</returns>
        MovieQuality GetQualityById(int id);

        /// <summary>
        ///   Adds the movie to the persistence store.
        /// </summary>
        /// <param name = "movie">The movie.</param>
        void Save(Movie movie);

        #endregion
    }
}
