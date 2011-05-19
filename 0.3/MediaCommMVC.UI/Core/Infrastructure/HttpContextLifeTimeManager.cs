#region Using Directives

using System;
using System.Web;

using Microsoft.Practices.Unity;

#endregion

namespace MediaCommMVC.Web.Core.Infrastructure
{
    /// <summary>
    ///   The http context lifetime manager.
    /// </summary>
    /// <typeparam name = "T">The type of the object to be stored in the HttpContext.</typeparam>
    public sealed class HttpContextLifetimeManager<T> : LifetimeManager, IDisposable
    {
        #region Public Methods

        /// <summary>
        ///   Gets the value from the current HttpContext.
        /// </summary>
        /// <returns>The get value.</returns>
        public override object GetValue()
        {
            return HttpContext.Current.Items[typeof(T).AssemblyQualifiedName];
        }

        /// <summary>
        ///   Removes the value from the current HttpContext.
        /// </summary>
        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove(typeof(T).AssemblyQualifiedName);
        }

        /// <summary>
        ///   Sets the value in the current HttpContext.
        /// </summary>
        /// <param name = "value">The new value.</param>
        public override void SetValue(object value)
        {
            HttpContext.Current.Items[typeof(T).AssemblyQualifiedName] = value;
        }

        #endregion

        #region Implemented Interfaces

        #region IDisposable

        /// <summary>
        ///   Removes the item from the HttpContext.
        /// </summary>
        public void Dispose()
        {
            this.RemoveValue();
        }

        #endregion

        #endregion
    }
}