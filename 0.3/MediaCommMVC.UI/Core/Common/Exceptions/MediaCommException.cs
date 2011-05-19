#region Using Directives

using System;

#endregion

namespace MediaCommMVC.Web.Core.Common.Exceptions
{
    /// <summary>Base class for media comm exceptions.</summary>
    [Serializable]
    public class MediaCommException : Exception
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="MediaCommException"/> class.</summary>
        public MediaCommException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MediaCommException"/> class.</summary>
        /// <param name="message">The message.</param>
        public MediaCommException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MediaCommException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MediaCommException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
