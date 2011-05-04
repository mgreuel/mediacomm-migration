namespace MediaCommMVC.Core.Data.Nh
{
    #region Using Directives

    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Model.Forums;

    #endregion

    public class NhForumsRepository : NhRepository<Forum>, IForumsRepository
    {
        #region Constructors and Destructors

        public NhForumsRepository(ISessionContainer sessionContainer)
            : base(sessionContainer)
        {
        }

        #endregion
    }
}