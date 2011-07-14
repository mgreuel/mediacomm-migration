using System;
using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    public interface IUserRepository
    {
        void CreateAdmin(string userName, string password, string mailAddress);

        void CreateUser(string username, string password, string mailAddress);

        IEnumerable<MediaCommUser> GetAllUsers();

        MediaCommUser GetUserByName(string userName);

        void UpdateUser(MediaCommUser user);

        bool ValidateUser(string userName, string password);

        IEnumerable<string> GetMailAddressesToNotifyAboutNewPost();

        void UpdateLastForumsNotification(IEnumerable<string> notifiedMailAddresses, DateTime notificationTime);

        IEnumerable<string> GetMailAddressesToNotifyAboutNewPhotos();

        void UpdateLastPhotosNotification(IEnumerable<string> notifiedMailAddresses, DateTime notificationTime);

        IEnumerable<string> GetMailAddressesToNotifyAboutNewVideos();

        void UpdateLastVideosNotification(IEnumerable<string> usersMailAddressesToNotify, DateTime notificationTime);
    }
}