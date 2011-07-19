namespace MediaCommMVC.Core.Infrastructure
{
    #region Using Directives

    using NHibernate;

    #endregion

    public interface ISessionContainer
    {
        #region Properties

        ISession CurrentSession { get; }

        #endregion
    }
}