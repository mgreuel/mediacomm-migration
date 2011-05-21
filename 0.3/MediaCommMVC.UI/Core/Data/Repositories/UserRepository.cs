#region Using Directives

using System;
using System.Collections.Generic;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Exceptions;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data.NHInfrastructure;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Users;

using NHibernate.Linq;

using Enumerable = System.Linq.Enumerable;
using Queryable = System.Linq.Queryable;

#endregion

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    using System.Linq;

    using MediaCommMVC.Web.Core.Infrastructure;

    /// <summary>Implements the IUserRepository using nHibernate.</summary>
    public class UserRepository : RepositoryBase, IUserRepository
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="UserRepository"/> class.</summary>
        /// <param name="sessionManager">The NHibernate session manager.</param>
        /// <param name="configAccessor">The config Accessor.</param>
        /// <param name="logger">The logger.</param>
        public UserRepository(ISessionContainer sessionManager, IConfigAccessor configAccessor, ILogger logger)
            : base(sessionManager, configAccessor, logger)
        {
        }

        #endregion

        #region Implemented Interfaces

        #region IUserRepository

        public void CreateAdmin(string userName, string password, string mailAddress)
        {
            MediaCommUser user = new MediaCommUser(userName, mailAddress, password) { IsAdmin = true };
            this.Session.Save(user);
        }

        public void CreateUser(string username, string password, string mailAddress)
        {
            this.Logger.Debug("Creating user with username '{0}', password '{1}', mailAddress: '{2}'", username, password, mailAddress);

            try
            {
                this.Session.Save(new MediaCommUser(username, mailAddress, password));
            }
            catch (Exception ex)
            {
                throw new CreateUserException(username, password, mailAddress, ex);
            }
        }

        public IEnumerable<MediaCommUser> GetAllUsers()
        {
            IEnumerable<MediaCommUser> users = this.Session.Query<MediaCommUser>().ToList();
            return users;
        }

        public MediaCommUser GetUserByName(string userName)
        {
            MediaCommUser user = this.Session.Query<MediaCommUser>().Single(u => u.UserName.Equals(userName));

            return user;
        }

        public void UpdateUser(MediaCommUser user)
        {
            this.Logger.Debug("Updating user: " + user);
            this.Session.Update(user);
        }

        /// <summary>Validates the user information.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>true, if the user/password combination is valid, otherwise false.</returns>
        public bool ValidateUser(string userName, string password)
        {
            return this.Session.Query<MediaCommUser>().Any(u => u.UserName.Equals(userName) && u.Password.Equals(password));
        }

        #endregion

        #endregion
    }
}
