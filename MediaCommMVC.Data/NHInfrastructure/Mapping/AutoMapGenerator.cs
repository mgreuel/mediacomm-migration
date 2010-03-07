﻿#region Using Directives

using System;

using FluentNHibernate.Automapping;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.Model.Forums;

#endregion

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    /// <summary>A Generator for the fluentNHibernate auto map.</summary>
    public class AutoMapGenerator : IAutoMapGenerator
    {
        #region Constants and Fields

        /// <summary>The logger.</summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AutoMapGenerator"/> class.</summary>
        /// <param name="logger">The logger.</param>
        public AutoMapGenerator(ILogger logger)
        {
            this.logger = logger;    
        }

        #endregion

        #region Implemented Interfaces

        #region IAutoMapGenerator

        /// <summary>Generates the FluentNHibernate automap.</summary>
        /// <returns>The auto persistence model.</returns>
        public AutoPersistenceModel Generate()
        {
            string namespaceToAdd = "MediaCommMVC.Core.Model";

            this.logger.Debug("Generating fluent NHibernate automap for the namespace '{0}'", namespaceToAdd);

            // The Forum type can be replaced by any other type in the core assembly
            var autoPersistenceModel = AutoMap.AssemblyOf<Forum>()
                .Where(t => t.Namespace.StartsWith(namespaceToAdd, StringComparison.Ordinal))
                .UseOverridesFromAssemblyOf<AutoMapGenerator>()
                .Conventions.AddFromAssemblyOf<AutoMapGenerator>();

            this.logger.Debug("Finished generating fluent NHibernate automap");

            return autoPersistenceModel;
        }

        #endregion

        #endregion
    }
}