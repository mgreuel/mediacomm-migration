using System.Collections.Generic;
using System.Linq;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Movies;

using NHibernate.Linq;

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    public class MovieRepository : RepositoryBase, IMovieRepository
    {
        public MovieRepository(ISessionContainer sessionManager, IConfigAccessor configAccessor, ILogger logger)
            : base(sessionManager, configAccessor, logger)
        {
        }

        public void DeleteMovieWithId(int movieId)
        {
            Movie movie = this.Session.Get<Movie>(movieId);
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
            MovieQuality quality = this.Session.Get<MovieQuality>(id);
            return quality;
        }

        public void Save(Movie movie)
        {
            this.Session.SaveOrUpdate(movie);
        }
    }
}