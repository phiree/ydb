<%@ WebHandler Language="C#" Class="FileUploader" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
public class FileUploader : IHttpHandler {

    BLLBusinessImage bllBusinessImage = new BLLBusinessImage();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        HttpFileCollection files = context.Request.Files;
    
        string strBusinessId = context.Request["businessId"];
        string imageType = context.Request["imageType"];
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
        HttpPostedFileBase posted = new HttpPostedFileWrapper(context.Request.Files["upload_file"]);
          string savedPath=  bllBusinessImage.Save(new Guid(strBusinessId), posted, enum_imagetype);

          context.Response.Write(savedPath + posted.FileName);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}