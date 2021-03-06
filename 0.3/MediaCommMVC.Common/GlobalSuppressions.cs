﻿// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File".
// You do not need to add suppressions to this file manually.
#region Using Directives

using System.Diagnostics.CodeAnalysis;

#endregion

[assembly: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "MediaCommMVC.Common.Exceptions.CreateUserException.#.ctor(System.String,System.String,System.String)")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "MediaCommMVC.Common.Exceptions.CreateUserException.#.ctor(System.String,System.String,System.String,System.Exception)")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "MediaCommMVC.Common.Logging.ILogger.Debug(System.String,System.Object[])", Scope = "member", Target = "MediaCommMVC.Common.Config.FileConfigAccessor.#GetConfigValue(System.String)")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "MediaCommMVC.Common.Logging.ILogger.Debug(System.String,System.Object[])", Scope = "member", Target = "MediaCommMVC.Common.Config.FileConfigAccessor.#GetConfigValues(System.String)")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "MediaCommMVC.Common.Logging.ILogger.Debug(System.String,System.Object[])", Scope = "member", Target = "MediaCommMVC.Common.Config.FileConfigAccessor.#SaveConfigValue(System.String,System.String)")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "MediaCommMVC.Common.Logging.ILogger.Debug(System.String,System.Object[])", Scope = "member", Target = "MediaCommMVC.Common.Config.FileConfigAccessor.#SaveConfigValues(System.String,System.Collections.Generic.IEnumerable`1<System.String>)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Scope = "type", Target = "MediaCommMVC.Common.Exceptions.CreateUserException")]
[assembly: SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Scope = "type", Target = "MediaCommMVC.Common.Exceptions.MediaCommException")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "MediaCommMVC.Common.Logging.EntLibLogger.Error(System.String,System.Exception)", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#CreateFormattedLogEntry(System.String,System.Object[])")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Debug(System.String)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Debug(System.String,System.Object[])")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Error(System.String)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Error(System.String,System.Exception)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Error(System.String,System.Object[])")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "MediaCommMVC.Common.Logging.EntLibLogger.Error(System.String,System.Exception)", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#HandleError(System.Exception,System.String)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#HandleError(System.Exception,System.String)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#HandleFatalError(System.String)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Info(System.String)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Info(System.String,System.Object[])")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Warn(System.String)")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "MediaCommMVC.Common.Logging.EntLibLogger.HandleFatalError(System.String)", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#WriteLogEntry(Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#WriteLogEntry(Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "MediaCommMVC.Common.Logging.EntLibLogger.#Warn(System.String,System.Object[])")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error", Scope = "member", Target = "MediaCommMVC.Common.Logging.ILogger.#Error(System.String)")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error", Scope = "member", Target = "MediaCommMVC.Common.Logging.ILogger.#Error(System.String,System.Object[])")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Error", Scope = "member", Target = "MediaCommMVC.Common.Logging.ILogger.#Error(System.String,System.Exception)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "MediaCommMVC.Common.Logging.Log4NetLogger.Error(System.String,System.Exception)", Scope = "member", Target = "MediaCommMVC.Common.Logging.Log4NetLogger.#CreateFormattedLogMessage(System.String,System.Object[])")]
