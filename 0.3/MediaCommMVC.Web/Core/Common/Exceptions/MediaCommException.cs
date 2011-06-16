using System;

namespace MediaCommMVC.Web.Core.Common.Exceptions
{
    [Serializable]
    public class MediaCommException : Exception
    {
        public MediaCommException()
        {
        }

        public MediaCommException(string message)
            : base(message)
        {
        }

        public MediaCommException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}