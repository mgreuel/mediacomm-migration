namespace MediaCommMVC.Core.Infrastructure
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;

    #endregion

    public class UnityDependencyResolver : IDependencyResolver
    {
        #region Constants and Fields

        private readonly IUnityContainer container;

        #endregion

        #region Constructors and Destructors

        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Implemented Interfaces

        #region IDependencyResolver

        public object GetService(Type serviceType)
        {
            return this.container.IsRegistered(serviceType) ? this.container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.IsRegistered(serviceType) ? this.container.ResolveAll(serviceType) : new List<object>();
        }

        #endregion

        #endregion
    }
}