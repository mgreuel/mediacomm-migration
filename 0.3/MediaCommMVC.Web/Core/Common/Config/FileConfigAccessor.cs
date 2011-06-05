#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using MediaCommMVC.Web.Core.Common.Logging;

#endregion

namespace MediaCommMVC.Web.Core.Common.Config
{
    public class FileConfigAccessor : IConfigAccessor
    {
        #region Constants and Fields

        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        public FileConfigAccessor(ILogger logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Implemented Interfaces

        #region IConfigAccessor

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

        public void SaveConfigValue(string key, string value)
        {
            throw new NotSupportedException();
        }

        public void SaveConfigValues(string key, IEnumerable<string> values)
        {
            throw new NotSupportedException();
        }

        #endregion

        #endregion
    }
}