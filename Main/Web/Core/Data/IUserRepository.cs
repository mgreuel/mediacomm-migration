namespace MediaCommMVC.Core.Data
{
    #region Using Directives

    using MediaCommMVC.Core.Model;

    #endregion

    public interface IUserRepository : IRepository<MediaCommUser>
    {
        #region Public Methods

        MediaCommUser GetByName(string name);

        #endregion
    }
}