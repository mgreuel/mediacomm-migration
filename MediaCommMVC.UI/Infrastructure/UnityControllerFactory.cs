#region Using Directives

using System;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Practices.Unity;

#endregion

namespace MediaCommMVC.UI.Infrastructure
{
    /// <summary>A controller factory using Unity for the creation of controller.</summary>
    public class UnityControllerFactory : DefaultControllerFactory
    {
        #region Public Methods

        /// <summary>Creates the controller.</summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns>A reference to the controller.</returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            Type type = this.GetControllerType(requestContext, controllerName);

            if (type == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Could not find a controller with the name {0}. Check if the file {1} exists", 
                        controllerName, 
                        requestContext.HttpContext.Request.Url));
            }

            IUnityContainer container = this.GetContainer(requestContext);
            return (IController)container.Resolve(type);
        }

        #endregion

        #region Methods

        /// <summary>Gets the unity container.</summary>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The unity container.</returns>
        protected virtual IUnityContainer GetContainer(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            var unityContainerAccessor = requestContext.HttpContext.ApplicationInstance as IUnityContainerAccessor;
            if (unityContainerAccessor == null)
            {
                throw new InvalidOperationException(
                    "You must extend the HttpApplication in your web project and implement the IContainerAccessor to properly expose your container instance");
            }

            IUnityContainer container = unityContainerAccessor.Container;
            if (container == null)
            {
                throw new InvalidOperationException(
                    "The container seems to be unavailable in your HttpApplication subclass");
            }

            return container;
        }

        #endregion
    }
}