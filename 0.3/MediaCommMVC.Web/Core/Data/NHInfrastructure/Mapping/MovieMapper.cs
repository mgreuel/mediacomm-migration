#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Web.Core.Model.Movies;

#endregion

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    /// <summary>Makes customizations to the auto mapping of the Movie type.</summary>
    public class MovieMapper : IAutoMappingOverride<Movie>
    {
        #region Implemented Interfaces

        #region IAutoMappingOverride<Movie>

        /// <summary>Overrides the specified mapping.</summary>
        /// <param name="mapping">The Forum auto mapping.</param>
        public void Override(AutoMapping<Movie> mapping)
        {
            mapping.Table("Movies");
            mapping.Map(m => m.Title).Not.Nullable();
            mapping.References(m => m.Language).Not.Nullable().Cascade.SaveUpdate();
            mapping.References(m => m.Quality).Not.Nullable().Cascade.SaveUpdate();
            mapping.References(m => m.Owner).Not.Nullable().Cascade.SaveUpdate();
        }

        #endregion

        #endregion
    }
}