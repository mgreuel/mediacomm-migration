#region Using Directives

using FluentNHibernate.Cfg;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Config
{
    /// <summary>Common interface for FluentConfiguration generation.</summary>
    public interface IConfigurationGenerator
    {
        #region Public Methods

        /// <summary>Generates a FluentConfiguration.</summary>
        /// <returns>The FluentConfiguration.</returns>
        FluentConfiguration Generate();

        #endregion
    }
}