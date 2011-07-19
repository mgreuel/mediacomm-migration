using System;
using System.Web;
using System.Web.Mvc;

using Elmah;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    /// <see cref="http://stackoverflow.com/questions/766610/"/>.
    public sealed class HandleErrorWithELMAHAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var e = context.Exception;
            if (!context.ExceptionHandled || RaiseErrorSignal(e) || IsFiltered(context))
            {
                return;
            }

            LogException(e);
        }

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

        private static void LogException(Exception exception)
        {
            HttpContext context = HttpContext.Current;
            ErrorLog.GetDefault(context).Log(new Error(exception, context));
        }

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
    }
}