#region Using Directives

using NHibernate;

#endregion

namespace MediaCommMVC.Web.Core.Data.NHInfrastructure
{
    /// <summary>Common interface for NHIbernate session managers.</summary>
    public interface ISessionManager
    {
        #region Properties

        /// <summary>Gets the NHibernate session.</summary>
        /// <value>The NHibernate session.</value>
        ISession Session { get; }

        /// <summary>Gets the NHibernate session factory.</summary>
        /// <value>The NHibernate session factory.</value>
        ISessionFactory SessionFactory { get; }

        #endregion
    }
}