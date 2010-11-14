#region Using Directives

using System;
using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Movies;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>
    ///   The movies controller.
    /// </summary>
    [Authorize]
    public class MoviesController : Controller
    {
        #region Constants and Fields

        /// <summary>
        ///   The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///   The movie repository.
        /// </summary>
        private readonly IMovieRepository movieRepository;

        /// <summary>
        ///   The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MoviesController" /> class.
        /// </summary>
        /// <param name = "movieRepository">The movie repository.</param>
        /// <param name = "userRepository">The user repository.</param>
        /// <param name = "logger">The logger.</param>
        public MoviesController(IMovieRepository movieRepository, IUserRepository userRepository, ILogger logger)
        {
            this.movieRepository = movieRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Deletes the movie.
        /// </summary>
        /// <param name = "id">The movie id.</param>
        /// <returns>Whether the deletion was successfull.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteMovie(int id)
        {
            this.logger.Debug("Deleting movie with id: " + id);
            try
            {
#warning check if allowed
                this.movieRepository.DeleteMovieWithId(id);

                return this.Json(new { success = true });
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error deleting movie with id {0}", id), ex);
                return this.Json(new { success = false });
            }
        }

        /// <summary>
        ///   Shows the movies index.
        /// </summary>
        /// <returns>The movies list view.</returns>
        public ActionResult Index()
        {
            IEnumerable<Movie> movies = this.movieRepository.GetAllMovies();
            this.ViewData["movieLanguages"] = new SelectList(this.movieRepository.GetAllLanguages(), "Id", "Name");
            this.ViewData["movieQualities"] = new SelectList(this.movieRepository.GetAllQualities(), "Id", "Name");

            return View(movies);
        }

        /// <summary>
        ///   Adds a new movie.
        /// </summary>
        /// <param name = "movie">The movie.</param>
        /// <param name = "languageId">The language id.</param>
        /// <param name = "qualityId">The quality id.</param>
        /// <returns>The movie index view.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(Movie movie, int languageId, int qualityId)
        {
            this.logger.Debug("Adding movie '{0}' with languageId {1} and qualityId {2}", movie, languageId, qualityId);

            movie.Language = this.movieRepository.GetLanguageById(languageId);
            movie.Quality = this.movieRepository.GetQualityById(qualityId);
            movie.Owner = this.userRepository.GetUserByName(this.User.Identity.Name);
            this.movieRepository.Save(movie);

            this.logger.Debug("Redirecting to movies index.");
            return this.RedirectToAction("Index");
        }

        #endregion
    }
}