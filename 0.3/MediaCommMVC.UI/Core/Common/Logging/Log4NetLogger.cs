using System;

using log4net;

namespace MediaCommMVC.Web.Core.Common.Logging
{
    /// <summary>
    /// Logger implementation using log4net.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        #region Constants and Fields

        /// <summary>
        /// The log4netlogger instance.
        /// </summary>
        private readonly ILog log;

        #endregion

        #region Constructors and Destructors

        public Log4NetLogger()
        {
            this.log = LogManager.GetLogger("Default");
        }

        #endregion

        #region Implemented Interfaces

        #region ILogger

        /// <summary>
        /// Logs the specified message with Debug level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Debug(string message)
        {
            if (this.log.IsDebugEnabled)
            {
                this.log.Debug(message);
            }
        }

        /// <summary>
        /// Logs the specified message with Debug level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Debug(string message, params object[] data)
        {
            if (this.log.IsDebugEnabled)
            {
                this.log.Debug(this.CreateFormattedLogMessage(message, data));
            }
        }

        /// <summary>
        /// Logs the specified message with Error level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Error(string message)
        {
            if (this.log.IsErrorEnabled)
            {
                this.log.Error(message);
            }
        }

        /// <summary>
        /// Logs the specified message with Error level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Error(string message, params object[] data)
        {
            if (this.log.IsErrorEnabled)
            {
                this.log.Error(this.CreateFormattedLogMessage(message, data));
            }
        }

        /// <summary>
        /// Logs the specified message with Error level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="innerException">The inner exception.</param>
        public void Error(string message, Exception innerException)
        {
            if (this.log.IsErrorEnabled)
            {
                this.log.Error(message, innerException);
            }
        }

        /// <summary>
        /// Logs the specified message with Info level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Info(string message)
        {
            if (this.log.IsInfoEnabled)
            {
                this.log.Info(message);
            }
        }

        /// <summary>
        /// Logs the specified message with Info level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Info(string message, params object[] data)
        {
            if (this.log.IsInfoEnabled)
            {
                this.log.Info(this.CreateFormattedLogMessage(message, data));
            }
        }

        /// <summary>
        /// Logs the specified message with Warn level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Warn(string message)
        {
            if (this.log.IsWarnEnabled)
            {
                this.log.Warn(message);
            }
        }

        /// <summary>
        /// Logs the specified message with Warn level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Warn(string message, params object[] data)
        {
            if (this.log.IsWarnEnabled)
            {
                this.log.Warn(this.CreateFormattedLogMessage(message, data));
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>Creates a formatted message.</summary>
        /// <param name="message">The message to format.</param>
        /// <param name="data">The parameters to add to the message.</param>
        /// <returns>The formatted log entry.</returns>
        private string CreateFormattedLogMessage(string message, object[] data)
        {
            string formattedLogMessage = message;
            try
            {
                formattedLogMessage = string.Format(message, data);
            }
            catch (FormatException ex)
            {
                string errorMessage =
                    string.Format(
                        "The message to format does not fit the parameter count. Message: '{0}', Parameters: '{1}'",
                        message,
                        data);

                this.Error(errorMessage, ex);
            }

            return formattedLogMessage;
        }

        #endregion
    }
}
