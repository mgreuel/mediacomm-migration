#region Using Directives

using System;

#endregion

namespace MediaCommMVC.Common.Logging
{
    /// <summary>
    ///   Defines all logging methods.
    /// </summary>
    public interface ILogger
    {
        #region Public Methods

        /// <summary>
        ///   Logs the specified message with Debug level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        void Debug(string message);

        /// <summary>
        ///   Logs the specified message with Debug level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        /// <param name = "data">The parameters for the message string.</param>
        void Debug(string message, params object[] data);

        /// <summary>
        ///   Logs the specified message with Error level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        void Error(string message);

        /// <summary>
        ///   Logs the specified message with Error level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        /// <param name = "data">The parameters for the message string.</param>
        void Error(string message, params object[] data);

        /// <summary>
        ///   Logs the specified message with Error level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        /// <param name = "innerException">The inner exception.</param>
        void Error(string message, Exception innerException);

        /// <summary>
        ///   Logs the specified message with Info level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        void Info(string message);

        /// <summary>
        ///   Logs the specified message with Info level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        /// <param name = "data">The parameters for the message string.</param>
        void Info(string message, params object[] data);

        /// <summary>
        ///   Logs the specified message with Warn level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        void Warn(string message);

        /// <summary>
        ///   Logs the specified message with Warn level.
        /// </summary>
        /// <param name = "message">The message to log.</param>
        /// <param name = "data">The parameters for the message string.</param>
        void Warn(string message, params object[] data);

        #endregion
    }
}
