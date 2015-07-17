<%@ WebHandler Language="C#" Class="FileUploader" %>

using System;
using System.Web;

public class FileUploader : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        HttpFileCollection files = context.Request.Files;
        string savePath = "/media/";
        string physicalPath = context.Server.MapPath(savePath);
         
            HttpPostedFile posted = files[0];
            
            
            posted.SaveAs(physicalPath+ posted.FileName);

            context.Response.Write(savePath + posted.FileName);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}