#region Using Directives

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mvc;

#endregion

namespace MediaCommMVC.UI.Infrastructure
{
    /// <summary>Writes an image to the response stream.</summary>
    public class ImageResult : ActionResult
    {
        #region Properties

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        public Image Image { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Enables processing of the result of an action method by a custom type that inherits from <see cref="T:System.Web.Mvc.ActionResult"/>.</summary>
        /// <param name="context">The context within which the result is executed.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (this.Image == null)
            {
                throw new ArgumentNullException("Image");
            }
  
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "image/jpeg";

            this.Image.Save(context.HttpContext.Response.OutputStream, ImageFormat.Jpeg);
        }

        #endregion
    }
}
