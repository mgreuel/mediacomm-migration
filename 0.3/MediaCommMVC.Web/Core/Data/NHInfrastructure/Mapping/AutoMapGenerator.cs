using System;

using FluentNHibernate.Automapping;

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class AutoMapGenerator : IAutoMapGenerator
    {
        public AutoPersistenceModel Generate()
        {
            const string NamespaceToAdd = "MediaCommMVC.Web.Core.Model";

            AutoPersistenceModel autoPersistenceModel =
                AutoMap.AssemblyOf<AutoMapGenerator>().Where(
                    t => t.Namespace != null && t.Namespace.StartsWith(NamespaceToAdd, StringComparison.Ordinal)).UseOverridesFromAssemblyOf
                    <AutoMapGenerator>().Conventions.AddFromAssemblyOf<AutoMapGenerator>();

            return autoPersistenceModel;
        }
    }
}