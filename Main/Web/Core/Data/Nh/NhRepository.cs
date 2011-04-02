namespace MediaCommMVC.Core.Data.Nh
{
    #region Using Directives

    using NHibernate;

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

        public T GetById(int id)
        {
            return this.Session.Get<T>(id);
        }

        #endregion

        #endregion
    }
}