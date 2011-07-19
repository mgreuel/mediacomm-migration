#region Using Directives

using System.Web;

using MediaCommMVC.Core.Model.Users;

#endregion

namespace MediaCommMVC.UI.Infrastructure
{
    /// <summary>Provides static access to context variables.</summary>
    public static class WebContext
    {
        #region Properties

        /// <summary>Gets or sets the current user.</summary>
        /// <value>The current user.</value>
        public static MediaCommUser CurrentUser
        {
            get
            {
                return (MediaCommUser)HttpContext.Current.Items["currentUser"];
            }

            set
            {
                HttpContext.Current.Items["currentUser"] = value;
            }
        }

        #endregion
    }
}