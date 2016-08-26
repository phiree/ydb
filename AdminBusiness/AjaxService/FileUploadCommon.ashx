<%@ WebHandler Language="C#" Class="FileUploadCommon" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
/// <summary>
/// 接受来自其他平台的
/// </summary>
public class FileUploadCommon : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest (HttpContext context) {
        //权限判断
        if (context.Session["UserName"]==null)
        {
            context.Response.Write("{\"result\":\""+false+"\",\"msg\":\"unlogin\"}");
            return;
        }

        context.Response.ContentType = "text/plain";
        // string extension=  context.Request["ext"];
        Stream s= context.Request.InputStream;
        StreamReader sr = new StreamReader(s);
        string dd = sr.ReadToEnd();

        Image img = Image.FromStream(s);
        string upload_filename ="/media/chat/original/"+  Guid.NewGuid() + "";
        
        img.Save(context.Server.MapPath(upload_filename));
        context.Response.Write(upload_filename);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}