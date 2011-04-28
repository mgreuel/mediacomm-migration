namespace MediaCommMVC.Core.Data.Nh
{
    #region Using Directives

    using System.Linq;

    using MediaCommMVC.Core.Model;

    using NHibernate;
    using NHibernate.Linq;

    #endregion

    public class NhUserRepository : NhRepository<MediaCommUser>, IUserRepository
    {
        #region Constructors and Destructors

        public NhUserRepository(ISession session)
            : base(session)
        {
        }

        #endregion

        #region Implemented Interfaces

        #region IUserRepository

        public MediaCommUser GetByName(string userName)
        {
            return this.Session.Query<MediaCommUser>().SingleOrDefault(u => u.UserName.Equals(userName));
        }

        #endregion

        #endregion
    }
}