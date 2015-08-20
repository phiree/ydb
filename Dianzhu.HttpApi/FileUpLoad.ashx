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
        string base64File = HttpUtility.UrlDecode(context.Request.Params["filecode"]);
        base64File = base64File.Replace(" ", "+");
        context.Response.Write(FileUpload(filetype, base64File));
    }

    public string FileUpload(string filetype, string base64file)
    {
        
        byte[] bytefile = Convert.FromBase64String(base64file);
        string saveName = null;
        string newfolder = "uploads_img";
        saveName = newfolder + "/" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png";
        if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(newfolder)) == false)    //如果文件夹不存在 则创建  
        {
            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(newfolder));
        }        

        FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(saveName), FileMode.Create, FileAccess.Write);
        fs.Write(bytefile, 0, bytefile.Length);
        fs.Flush();
        fs.Close();

        return "上传文件成功";

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}