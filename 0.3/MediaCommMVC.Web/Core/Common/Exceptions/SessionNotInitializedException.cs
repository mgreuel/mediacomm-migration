namespace MediaCommMVC.Web.Core.Common.Exceptions
{
    using System;

    public class SessionNotInitializedException : Exception
    {
        public SessionNotInitializedException(string message) : base(message)
        {
        }
    }
}