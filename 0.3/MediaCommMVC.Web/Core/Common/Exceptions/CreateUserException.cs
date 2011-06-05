using System;

namespace MediaCommMVC.Web.Core.Common.Exceptions
{
    [Serializable]
    public sealed class CreateUserException : MediaCommException
    {
        public CreateUserException()
            : base("User creation failed")
        {
        }

        public CreateUserException(string username, string password, string mailAddress)
            : base("User creation failed")
        {
            this.Data.Add("Username", username);
            this.Data.Add("Password", password);
            this.Data.Add("mailAddress", mailAddress);
        }

        public CreateUserException(string username, string password, string mailAddress, Exception innerException)
            : base("User creation failed", innerException)
        {
            this.Data.Add("Username", username);
            this.Data.Add("Password", password);
            this.Data.Add("mailAddress", mailAddress);
        }
    }
}
