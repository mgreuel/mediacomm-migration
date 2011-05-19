#region Using Directives

using System.Collections.Generic;

#endregion

namespace MediaCommMVC.Web.Core.Common.Config
{
    /// <summary>Common interface for retrieving configuration settings.</summary>
    public interface IConfigAccessor
    {
        #region Public Methods

        /// <summary>Gets the config value from the configuration store.</summary>
        /// <param name="key">The config key.</param>
        /// <returns>The configuration value.</returns>
        string GetConfigValue(string key);

        /// <summary>Gets multiple config values.</summary>
        /// <param name="key">The config key.</param>
        /// <returns>The configuration values.</returns>
        IEnumerable<string> GetConfigValues(string key);

        /// <summary>Adds the config value to the configuration store.</summary>
        /// <param name="key">The config key.</param>
        /// <param name="value">The config value.</param>
        void SaveConfigValue(string key, string value);

        /// <summary>Adds multiple config values.</summary>
        /// <param name="key">The config key.</param>
        /// <param name="values">The config values.</param>
        void SaveConfigValues(string key, IEnumerable<string> values);

        #endregion
    }
}