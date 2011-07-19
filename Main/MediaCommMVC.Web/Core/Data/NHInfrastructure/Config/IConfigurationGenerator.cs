using FluentNHibernate.Cfg;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Config
{
    public interface IConfigurationGenerator
    {
        FluentConfiguration Generate();
    }
}