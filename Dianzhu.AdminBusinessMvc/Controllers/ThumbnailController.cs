using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Web.Security;
using PHSuit;
using System.Web.Hosting;

namespace Dianzhu.AdminBusinessMvc.Controllers
{
    public class ThumbnailController : Controller
    {
        public ActionResult Index(string imageName,int width,int height)
        {
            //string imageName = Request.Params["imagename"];
            //int width = Convert.ToInt32(paramWidth);
            //int height = Convert.ToInt32(paramHeight);
            //string paramWidth = Request.Params["width"];
            //string paramHeight = Request.Params["height"];
            //string paramType = Request.Params["scalingType"];

            string physicalPath = HostingEnvironment.MapPath("/media/business/");
            
            ThumbnailType tt = ThumbnailType.GeometricScalingByWidth;// (NBiz.ThumbnailType)Enum.Parse(typeof(NBiz.ThumbnailType), paramType);
            string thumbnailName = ThumbnailMaker.Make(physicalPath + "original\\", physicalPath + "thumbnail\\", imageName, width, height, tt);
            if (string.IsNullOrEmpty(thumbnailName)) { return null; }
             Response.ContentType = "image/png";
            // context.Response.TransmitFile(physicalPath + imageName);
             Response.WriteFile(thumbnailName);
             return null;

        }
    }
}
