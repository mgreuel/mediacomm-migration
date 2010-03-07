#region Using Directives

using System;

using MediaCommMVC.Common.Logging;

#endregion

namespace MediaCommMVC.Tests.TestImplementations
{
    /// <summary>Implements a logger using the Console Output.</summary>
    public class ConsoleLogger : ILogger
    {
        #region Implemented Interfaces

        #region ILogger

        /// <summary>Logs the specified message with Debug level.</summary>
        /// <param name="message">The message to log.</param>
        public void Debug(string message)
        {
            Console.WriteLine("{0}\tDEBUG\t{1}", DateTime.Now.ToShortTimeString(), message);
        }

        /// <summary>Logs the specified message with Debug level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Debug(string message, params object[] data)
        {
            Console.WriteLine("{0}\tDEBUG\t{1}", DateTime.Now.ToShortTimeString(), string.Format(message, data));
        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        public void Error(string message)
        {
            Console.WriteLine("{0}\tERROR\t{1}", DateTime.Now.ToShortTimeString(), message);
        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Error(string message, params object[] data)
        {
            Console.WriteLine("{0}\tERROR\t{1}", DateTime.Now.ToShortTimeString(), string.Format(message, data));
        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="innerException">The inner exception.</param>
        public void Error(string message, Exception innerException)
        {
            Console.WriteLine("{0}\tERROR\t{1}\n{2}", DateTime.Now.ToShortTimeString(), innerException.ToString());
        }

        /// <summary>Logs the specified message with Info level.</summary>
        /// <param name="message">The message to log.</param>
        public void Info(string message)
        {
            Console.WriteLine("{0}\tINFO\t{1}", DateTime.Now.ToShortTimeString(), message);
        }

        /// <summary>Logs the specified message with Info level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Info(string message, params object[] data)
        {
            Console.WriteLine("{0}\tINFO\t{1}", DateTime.Now.ToShortTimeString(), string.Format(message, data));
        }

        /// <summary>Logs the specified message with Warn level.</summary>
        /// <param name="message">The message to log.</param>
        public void Warn(string message)
        {
            Console.WriteLine("{0}\tWARN\t{1}", DateTime.Now.ToShortTimeString(), message);
        }

        /// <summary>Logs the specified message with Warn level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Warn(string message, params object[] data)
        {
            Console.WriteLine("{0}\tWARN\t{1}", DateTime.Now.ToShortTimeString(), string.Format(message, data));
        }

        #endregion

        #endregion
    }
}