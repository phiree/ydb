<%@ WebHandler Language="C#" Class="ImageDelete" %>

using System;
using System.Web;

 using Ydb.Common;
       using Ydb.BusinessResource.Application;
     using Ydb.BusinessResource.DomainModel;
public class ImageDelete : IHttpHandler{

   IBusinessImageService bllBusinessImage =Bootstrap.Container.Resolve<IBusinessImageService>();

    public void ProcessRequest(HttpContext context) {

      

        context.Response.ContentType = "text/plain";

        string imageName = context.Request["imageName"];

        if (bllBusinessImage.DeleteBusImageByName(imageName)) {
            context.Response.Write("success");
        } else {
            context.Response.StatusCode = 400;
            context.Response.Write("error");
        }



    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}