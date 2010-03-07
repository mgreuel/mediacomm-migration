#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Data.NHInfrastructure.Mapping;

using NHibernate.Cfg;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Config
{
    /// <summary>Genrates the default FluentConfiguration.</summary>
    public class ConfigurationGenerator : IConfigurationGenerator
    {
        #region Constants and Fields

        /// <summary>Generator used for creating the auto map model.</summary>
        private readonly IAutoMapGenerator autoMapGenerator;

        /// <summary>The logger.</summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ConfigurationGenerator"/> class.</summary>
        /// <param name="autoMapGenerator">The auto map generator.</param>
        /// <param name="logger">The logger.</param>
        public ConfigurationGenerator(IAutoMapGenerator autoMapGenerator, ILogger logger)
        {
            this.autoMapGenerator = autoMapGenerator;
            this.logger = logger;
        }

        #endregion

        #region Implemented Interfaces

        #region IConfigurationGenerator

        /// <summary>Generates a FluentConfiguration.</summary>
        /// <returns>The FluentConfiguration.</returns>
        public FluentConfiguration Generate()
        {
            this.logger.Debug("Generating fluentNHibernate configuration");

            Configuration config = this.CreateNHibernateConfiguration();

            AutoPersistenceModel autoMapModel = this.autoMapGenerator.Generate();

            FluentConfiguration fluentConfiguration = Fluently.Configure(config).Mappings(m => m.AutoMappings.Add(autoMapModel));

            this.logger.Debug("Finished generating fluentNHibernate configuration");

            return fluentConfiguration;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>Creates the NHibernate configuration.</summary>
        /// <returns>THe NHibernate configuration.</returns>
        private Configuration CreateNHibernateConfiguration()
        {
            this.logger.Debug("Creating NHiberante configuration from standard xml file.");

            Configuration config = new Configuration();

            // Reads the configuration from hibernate.cfg.xml
            config.Configure();

            this.logger.Debug("Finished creating NHibernate configuration.");
            
            return config;
        }

        #endregion
    }
}