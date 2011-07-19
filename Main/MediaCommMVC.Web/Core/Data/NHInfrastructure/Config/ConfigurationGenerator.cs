using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping;

using NHibernate.ByteCode.Castle;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Config
{
    public sealed class ConfigurationGenerator : IConfigurationGenerator
    {
        private readonly IAutoMapGenerator autoMapGenerator;

        public ConfigurationGenerator(IAutoMapGenerator autoMapGenerator)
        {
            this.autoMapGenerator = autoMapGenerator;
        }

        public FluentConfiguration Generate()
        {
            AutoPersistenceModel autoMapModel = this.autoMapGenerator.Generate();

            FluentConfiguration fluentConfiguration = Fluently.Configure().Database(
                MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("MediaCommDb")).AdoNetBatchSize(100))
                .ProxyFactoryFactory(typeof(ProxyFactoryFactory)).CurrentSessionContext("web").Mappings(m => m.AutoMappings.Add(autoMapModel));

            return fluentConfiguration;
        }
    }
}