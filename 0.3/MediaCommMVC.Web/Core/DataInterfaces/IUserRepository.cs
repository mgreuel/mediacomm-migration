#region Using Directives

using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Users;

#endregion

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    /// <summary>Interface for all user repositories.</summary>
    public interface IUserRepository
    {
        #region Public Methods

        /// <summary>Creates an admin user.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="mailAddress">The mail address.</param>
        void CreateAdmin(string userName, string password, string mailAddress);

        /// <summary>Creates a new user.</summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="mailAddress">The mail address.</param>
        void CreateUser(string username, string password, string mailAddress);

        /// <summary>Gets all users.</summary>
        /// <returns>The users.</returns>
        IEnumerable<MediaCommUser> GetAllUsers();

        /// <summary>Gets user by name.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The user with the provided name..</returns>
        MediaCommUser GetUserByName(string userName);

        /// <summary>Updates the user.</summary>
        /// <param name="user">The user to update.</param>
        void UpdateUser(MediaCommUser user);

        /// <summary>Validates the user information.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>true, if thew user/password combination is valid, otherwise false.</returns>
        bool ValidateUser(string userName, string password);

        #endregion
    }
}
