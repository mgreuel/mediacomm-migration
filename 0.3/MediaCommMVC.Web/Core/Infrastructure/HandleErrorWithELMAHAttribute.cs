#region Using Directives

using System;
using System.Web;
using System.Web.Mvc;

using Elmah;

#endregion

namespace MediaCommMVC.Web.Core.Infrastructure
{
    /// <summary>
    /// Attribute used to track errors with ELMAH
    /// <see cref="http://stackoverflow.com/questions/766610/"/>.
    /// </summary>
    public sealed class HandleErrorWithELMAHAttribute : HandleErrorAttribute
    {
        #region Public Methods

        /// <summary>
        /// Called when a [exception] occurs.
        /// </summary>
        /// <param name="context">The exception context.</param>
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var e = context.Exception;
            if (!context.ExceptionHandled
                || RaiseErrorSignal(e)
                || IsFiltered(context))
            {
                return;
            }

            LogException(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified context is filtered.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// 	<c>true</c> if the specified context is filtered; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsFiltered(ExceptionContext context)
        {
            var config = context.HttpContext.GetSection("elmah/errorFilter") as ErrorFilterConfiguration;

            if (config == null)
            {
                return false;
            }

            var testContext = new ErrorFilterModule.AssertionHelperContext(context.Exception, HttpContext.Current);

            return config.Assertion.Test(testContext);
        }

        /// <summary>
        /// Logs the exception with ELMAH.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private static void LogException(Exception exception)
        {
            HttpContext context = HttpContext.Current;
            ErrorLog.GetDefault(context).Log(new Error(exception, context));
        }

        /// <summary>
        /// Raises the error signal.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>Whether an error signal was raised.</returns>
        private static bool RaiseErrorSignal(Exception exception)
        {
            var context = HttpContext.Current;
            if (context == null)
            {
                return false;
            }

            var signal = ErrorSignal.FromContext(context);
            if (signal == null)
            {
                return false;
            }

            signal.Raise(exception, context);
            return true;
        }

        #endregion
    }
}