using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when the creation of a user failed.
    /// </summary>
    public class CreateUserException : MediaCommException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserException"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="mailAddress">The mail address.</param>
        public CreateUserException(string username, string password, string mailAddress) : base("User creation failed")
        {
            this.Data.Add("Username", username);
            this.Data.Add("Password", password);
            this.Data.Add("mailAddress", mailAddress);
        }
    }
}
