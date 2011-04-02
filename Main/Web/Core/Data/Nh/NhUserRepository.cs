namespace MediaCommMVC.Core.Data.Nh
{
    using System.Linq;

    using MediaCommMVC.Core.Model;

    using NHibernate;
    using NHibernate.Linq;

    public class NhUserRepository : NhRepository<MediaCommUser>, IUserRepository
    {
        #region Implementation of IUserRepository

        public NhUserRepository(ISession session)
            : base(session)
        {
        }

        public MediaCommUser GetByName(string userName)
        {
            return this.Session.Query<MediaCommUser>().SingleOrDefault(u => u.UserName.Equals(userName));
        }

        #endregion
    }
}