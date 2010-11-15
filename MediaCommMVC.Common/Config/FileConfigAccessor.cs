#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using MediaCommMVC.Common.Logging;

#endregion

namespace MediaCommMVC.Common.Config
{
    /// <summary>Implements the IConfigAccessor using app/web.config files.</summary>
    public class FileConfigAccessor : IConfigAccessor
    {
        #region Constants and Fields

        /// <summary>The logger.</summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="FileConfigAccessor"/> class.</summary>
        /// <param name="logger">The logger.</param>
        public FileConfigAccessor(ILogger logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Implemented Interfaces

        #region IConfigAccessor

        /// <summary>Gets the config value from the configuration store.</summary>
        /// <param name="key">The config key.</param>
        /// <returns>The configuration value.</returns>
        public string GetConfigValue(string key)
        {
            this.logger.Debug("Getting configuration value for key '{0}'", key);

            string value = ConfigurationManager.AppSettings[key];

            if (value == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format("Configuration value with the key {0} does not exist.", key));
            }

            this.logger.Debug("Got '{0}' as configuration value for key '{1}'", value, key);

            return value;
        }

        /// <summary>Gets multiple config values.</summary>
        /// <param name="key">The config key.</param>
        /// <returns>The configuration values.</returns>
        public IEnumerable<string> GetConfigValues(string key)
        {
            this.logger.Debug("Getting configuration values for key '{0}'", key);

            IEnumerable<string> values = ConfigurationManager.AppSettings[key].Split(new[] { "#;" }, StringSplitOptions.RemoveEmptyEntries);

            if (values == null || values.Count() == 0)
            {
                throw new ConfigurationErrorsException(
                    string.Format("Configuration value with the key {0} does not exist.", key));
            }

            this.logger.Debug("Got '{0}' as configuration values for key '{1}'", ConfigurationManager.AppSettings[key], key);

            return values;
        }

        /// <summary>Adds the config value to the configuration store.</summary>
        /// <param name="key">The config key.</param>
        /// <param name="value">The config value.</param>
        public void SaveConfigValue(string key, string value)
        {
            this.logger.Debug("Saving configuration value. key: '{0} value: '{1}", key, value);

            ConfigurationManager.AppSettings[key] = value;
        }

        /// <summary>Adds multiple config values.</summary>
        /// <param name="key">The config key.</param>
        /// <param name="values">The config values.</param>
        public void SaveConfigValues(string key, IEnumerable<string> values)
        {
            if (values.Any(v => v.Contains("#;")))
            {
                throw new ArgumentException("Configuration values must not contain '#;'");
            }

            StringBuilder builder = new StringBuilder();
            values.ToList().ForEach(v => builder.Append(v + "#;"));

            this.logger.Debug("Saving configuration values. key: '{0}' value: '{1}'", key, builder.ToString());

            ConfigurationManager.AppSettings[key] = builder.ToString();
        }

        #endregion

        #endregion
    }
}