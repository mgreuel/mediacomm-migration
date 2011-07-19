using FluentNHibernate.Automapping;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public interface IAutoMapGenerator
    {
        AutoPersistenceModel Generate();
    }
}