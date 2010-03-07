#region Using Directives

using System.Collections.Generic;

using MediaCommMVC.Core.Model.Users;

#endregion

namespace MediaCommMVC.Core.DataInterfaces
{
    /// <summary>Interface for all user repositories.</summary>
    public interface IUserRepository
    {
        #region Public Methods

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

        #endregion
    }
}
