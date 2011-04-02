namespace MediaCommMVC.Core.Data.Nh.Config
{
    using FluentNHibernate.Cfg;

    public interface IConfigurationGenerator
    {
        FluentConfiguration Generate();
    }
}