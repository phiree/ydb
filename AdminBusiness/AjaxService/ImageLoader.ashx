<%@ WebHandler Language="C#" Class="ImageLoader" %>

using System;
using System.Web;
using System.Collections.Generic;


using Ydb.Common;
       using Ydb.BusinessResource.Application;
     using Ydb.BusinessResource.DomainModel;
public class ImageLoader : IHttpHandler {

    IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
   IBusinessImageService bllBusinessImage =Bootstrap.Container.Resolve<IBusinessImageService>();

    public void ProcessRequest (HttpContext context) {
       

        context.Response.ContentType = "text/plain";

        string strBusinessId = context.Request["businessId"];
        string imageType = context.Request["imageType"];

        Business b = businessService.GetOne(new Guid(strBusinessId));

        enum_ImageType enum_imagetype = enum_ImageType.Business_Show;

        // 返回对应类型图片url
        switch (imageType) {
            case "businesslicense":
                context.Response.Write(getPathsByImageList(b.BusinessLicenses));
                context.Response.End();
                break;
            case "businessshow":
                context.Response.Write(getPathsByImageList(b.BusinessShows));
                context.Response.End();
                break;
            case "businesschargeperson":

                context.Response.Write(getPathsByImageList(b.BusinessChargePersonIdCards));
                context.Response.End();
                break;
            case "businessavater":

                context.Response.Write(getPathByImage(b.BusinessAvatar));
                context.Response.End();
                break;
            default: break;
        }

    }

    public string getPathsByImageList(IList<BusinessImage> imageList )
    {
        string JsonPaths = "[";

        foreach (BusinessImage i in imageList)
        {
            JsonPaths += (i.ToString() + ",");
        }

        JsonPaths = JsonPaths.TrimEnd(',') + "]";

        return JsonPaths;
    }

    public string getPathByImage(BusinessImage image) {
        string JsonPath = "[";

        JsonPath = JsonPath + image.ToString() + "]";

        return JsonPath;
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}