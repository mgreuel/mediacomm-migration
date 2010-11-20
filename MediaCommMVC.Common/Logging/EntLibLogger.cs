
#region Using Directives

using System;
using System.Collections;
using System.Diagnostics;



#endregion

namespace MediaCommMVC.Common.Logging
{
    /// <summary>Provides simple logging using the enterprise library.</summary>
    public class EntLibLogger : ILogger
    {


        #region Implemented Interfaces

        #region ILogger

        /// <summary>Logs the specified message with Debug level.</summary>
        /// <param name="message">The message to log.</param>
        public void Debug(string message)
        {

        }

        /// <summary>Logs the specified message with Debug level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Debug(string message, params object[] data)
        {

        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        public void Error(string message)
        {

        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Error(string message, params object[] data)
        {
            
        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="innerException">The inner exception.</param>
        public void Error(string message, Exception innerException)
        {

        }

        /// <summary>Logs the specified message with Info level.</summary>
        /// <param name="message">The message to log.</param>
        public void Info(string message)
        {

        }

        /// <summary>Logs the specified message with Info level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Info(string message, params object[] data)
        {

        }

        /// <summary>Logs the specified message with Warn level.</summary>
        /// <param name="message">The message to log.</param>
        public void Warn(string message)
        {

        }

        /// <summary>Logs the specified message with Warn level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Warn(string message, params object[] data)
        {

        }

        #endregion

        #endregion




 
    }
}
