using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;

using MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping;

using NHibernate.Cfg;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Config
{
    public class ConfigurationGenerator : IConfigurationGenerator
    {
        private readonly IAutoMapGenerator autoMapGenerator;

        public ConfigurationGenerator(IAutoMapGenerator autoMapGenerator)
        {
            this.autoMapGenerator = autoMapGenerator;
        }

        public FluentConfiguration Generate()
        {
            Configuration config = CreateNHibernateConfiguration();
            AutoPersistenceModel autoMapModel = this.autoMapGenerator.Generate();
            FluentConfiguration fluentConfiguration = Fluently.Configure(config).Mappings(m => m.AutoMappings.Add(autoMapModel));

            return fluentConfiguration;
        }

        private static Configuration CreateNHibernateConfiguration()
        {
            Configuration config = new Configuration();

            // Reads the configuration from hibernate.cfg.xml
            config.Configure();

            return config;
        }
    }
}