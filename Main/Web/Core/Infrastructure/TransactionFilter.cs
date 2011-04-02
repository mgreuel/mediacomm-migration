namespace MediaCommMVC.Core.Infrastructure
{
    #region Using Directives

    using System.Web.Mvc;

    using NHibernate;

    #endregion

    public class TransactionFilter : ActionFilterAttribute
    {
        #region Public Methods

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ISession session = MvcApplication.SessionFactory.GetCurrentSession();

            using (session.Transaction)
            {
                if (filterContext.Exception == null)
                {
                    session.Transaction.Commit();
                }
                else
                {
                    session.Transaction.Rollback();
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = MvcApplication.SessionFactory.GetCurrentSession();
            session.BeginTransaction();
        }

        #endregion
    }
}