#region Using Directives

using System;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;

using MediaCommMVC.Data.NHInfrastructure.Mapping;
using MediaCommMVC.Tests.TestImplementations;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

#endregion

namespace MediaCommMVC.DBSetup
{
    /// <summary>Generates the database schema.</summary>
    public class Program
    {
        #region Public Methods

        /// <summary>Generates a FluentConfiguration.</summary>
        /// <returns>The FluentConfiguration.</returns>
        public static FluentConfiguration Generate()
        {
            AutoPersistenceModel autoMapModel = new AutoMapGenerator(new ConsoleLogger()).Generate();

            Configuration configuration = new Configuration();
            configuration.Configure();
            
            FluentConfiguration config =
                Fluently.Configure(configuration).Mappings(m => m.AutoMappings.Add(autoMapModel))
                .ExposeConfiguration(BuildSchema);

            return config;
        }

        /// <summary>Enty point.</summary>
        /// <param name="args">The unused args.</param>
        public static void Main(string[] args)
        {
            FluentConfiguration config = Generate();

            try
            {
                Console.WriteLine("ALL DATA WILL BE LOST! If you really want to create the DB schema, enter 'y' ");

                string response = Console.ReadLine();

                if (response == "y")
                {
                    config.BuildSessionFactory();

                    Console.WriteLine("Successfully created DB Schema.");
                }
                else
                {
                    Console.WriteLine("Schema creation aborted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }

        #endregion

        #region Methods

        /// <summary>Builds the schema in the database file.</summary>
        /// <param name="config">The config.</param>
        private static void BuildSchema(Configuration config)
        {
            // Drops all tables
            new SchemaExport(config).Drop(true, true);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(true, true);
        }

        #endregion
    }
}
