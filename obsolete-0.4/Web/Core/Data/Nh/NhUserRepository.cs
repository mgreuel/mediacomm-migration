namespace MediaCommMVC.Core.Data.Nh
{
    #region Using Directives

    using System.Linq;

    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Model;

    using NHibernate.Linq;

    #endregion

    public class NhUserRepository : NhRepository<MediaCommUser>, IUserRepository
    {
        #region Constructors and Destructors

        public NhUserRepository(ISessionContainer sessionContainer)
            : base(sessionContainer)
        {
        }

        #endregion

        #region Implemented Interfaces

        #region IUserRepository

        public MediaCommUser GetByName(string userName)
        {
            return this.SessionContainer.CurrentSession.Query<MediaCommUser>().SingleOrDefault(u => u.UserName.Equals(userName));
        }

        #endregion

        #endregion
    }
}