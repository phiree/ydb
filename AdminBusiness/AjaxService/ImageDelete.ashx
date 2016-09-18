<%@ WebHandler Language="C#" Class="ImageDelete" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;

public class ImageDelete : IHttpHandler{

    BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
    BLLBusinessImage bllBusinessImage = Bootstrap.Container.Resolve<BLLBusinessImage>();

    public void ProcessRequest(HttpContext context) {

        if (NHibernateUnitOfWork.UnitOfWork.IsStarted)
        {
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);

        }
        NHibernateUnitOfWork.UnitOfWork.Start();

        context.Response.ContentType = "text/plain";

        string imageName = context.Request["imageName"];

        if (bllBusinessImage.DeleteBusImageByName(imageName)) {
            context.Response.Write("success");
        } else {
            context.Response.StatusCode = 400;
            context.Response.Write("error");
        }


        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}