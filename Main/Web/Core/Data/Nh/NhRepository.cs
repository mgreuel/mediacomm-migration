namespace MediaCommMVC.Core.Data.Nh
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MediaCommMVC.Core.Infrastructure;

    using NHibernate.Linq;

    #endregion

    public abstract class NhRepository<T> : IRepository<T>
    {
        #region Constructors and Destructors

        protected NhRepository(ISessionContainer sessionContainer)
        {
            this.SessionContainer = sessionContainer;
        }

        #endregion

        #region Properties

        protected ISessionContainer SessionContainer { get; private set; }

        #endregion

        #region Implemented Interfaces

        #region IRepository<T>

        public IEnumerable<T> GetAll()
        {
            return this.SessionContainer.CurrentSession.Query<T>().ToList();
        }

        public IEnumerable<T> GetAllMatching(Func<T, bool> filter)
        {
            return this.SessionContainer.CurrentSession.Query<T>().Where(x => filter(x)).ToList();
        }

        public T GetById(int id)
        {
            return this.SessionContainer.CurrentSession.Get<T>(id);
        }

        #endregion

        #endregion
    }
}