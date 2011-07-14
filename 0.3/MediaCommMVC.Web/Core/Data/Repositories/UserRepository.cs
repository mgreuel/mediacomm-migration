using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMVC.Web.Core.Common.Exceptions;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Users;

using NHibernate;
using NHibernate.Linq;

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISessionContainer sessionContainer;

        public UserRepository(ISessionContainer sessionContainer)
        {
            this.sessionContainer = sessionContainer;
        }

        private ISession Session
        {
            get
            {
                return this.sessionContainer.CurrentSession;
            }
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
            return this.Session.Query<MediaCommUser>().Single(u => u.UserName == userName);
        }

        public void UpdateUser(MediaCommUser user)
        {
            this.Session.Update(user);
        }

        public bool ValidateUser(string userName, string password)
        {
            return this.Session.Query<MediaCommUser>().Any(u => u.UserName.Equals(userName) && u.Password.Equals(password));
        }

        public IEnumerable<string> GetMailAddressesToNotifyAboutNewPost()
        {
            IEnumerable<string> mailAddresses =
                this.Session.CreateSQLQuery(
                    @"SELECT     EMailAddress
                    FROM         MediaCommUsers
                    WHERE     (ForumsNotificationInterval = 1) AND (LastForumsNotification IS NULL) OR
                      (ForumsNotificationInterval = 1) AND (LastVisit IS NULL) OR
                      (ForumsNotificationInterval = 1) AND (LastForumsNotification < LastVisit) OR
                      (ForumsNotificationInterval = 1) AND (LastForumsNotification < DATEADD(day, - 7, GETDATE()))")
                    .List<string>();

            return mailAddresses;
        }

        public void UpdateLastForumsNotification(IEnumerable<string> notifiedMailAddresses, DateTime notificationTime)
        {
            // Users cannot edit their mailadresses so there is no SQL injection possible
            string joinedMailAddresses = string.Join(",", notifiedMailAddresses.Select(m => "'" + m + "'"));
            ISQLQuery updateQuery =
                this.Session.CreateSQLQuery(
                    @"UPDATE MediaCommUsers SET LastForumsNotification = :notificationTime WHERE EMailAddress IN ( " + joinedMailAddresses + " )");
            updateQuery.SetDateTime("notificationTime", notificationTime);
            updateQuery.ExecuteUpdate();
        }

        public IEnumerable<string> GetMailAddressesToNotifyAboutNewPhotos()
        {
            IEnumerable<string> mailAddresses =
                this.Session.CreateSQLQuery(
                    @"SELECT     EMailAddress
                    FROM         MediaCommUsers
                    WHERE     (PhotosNotificationInterval = 1) AND (LastPhotosNotification IS NULL) OR
                      (PhotosNotificationInterval = 1) AND (LastVisit IS NULL) OR
                      (PhotosNotificationInterval = 1) AND (LastPhotosNotification < LastVisit) OR
                      (PhotosNotificationInterval = 1) AND (LastPhotosNotification < DATEADD(day, - 7, GETDATE()))")
                    .List<string>();

            return mailAddresses;
        }

        public void UpdateLastPhotosNotification(IEnumerable<string> notifiedMailAddresses, DateTime notificationTime)
        {
            // Users cannot edit their mailadresses so there is no SQL injection possible
            string joinedMailAddresses = string.Join(",", notifiedMailAddresses.Select(m => "'" + m + "'"));
            ISQLQuery updateQuery =
                this.Session.CreateSQLQuery(
                    @"UPDATE MediaCommUsers SET LastPhotosNotification = :notificationTime WHERE EMailAddress IN ( " + joinedMailAddresses + " )");
            updateQuery.SetDateTime("notificationTime", notificationTime);
            updateQuery.ExecuteUpdate();
        }

        public IEnumerable<string> GetMailAddressesToNotifyAboutNewVideos()
        {
            IEnumerable<string> mailAddresses =
                this.Session.CreateSQLQuery(
                    @"SELECT     EMailAddress
                    FROM         MediaCommUsers
                    WHERE     (VideosNotificationInterval = 1) AND (LastVideosNotification IS NULL) OR
                      (VideosNotificationInterval = 1) AND (LastVisit IS NULL) OR
                      (VideosNotificationInterval = 1) AND (LastVideosNotification < LastVisit) OR
                      (VideosNotificationInterval = 1) AND (LastVideosNotification < DATEADD(day, - 7, GETDATE()))")
                    .List<string>();

            return mailAddresses;
        }

        public void UpdateLastVideosNotification(IEnumerable<string> notifiedMailAddresses, DateTime notificationTime)
        {
            // Users cannot edit their mailadresses so there is no SQL injection possible
            string joinedMailAddresses = string.Join(",", notifiedMailAddresses.Select(m => "'" + m + "'"));
            ISQLQuery updateQuery =
                this.Session.CreateSQLQuery(
                    @"UPDATE MediaCommUsers SET LastVideosNotification = :notificationTime WHERE EMailAddress IN ( " + joinedMailAddresses + " )");
            updateQuery.SetDateTime("notificationTime", notificationTime);
            updateQuery.ExecuteUpdate();
        }
    }
}