<%@ WebHandler Language="C#" Class="FileUpLoad" %>

using System;
using System.Web;
using System.IO;
using System.Text;
using System.Drawing;
public class FileUpLoad : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string filetype = context.Request.Params["filetype"].ToString();
        string base64File = HttpUtility.UrlDecode(context.Request.Params["imgData"]);
        base64File = base64File.Replace(" ", "+");
        context.Response.Write(FileUpload(filetype, base64File));
    }

    public string FileUpload(string filetype, string base64file)
    {

        byte[] bytefile = Convert.FromBase64String(base64file);
        string fileExtnsion = string.Empty;
         
        string saveName = Guid.NewGuid() + ".png";
        string filePath = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["user_avatar_image_root"]);

        FileStream fs = new FileStream(filePath+saveName, FileMode.Create, FileAccess.Write);
        fs.Write(bytefile, 0, bytefile.Length);
        fs.Flush();
        fs.Close();

        return saveName;

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}