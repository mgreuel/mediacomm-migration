#region Using Directives

using System;
using System.Collections.Generic;
using System.Web.Security;

using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Movies;
using MediaCommMVC.Data.NHInfrastructure.Mapping;
using MediaCommMVC.Data.Repositories;
using MediaCommMVC.Tests.TestImplementations;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

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
                Fluently.Configure(configuration).Mappings(m => m.AutoMappings.Add(autoMapModel)).ExposeConfiguration(
                    BuildSchema);

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

                    SessionManager sessionManager = new SessionManager();
                    sessionManager.CreateNewSession();

                    if (!Roles.RoleExists(AdministratorRoleName))
                    {
                        Roles.CreateRole(AdministratorRoleName);
                    }

                    IUserRepository userRepository = new UserRepository(
                        sessionManager, new FileConfigAccessor(new ConsoleLogger()), new ConsoleLogger());

                    try
                    {
                        userRepository.CreateUser(AdministratorUsername, DefaultAdminPassword, "admin@localhost");
                    }
                    catch (MembershipCreateUserException createUserException)
                    {
                        if (!createUserException.Message.Equals("The username is already in use."))
                        {
                            throw;
                        }
                    }

                    ISession session = sessionManager.Session;

                    List<MovieQuality> movieQualities = new List<MovieQuality>
                        {
                           new MovieQuality { Name = "DVD" }, new MovieQuality { Name = "BluRay" } 
                        };
                    movieQualities.ForEach(q => session.Save(q));

                    List<MovieLanguage> movieLanguages = new List<MovieLanguage>
                        {
                           new MovieLanguage { Name = "EN" }, 
                          new MovieLanguage { Name = "DE" }, 
                          new MovieLanguage { Name = "ES" }, 
                          new MovieLanguage { Name = "FR" }
                        };
                    movieLanguages.ForEach(l => session.Save(l));

                    session.Flush();
                    session.Close();

                    if (!Roles.IsUserInRole(AdministratorUsername, AdministratorRoleName))
                    {
                        Roles.AddUserToRole(AdministratorUsername, AdministratorRoleName);
                    }

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

        /// <summary>
        /// Builds the schema in the database file.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
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