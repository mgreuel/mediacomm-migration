#region Using Directives

using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.Common.Exceptions;

#endregion

namespace MediaCommMVC.Web.Core.Infrastructure
{
    /// <summary>
    ///   Writes an image to the response stream.
    /// </summary>
    public class ImageResult : ActionResult
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Enables processing of the result of an action method by a custom type that inherits from <see cref = "T:System.Web.Mvc.ActionResult" />.
        /// </summary>
        /// <param name = "context">The context within which the result is executed.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (this.Image == null)
            {
                throw new MediaCommException("The Image must not be null");
            }
  
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "image/jpeg";
            context.HttpContext.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.HttpContext.Response.Cache.SetExpires(Cache.NoAbsoluteExpiration);

            this.Image.Save(context.HttpContext.Response.OutputStream, ImageFormat.Jpeg);
        }

        #endregion
    }
}
