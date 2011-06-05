namespace MediaCommMVC.Web.Core.Infrastructure
{
    #region Using Directives

    using System.Web.Mvc;

    using NHibernate;

    #endregion

    public class NHibernateActionFilter : ActionFilterAttribute
    {
        #region Public Methods

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

        #endregion
    }
}