#region Using Directives

using Microsoft.Practices.Unity;

#endregion

namespace MediaCommMVC.UI.Infrastructure
{
    /// <summary>
    ///   Provides access to the unity container.
    /// </summary>
    public interface IUnityContainerAccessor
    {
        #region Properties

        /// <summary>
        ///   Gets the unity container.
        /// </summary>
        /// <value>The unity container.</value>
        IUnityContainer Container { get; }

        #endregion
    }
}