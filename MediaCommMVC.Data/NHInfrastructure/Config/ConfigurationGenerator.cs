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

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ConfigurationGenerator"/> class.</summary>
        /// <param name="autoMapGenerator">The auto map generator.</param>
        /// <param name="logger">The logger.</param>
        public ConfigurationGenerator(IAutoMapGenerator autoMapGenerator, ILogger logger)
        {
            this.autoMapGenerator = autoMapGenerator;
        }

        #endregion

        #region Implemented Interfaces

        #region IConfigurationGenerator

        /// <summary>Generates a FluentConfiguration.</summary>
        /// <returns>The FluentConfiguration.</returns>
        public FluentConfiguration Generate()
        {
            Configuration config = CreateNHibernateConfiguration();
            AutoPersistenceModel autoMapModel = this.autoMapGenerator.Generate();
            FluentConfiguration fluentConfiguration = Fluently.Configure(config).Mappings(m => m.AutoMappings.Add(autoMapModel));

            return fluentConfiguration;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>Creates the NHibernate configuration.</summary>
        /// <returns>THe NHibernate configuration.</returns>
        private static Configuration CreateNHibernateConfiguration()
        {
            Configuration config = new Configuration();

            // Reads the configuration from hibernate.cfg.xml
            config.Configure();
            
            return config;
        }

        #endregion
    }
}