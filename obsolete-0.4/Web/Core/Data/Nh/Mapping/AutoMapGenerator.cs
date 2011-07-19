namespace MediaCommMVC.Core.Data.Nh.Mapping
{
    #region Using Directives

    using System;

    using FluentNHibernate.Automapping;

    #endregion

    public class AutoMapGenerator : IAutoMapGenerator
    {
        #region Implemented Interfaces

        #region IAutoMapGenerator

        public AutoPersistenceModel Generate()
        {
            const string NamespaceToAdd = "MediaCommMVC.Core.Model";

            AutoPersistenceModel autoPersistenceModel =
                AutoMap.AssemblyOf<AutoMapGenerator>().Where(
                    t => t.Namespace != null && t.Namespace.StartsWith(NamespaceToAdd, StringComparison.Ordinal)).UseOverridesFromAssemblyOf
                    <AutoMapGenerator>().Conventions.AddFromAssemblyOf<AutoMapGenerator>();

            return autoPersistenceModel;
        }

        #endregion

        #endregion
    }
}