using NHibernate;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public interface ISessionContainer
    {
        ISession CurrentSession { get; set; }
    }
}