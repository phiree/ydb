<%@ WebHandler Language="C#" Class="DianzhuApi" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Web.SessionState;
public class DianzhuApi : IHttpHandler,IRequiresSessionState
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public void ProcessRequest(HttpContext context)
    {

           NHibernateUnitOfWork.UnitOfWork.Start();
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;
        string jsonStr = new StreamReader(context.Request.InputStream).ReadToEnd();
        Guid rid = Guid.NewGuid();
        ilog.Debug("Request("+rid+"):"+ PHSuit.JsonHelper.FormatJson( jsonStr));
        BaseRequest request = JsonConvert.DeserializeObject<BaseRequest>(jsonStr);

        ResponseFactory responseFactory = Bootstrap.Container.Resolve<ResponseFactory>();
        BaseResponse response= responseFactory.GetApiResponse(request);
        string jsonResponse = response.BuildJsonResponse();
        context.Response.Write(jsonResponse);
        ilog.Debug("Resonse("+rid+"):"+PHSuit.JsonHelper.FormatJson(jsonResponse));
        if(NHibernateUnitOfWork.UnitOfWork.IsStarted)
        { 
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();}
    }
    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }
}