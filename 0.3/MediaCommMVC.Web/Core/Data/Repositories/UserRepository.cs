using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Exceptions;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Users;

using NHibernate.Linq;

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(ISessionContainer sessionManager, IConfigAccessor configAccessor, ILogger logger)
            : base(sessionManager, configAccessor, logger)
        {
        }

        public void CreateAdmin(string userName, string password, string mailAddress)
        {
            MediaCommUser user = new MediaCommUser(userName, mailAddress, password) { IsAdmin = true };
            this.Session.Save(user);
        }

        public void CreateUser(string username, string password, string mailAddress)
        {
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
            return this.Session.Query<MediaCommUser>().ToList();
        }

        public MediaCommUser GetUserByName(string userName)
        {
            return this.Session.Query<MediaCommUser>().Single(u => u.UserName.Equals(userName));
        }

        public void UpdateUser(MediaCommUser user)
        {
            this.Session.Update(user);
        }

        public bool ValidateUser(string userName, string password)
        {
            return this.Session.Query<MediaCommUser>().Any(u => u.UserName.Equals(userName) && u.Password.Equals(password));
        }
    }
}