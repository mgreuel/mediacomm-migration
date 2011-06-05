namespace MediaCommMVC.Web.Core.Infrastructure
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using StructureMap;

    #endregion

    public class SmDependencyResolver : IDependencyResolver
    {
        #region Constants and Fields

        private readonly IContainer container;

        #endregion

        #region Constructors and Destructors

        public SmDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Implemented Interfaces

        #region IDependencyResolver

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }


            return serviceType.IsAbstract || serviceType.IsInterface
                       ? this.container.TryGetInstance(serviceType)
                       : this.container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.GetAllInstances(serviceType).Cast<object>();
        }

        #endregion

        #endregion
    }
}