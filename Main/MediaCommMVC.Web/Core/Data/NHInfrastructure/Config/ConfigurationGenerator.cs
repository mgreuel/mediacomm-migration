using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping;

using NHibernate.Cfg;

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

            Configuration configuration = new Configuration();

            configuration.SetProperty(
                Environment.ConnectionDriver, typeof(MiniProfilerContrib.NHibernate.ProfiledSql2008ClientDriver).AssemblyQualifiedName);

            configuration.SetProperty(Environment.CurrentSessionContextClass, "web");

            FluentConfiguration fluentConfiguration = Fluently.Configure(configuration).Database(
                MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("MediaCommDb")).AdoNetBatchSize(100))
                .Mappings(m => m.AutoMappings.Add(autoMapModel));
            
            return fluentConfiguration;
        }
    }
}