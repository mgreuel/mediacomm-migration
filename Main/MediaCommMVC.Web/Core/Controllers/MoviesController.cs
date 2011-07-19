using System;
using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Movies;

namespace MediaCommMVC.Web.Core.Controllers
{
    using MediaCommMVC.Web.Core.Infrastructure;

    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ILogger logger;

        private readonly IMovieRepository movieRepository;

        private readonly IUserRepository userRepository;

        public MoviesController(IMovieRepository movieRepository, IUserRepository userRepository, ILogger logger)
        {
            this.movieRepository = movieRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NHibernateActionFilter]
        public ActionResult DeleteMovie(int id)
        {
            try
            {
                this.movieRepository.DeleteMovieWithId(id);

                return this.Json(new { success = true });
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error deleting movie with id {0}", id), ex);
                return this.Json(new { success = false });
            }
        }

        [NHibernateActionFilter]
        public ActionResult Index()
        {
            IEnumerable<Movie> movies = this.movieRepository.GetAllMovies();
            this.ViewData["movieLanguages"] = new SelectList(this.movieRepository.GetAllLanguages(), "Id", "Name");
            this.ViewData["movieQualities"] = new SelectList(this.movieRepository.GetAllQualities(), "Id", "Name");

            return View(movies);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [NHibernateActionFilter]
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
    }
}