#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Movies;

#endregion

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class MovieMapper : IAutoMappingOverride<Movie>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Movie>

        public void Override(AutoMapping<Movie> mapping)
        {
            mapping.Table("Movies");
            mapping.Map(m => m.Title).Not.Nullable();
            mapping.References(m => m.Language).Not.LazyLoad().Not.Nullable().Cascade.SaveUpdate();
            mapping.References(m => m.Quality).Not.LazyLoad().Not.Nullable().Cascade.SaveUpdate();
            mapping.References(m => m.Owner).Not.LazyLoad().Not.Nullable().Cascade.SaveUpdate();
        }

        #endregion

        #endregion
    }
}