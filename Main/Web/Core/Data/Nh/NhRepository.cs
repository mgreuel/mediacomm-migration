namespace MediaCommMVC.Core.Data.Nh
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate;
    using NHibernate.Linq;

    #endregion

    public abstract class NhRepository<T> : IRepository<T>
    {
        #region Constructors and Destructors

        protected NhRepository(ISession session)
        {
            this.Session = session;
        }

        #endregion

        #region Properties

        protected ISession Session { get; private set; }

        #endregion

        #region Implemented Interfaces

        #region IRepository<T>

        public IEnumerable<T> GetAllMatching(Func<T, bool> filter)
        {
            return this.Session.Query<T>().Where(x => filter(x)).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return this.Session.Query<T>().ToList();
        }

        public T GetById(int id)
        {
            return this.Session.Get<T>(id);
        }

        #endregion

        #endregion
    }
}