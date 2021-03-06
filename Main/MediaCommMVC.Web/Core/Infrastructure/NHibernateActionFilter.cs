﻿using System.Web.Mvc;

using NHibernate;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class NHibernateActionFilter : ActionFilterAttribute
    {
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

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = SessionFactoryContainer.SessionFactory.OpenSession();
            session.BeginTransaction();
            new HttpContextSessionContainer().CurrentSession = session;
        }
    }
}