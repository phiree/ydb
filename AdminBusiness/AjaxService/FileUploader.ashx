<%@ WebHandler Language="C#" Class="FileUploader" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.Common;
    using Ydb.BusinessResource.Application;
     using Ydb.BusinessResource.DomainModel;
public class FileUploader : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
   IBusinessImageService bllBusinessImage =Bootstrap.Container.Resolve<IBusinessImageService>();
    public void ProcessRequest (HttpContext context) {
        //权限判断
        if (!AjaxAuth.authAjaxUser(context)){
            context.Response.StatusCode = 400;
            context.Response.Clear();
            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"unlogin\"}");
            return;
        }

        if (NHibernateUnitOfWork.UnitOfWork.IsStarted)
        {
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);

        }
        NHibernateUnitOfWork.UnitOfWork.Start();

        context.Response.ContentType = "text/plain";

        HttpFileCollection files = context.Request.Files;

        string strBusinessId = context.Request["businessId"];
        Business b = businessService.GetOne(new Guid(strBusinessId));
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

        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();


    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}