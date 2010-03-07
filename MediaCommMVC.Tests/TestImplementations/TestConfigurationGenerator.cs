#region Using Directives

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;

using MediaCommMVC.Data.NHInfrastructure.Config;
using MediaCommMVC.Data.NHInfrastructure.Mapping;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

#endregion

namespace MediaCommMVC.Tests.TestImplementations
{
    /// <summary>Generates a FluentCOnfiguration for testing.</summary>
    public class TestConfigurationGenerator : IConfigurationGenerator
    {
        #region Implemented Interfaces

        #region IConfigurationGenerator

        /// <summary>Generates a FluentConfiguration.</summary>
        /// <returns>The FluentConfiguration.</returns>
        public FluentConfiguration Generate()
        {
            AutoPersistenceModel autoMapModel = new AutoMapGenerator(new ConsoleLogger()).Generate();

            Configuration configuration = new Configuration();
            configuration.Configure();
            configuration.Properties["current_session_context_class"] = "thread";

            FluentConfiguration config =
                Fluently.Configure(configuration)
                .Mappings(m => m.AutoMappings.Add(autoMapModel))
                .ExposeConfiguration(BuildSchema);
            
            return config;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>Builds the schema in the database file.</summary>
        /// <param name="config">The config.</param>
        private static void BuildSchema(Configuration config)
        {
            // Drops all tables
            new SchemaExport(config).Drop(true, true);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(true, true);
        }

        #endregion
    }
}
