using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NHibernate;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class MemorySessionContainer : ISessionContainer, IDisposable
    {
        public ISession CurrentSession { get; set; }

        public void BeginSessionAndTransaction()
        {
            ISession session = SessionFactoryContainer.SessionFactory.OpenSession();
            session.BeginTransaction();
            this.CurrentSession = session;
        }

        public void EndSessionAndCommitTransaftion()
        {
            using (this.CurrentSession)
            {
                if (CurrentSession == null || CurrentSession.Transaction == null || !CurrentSession.Transaction.IsActive)
                {
                    return;
                }

                CurrentSession.Transaction.Commit();
            }
        }

        public void EndSessionAndRollbackTransaftion()
        {
            using (this.CurrentSession)
            {
                if (CurrentSession == null || CurrentSession.Transaction == null || !CurrentSession.Transaction.IsActive)
                {
                    return;
                }

                CurrentSession.Transaction.Rollback();
            }
        }

        public void Dispose()
        {
            this.EndSessionAndRollbackTransaftion();
        }
    }
}