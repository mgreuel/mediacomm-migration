#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Exceptions;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Data.NHInfrastructure;

using NHibernate.Linq;

#endregion

namespace MediaCommMVC.Data.Repositories
{
    /// <summary>
    ///   Implements the IUserRepository using nHibernate.
    /// </summary>
    public class UserRepository : RepositoryBase, IUserRepository
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UserRepository" /> class.
        /// </summary>
        /// <param name = "sessionManager">The NHibernate session manager.</param>
        /// <param name = "configAccessor">The config Accessor.</param>
        /// <param name = "logger">The logger.</param>
        public UserRepository(ISessionManager sessionManager, IConfigAccessor configAccessor, ILogger logger)
            : base(sessionManager, configAccessor, logger)
        {
        }

        #endregion

        #region Implemented Interfaces

        #region IUserRepository

        /// <summary>
        ///   Creates a new user.
        /// </summary>
        /// <param name = "username">The username.</param>
        /// <param name = "password">The password.</param>
        /// <param name = "mailAddress">The mail address.</param>
        public void CreateUser(string username, string password, string mailAddress)
        {
            this.Logger.Debug("Creating user with username '{0}', password '{1}', mailAddress: '{2}'", username, password, mailAddress);

            Membership.CreateUser(username, password, mailAddress);
            try
            {
                this.InvokeTransaction(s => s.Save(new MediaCommUser(username, mailAddress)));
            }
            catch (Exception ex)
            {
                Membership.DeleteUser(username);

                throw new CreateUserException(username, password, mailAddress, ex);
            }

            this.Logger.Debug("Finished creating user");
        }

        /// <summary>
        ///   Gets all users.
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<MediaCommUser> GetAllUsers()
        {
            this.Logger.Debug("Getting all users");
            IEnumerable<MediaCommUser> users = this.Session.Linq<MediaCommUser>().ToList();

            this.Logger.Debug("Got {0} users", users.Count());
            return users;
        }

        /// <summary>
        ///   Gets user by name.
        /// </summary>
        /// <param name = "userName">Name of the user.</param>
        /// <returns>The user with the provided name..</returns>
        public MediaCommUser GetUserByName(string userName)
        {
            this.Logger.Debug("Getting user for username '{0}'", userName);
            MediaCommUser user = this.Session.Linq<MediaCommUser>().Single(
                u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            this.Logger.Debug("Got user: " + user);
            return user;
        }

        /// <summary>
        ///   Updates the user.
        /// </summary>
        /// <param name = "user">The user to update.</param>
        public void UpdateUser(MediaCommUser user)
        {
            this.Logger.Debug("Updating user: " + user);
            this.InvokeTransaction(s => s.Update(user));
            this.Logger.Debug("Finished updating user");
        }

        #endregion

        #endregion
    }
}
