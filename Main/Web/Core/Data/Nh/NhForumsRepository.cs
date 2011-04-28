namespace MediaCommMVC.Core.Data.Nh
{
    #region Using Directives

    using MediaCommMVC.Core.Model;

    using NHibernate;

    #endregion

    public class NhForumsRepository : NhRepository<Forum>, IForumsRepository
    {
        #region Constructors and Destructors

        public NhForumsRepository(ISession session)
            : base(session)
        {
        }

        #endregion
    }
}