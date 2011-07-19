#region Using Directives

using System;

#endregion

namespace MediaCommMVC.Common.Exceptions
{
    /// <summary>Exception thrown when the creation of a user failed.</summary>
    [Serializable]
    public class CreateUserException : MediaCommException
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="CreateUserException"/> class.</summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="mailAddress">The mail address.</param>
        public CreateUserException(string username, string password, string mailAddress) : base("User creation failed")
        {
            this.Data.Add("Username", username);
            this.Data.Add("Password", password);
            this.Data.Add("mailAddress", mailAddress);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateUserException"/> class.</summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="mailAddress">The mail address.</param>
        /// <param name="innerException">The inner exception.</param>
        public CreateUserException(string username, string password, string mailAddress, Exception innerException)
            : base("User creation failed", innerException)
        {
            this.Data.Add("Username", username);
            this.Data.Add("Password", password);
            this.Data.Add("mailAddress", mailAddress);
        }

        #endregion
    }
}
