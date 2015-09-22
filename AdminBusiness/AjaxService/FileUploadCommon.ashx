<%@ WebHandler Language="C#" Class="FileUploadCommon" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
/// <summary>
/// 接受来自其他平台的
/// </summary>
public class FileUploadCommon : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        // string extension=  context.Request["ext"];
        Stream s= context.Request.InputStream;
        StreamReader sr = new StreamReader(s);
        string dd = sr.ReadToEnd();

        Image img = Image.FromStream(s);
        string upload_filename ="/media/chat/original/"+  Guid.NewGuid() + ".jpg";
        
        img.Save(context.Server.MapPath(upload_filename));
        context.Response.Write(upload_filename);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}