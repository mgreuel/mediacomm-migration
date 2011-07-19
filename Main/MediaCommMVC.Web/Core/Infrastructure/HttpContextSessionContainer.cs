using System.Web;

using MediaCommMVC.Web.Core.Common.Exceptions;

using NHibernate;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public sealed class HttpContextSessionContainer : ISessionContainer
    {
        public ISession CurrentSession
        {
            get
            {
                if (InternalSession == null)
                {
                    throw new SessionNotInitializedException(
                        "The NH session has not been initialized. Make sure all actions accessing the DB have the NHibernateActionFilter attribute.");
                }

                return InternalSession;
            }

            set
            {
                InternalSession = value;
            }
        }

        private static ISession InternalSession
        {
            get
            {
                return HttpContext.Current.Items["NHibernateSession"] as ISession;
            }

            set
            {
                HttpContext.Current.Items["NHibernateSession"] = value;
            }
        }
    }
}