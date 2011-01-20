#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Movies;
using MediaCommMVC.Data.NHInfrastructure.Mapping;
using MediaCommMVC.Data.Repositories;
using MediaCommMVC.Tests.Common.TestImplementations;
using MediaCommMVC.Tests.TestImplementations;

using NHibernate;

using Configuration = NHibernate.Cfg.Configuration;

#endregion

namespace MediaCommMVC.DBSetup
{
    /// <summary>
    /// Generates the database schema.
    /// </summary>
    public class Program
    {
        #region Constants and Fields

        /// <summary>
        /// The default admin role name.
        /// </summary>
        private const string AdministratorRoleName = "Administrators";

        /// <summary>
        /// The default admin username.
        /// </summary>
        private const string AdministratorUsername = "admin";

        /// <summary>
        /// The default admin password.
        /// </summary>
        private const string DefaultAdminPassword = "changeME!";

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates a FluentConfiguration.
        /// </summary>
        /// <returns>
        /// The FluentConfiguration.
        /// </returns>
        public static FluentConfiguration Generate()
        {
            AutoPersistenceModel autoMapModel = new AutoMapGenerator(new ConsoleLogger()).Generate();

            Configuration configuration = new Configuration();
            configuration.Configure();

            FluentConfiguration config =
                Fluently.Configure(configuration).Mappings(m => m.AutoMappings.Add(autoMapModel));

            return config;
        }

        /// <summary>
        /// Enty point.
        /// </summary>
        /// <param name="args">
        /// The unused args.
        /// </param>
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

                    SessionManager sessionManager = new SessionManager(new TestConfigurationGenerator());
                    sessionManager.CreateNewSession();

                    IUserRepository userRepository = new UserRepository(
                        sessionManager, new FileConfigAccessor(new ConsoleLogger()), new ConsoleLogger());

                    userRepository.CreateAdmin(AdministratorUsername, DefaultAdminPassword, "admin@localhost");

                    ISession session = sessionManager.Session;

                    List<MovieQuality> movieQualities = new List<MovieQuality> {
                           new MovieQuality { Name = "DVD" }, new MovieQuality { Name = "BluRay" } 
                        };
                    movieQualities.ForEach(q => session.Save(q));

                    List<MovieLanguage> movieLanguages = new List<MovieLanguage> {
                           new MovieLanguage { Name = "EN" }, 
                          new MovieLanguage { Name = "DE" }, 
                          new MovieLanguage { Name = "ES" }, 
                          new MovieLanguage { Name = "FR" }
                        };
                    movieLanguages.ForEach(l => session.Save(l));

                    session.Flush();
                    session.Close();


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


        #endregion
    }
}