<%@ WebHandler Language="C#" Class="FileUploader" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
public class FileUploader : IHttpHandler {

    BLLBusiness bllBusiness = new BLLBusiness();
    BLLBusinessImage bllBusinessImage = new BLLBusinessImage();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        HttpFileCollection files = context.Request.Files;
    
        string strBusinessId = context.Request["businessId"];
        Business b = bllBusiness.GetOne(new Guid(strBusinessId));
        string imageType = context.Request["imageType"];
        enum_ImageType enum_imagetype = enum_ImageType.Business_Show;
        
        switch (imageType)
        {
            case "businesslicense":
                enum_imagetype = enum_ImageType.Business_License;
                if (b.BusinessLicenses.Count >= 2)
                {
                    context.Response.Write("F,营业执照不能超过2张");
                    context.Response.End();
                }
                break;
            case "businessshow":
                enum_imagetype = enum_ImageType.Business_Show;
                if (b.BusinessShows.Count >=6)
                {
                    context.Response.Write("F,展示图片不能超过6张");
                    context.Response.End();
                }
                break;
            case "businesschargeperson":
                enum_imagetype = enum_ImageType.Business_ChargePersonIdCard;
                if (b.BusinessChargePersonIdCards.Count >= 2)
                {
                    context.Response.Write("F,身份证照片不能超过2张");
                    context.Response.End();
                }
                break;
            case "businessavater":
                enum_imagetype = enum_ImageType.Business_Avatar;
                break;
            default: break;
        }
        HttpPostedFileBase posted = new HttpPostedFileWrapper(context.Request.Files["upload_file"]);
        if (posted.ContentLength > 2 * 1024 * 1024)
        {
            context.Response.Write("F,图片大小不能超过2M");
            context.Response.End();
        }
          string imagePath=  bllBusinessImage.Save(new Guid(strBusinessId), posted, enum_imagetype);

          context.Response.Write(imagePath);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}