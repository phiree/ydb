using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.Web.Security;
namespace Dianzhu.AdminBusinessMvc.Controllers
{
    public class ImageHandlerController : Controller
    {
        BLLBusinessImage bllBusinessImage = new BLLBusinessImage();
        [HttpPost]
        public ActionResult Upload(Guid businessId, string imageType)
        {
            
            Response.ContentType = "text/plain";

            HttpFileCollectionBase files =  Request.Files;
          
             
            enum_ImageType enum_imagetype = enum_ImageType.Business_Show;
            switch (imageType)
            {
                case "businesslicense":
                    enum_imagetype = enum_ImageType.Business_License;
                    break;
                case "businessshow":
                    enum_imagetype = enum_ImageType.Business_Show;
                    break;
                case "businesschargeperson":
                    enum_imagetype = enum_ImageType.Business_ChargePersonIdCard;
                    break;
                case "businessavater":
                    enum_imagetype = enum_ImageType.Business_Avatar;
                    break;
                default: break;
            }
            HttpPostedFileBase posted = files["upload_file"];

            string savedPath = bllBusinessImage.Save(businessId, posted, enum_imagetype);

            Response.Write(savedPath);
            return null;
        }
    }
}
