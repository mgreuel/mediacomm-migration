namespace MediaCommMVC.Web.Core.Infrastructure
{
    #region Using Directives

    using NHibernate;

    #endregion

    public interface ISessionContainer
    {
        #region Properties

        ISession CurrentSession { get; set; }

        #endregion
    }
}