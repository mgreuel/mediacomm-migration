namespace MediaCommMVC.Core.Data.Nh.Mapping
{
    using FluentNHibernate.Automapping;

    public interface IAutoMapGenerator
    {
        AutoPersistenceModel Generate();
    }
}