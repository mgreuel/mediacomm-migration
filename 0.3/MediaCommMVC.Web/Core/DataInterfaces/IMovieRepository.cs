using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Movies;

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    public interface IMovieRepository
    {
        void DeleteMovieWithId(int movieId);

        IEnumerable<MovieLanguage> GetAllLanguages();

        IEnumerable<Movie> GetAllMovies();

        IEnumerable<MovieQuality> GetAllQualities();

        MovieLanguage GetLanguageById(int id);

        MovieQuality GetQualityById(int id);

        void Save(Movie movie);
    }
}
