
#region Using Directives

using System;
using System.Diagnostics;

using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace MediaCommMVC.Common.Logging
{
    /// <summary>Provides simple logging using the enterprise library.</summary>
    public class EntLibLogger : ILogger
    {
        #region Properties

        /// <summary>Gets the last log entry.</summary>
        /// <value>The last log entry.</value>
        public LogEntry LastLogEntry
        {
            get;
            private set;
        }

        #endregion

        #region Implemented Interfaces

        #region ILogger

        /// <summary>Logs the specified message with Debug level.</summary>
        /// <param name="message">The message to log.</param>
        public void Debug(string message)
        {
            try
            {
                CheckMessageParameterForNull(message);

                LogEntry entry = new LogEntry { Message = message, Severity = TraceEventType.Verbose };
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        /// <summary>Logs the specified message with Debug level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Debug(string message, params object[] data)
        {
            try
            {
                CheckMessageParameterForNull(message);
                CheckDataParameterForNull(data);

                LogEntry entry = this.CreateFormattedLogEntry(message, data);
                entry.Severity = TraceEventType.Verbose;
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        public void Error(string message)
        {
            try
            {
                CheckMessageParameterForNull(message);

                LogEntry entry = new LogEntry { Message = message, Severity = TraceEventType.Error };
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Error(string message, params object[] data)
        {
            try
            {
                CheckMessageParameterForNull(message);
                CheckDataParameterForNull(data);

                LogEntry entry = this.CreateFormattedLogEntry(message, data);
                entry.Severity = TraceEventType.Error;
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        /// <summary>Logs the specified message with Error level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="innerException">The inner exception.</param>
        public void Error(string message, Exception innerException)
        {
            try
            {
                CheckMessageParameterForNull(message);

                if (innerException == null)
                {
                    throw new ArgumentNullException("innerException");
                }

                LogEntry entry = new LogEntry
                    {
                        Message = string.Format("{0}{1}{2}", message, Environment.NewLine, innerException), 
                        Severity = TraceEventType.Error
                    };
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        /// <summary>Logs the specified message with Info level.</summary>
        /// <param name="message">The message to log.</param>
        public void Info(string message)
        {
            try
            {
                CheckMessageParameterForNull(message);

                LogEntry entry = new LogEntry { Message = message, Severity = TraceEventType.Information };
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        /// <summary>Logs the specified message with Info level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Info(string message, params object[] data)
        {
            try
            {
                CheckMessageParameterForNull(message);
                CheckDataParameterForNull(data);

                LogEntry entry = this.CreateFormattedLogEntry(message, data);
                entry.Severity = TraceEventType.Information;
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        /// <summary>Logs the specified message with Warn level.</summary>
        /// <param name="message">The message to log.</param>
        public void Warn(string message)
        {
            try
            {
                CheckMessageParameterForNull(message);

                LogEntry entry = new LogEntry { Message = message, Severity = TraceEventType.Warning };
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        /// <summary>Logs the specified message with Warn level.</summary>
        /// <param name="message">The message to log.</param>
        /// <param name="data">The parameters for the message string.</param>
        public void Warn(string message, params object[] data)
        {
            try
            {
                CheckMessageParameterForNull(message);
                CheckDataParameterForNull(data);

                LogEntry entry = this.CreateFormattedLogEntry(message, data);
                entry.Severity = TraceEventType.Warning;
                WriteLogEntry(entry);
                this.LastLogEntry = entry;
            }
            catch (Exception ex)
            {
                this.HandleError(ex, message);
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>Checks the data parameter.</summary>
        /// <param name="data">The data to check for null.</param>
        private static void CheckDataParameterForNull(object[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(
                    "data", "Don't call logger with null data parameter, use overloaded methods instead.");
            }
        }

        /// <summary>Checks the message parameter.</summary>
        /// <param name="message">The message.</param>
        private static void CheckMessageParameterForNull(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
        }

        /// <summary>Handles the fatal error.</summary>
        /// <param name="message">The message.</param>
        private static void HandleFatalError(string message)
        {
            try
            {
                EventLog.WriteEntry("AdventureWorkCRM", message, EventLogEntryType.Error);
            }
            catch
            {
                // No futher logging available
            }
        }

        /// <summary>Writes the log entry.</summary>
        /// <param name="entry">The log entry.</param>
        private static void WriteLogEntry(LogEntry entry)
        {
            try
            {
                Logger.Write(entry);
            }
            catch (Exception ex)
            {
                HandleFatalError("Error writing logfile:" + ex.Message + "\nOriginal Exception:\n" + entry == null ? "null" : entry.ToString());
            }
        }

        /// <summary>Creates a formatted log entry.</summary>
        /// <param name="message">The message to format.</param>
        /// <param name="data">The parameters to add to the message.</param>
        /// <returns>The formatted log entry.</returns>
        private LogEntry CreateFormattedLogEntry(string message, object[] data)
        {
            LogEntry entry = null;
            try
            {
                entry = new LogEntry { Message = string.Format(message, data) };
            }
            catch (FormatException ex)
            {
                string errorMessage = string.Format(
                    "The message to format does not fit the parameter count. Message: '{0}', Parameters: '{1}'", 
                    message ?? string.Empty, 
                    data ?? new object[] { string.Empty });

                this.Error(errorMessage, ex);

                entry = new LogEntry { Message = message };
            }

            return entry;
        }

        /// <summary>Handles logging errors.</summary>
        /// <param name="ex">The exeption to handle.</param>
        /// <param name="originalMessage">The original message.</param>
        private void HandleError(Exception ex, string originalMessage)
        {
            try
            {
                this.Error("Error while trying to log. Original Message: " + originalMessage ?? string.Empty, ex);
            }
            catch (Exception fex)
            {
                HandleFatalError(fex.ToString());
            }
        }

        #endregion
    }
}
