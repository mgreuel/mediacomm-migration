#region Using Directives

using FluentNHibernate.Automapping;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>A common interface for FluentNHibernate AutoMaps.</summary>
    public interface IAutoMapGenerator
    {
        #region Public Methods

        /// <summary>Generates the FluentHibernate automap.</summary>
        /// <returns>The auto persistence model.</returns>
        AutoPersistenceModel Generate();

        #endregion
    }
}