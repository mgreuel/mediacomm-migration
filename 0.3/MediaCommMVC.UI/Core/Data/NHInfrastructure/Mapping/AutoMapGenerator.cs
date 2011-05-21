#region Using Directives

using System;

using FluentNHibernate.Automapping;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping
{
    public class AutoMapGenerator : IAutoMapGenerator
    {
        #region Constants and Fields

        #endregion

        #region Constructors and Destructors

        public AutoMapGenerator()
        {
        }

        #endregion

        #region Implemented Interfaces

        #region IAutoMapGenerator

        public AutoPersistenceModel Generate()
        {
            const string NamespaceToAdd = "MediaCommMVC.Web.Core.Model";

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