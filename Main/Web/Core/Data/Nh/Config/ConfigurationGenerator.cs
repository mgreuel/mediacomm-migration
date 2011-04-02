namespace MediaCommMVC.Core.Data.Nh.Config
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using MediaCommMVC.Core.Data.Nh.Mapping;

    using NHibernate.ByteCode.Castle;

    #endregion

    public class ConfigurationGenerator : IConfigurationGenerator
    {
        #region Constants and Fields

        private readonly IAutoMapGenerator autoMapGenerator;

        #endregion

        #region Constructors and Destructors

        public ConfigurationGenerator(IAutoMapGenerator autoMapGenerator)
        {
            this.autoMapGenerator = autoMapGenerator;
        }

        #endregion

        #region Implemented Interfaces

        #region IConfigurationGenerator

        public FluentConfiguration Generate()
        {
            AutoPersistenceModel autoMapModel = this.autoMapGenerator.Generate();

            FluentConfiguration fluentConfiguration =
                Fluently.Configure()
                    .Database(
                        MsSqlConfiguration.MsSql2008
                        .ConnectionString(c => c.FromConnectionStringWithKey("MediaCommDb"))
                        .ProxyFactoryFactory<ProxyFactoryFactory>()
                        .AdoNetBatchSize(100).CurrentSessionContext("web"))
                    .Mappings(m => m.AutoMappings.Add(autoMapModel));

            return fluentConfiguration;
        }

        #endregion

        #endregion
    }
}