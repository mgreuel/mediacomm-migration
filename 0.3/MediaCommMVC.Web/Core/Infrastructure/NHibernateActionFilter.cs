namespace MediaCommMVC.Web.Core.Infrastructure
{
    #region Using Directives

    using System.Web.Mvc;

    using NHibernate;

    #endregion

    public class NHibernateActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = HttpContextSessionContainer.SessionFactory.OpenSession();
            session.BeginTransaction();
            new HttpContextSessionContainer().CurrentSession = session;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (ISession session = new HttpContextSessionContainer().CurrentSession)
            {
                if (session == null || session.Transaction == null || !session.Transaction.IsActive)
                {
                    return;
                }

                if (filterContext.Exception != null)
                {
                    session.Transaction.Rollback();
                }
                else
                {
                    session.Transaction.Commit();
                }
            }
        }
    }
}