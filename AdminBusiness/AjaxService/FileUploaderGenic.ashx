<%@ WebHandler Language="C#" Class="FileUploader" %>

using System;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.IO;
public class FileUploader : IHttpHandler {

    BLLBusinessImage bllBusinessImage = new BLLBusinessImage();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        HttpFileCollection files = context.Request.Files;

        if (context.Request.Files.AllKeys.Length == 0)
        {
            context.Response.Write("{}");
            context.Response.Flush();
            context.Response.End();
        }
        for (int i=0;i<files.Count;i++)
        {
            var posted = files[i];
            string savePath = context.Server.MapPath("/media/generic_files/");
            string fileName = Guid.NewGuid() + posted.FileName;
            string fullName = savePath + fileName;
            PHSuit.IOHelper.EnsureFileDirectory(fullName);
            posted.SaveAs(fullName);
           
        }
        context.Response.Write("ok");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}