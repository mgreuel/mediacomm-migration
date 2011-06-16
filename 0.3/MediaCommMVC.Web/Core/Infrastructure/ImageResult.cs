using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.Common.Exceptions;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class ImageResult : ActionResult
    {
        public Image Image { get; set; }

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
    }
}
