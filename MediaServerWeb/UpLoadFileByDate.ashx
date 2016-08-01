<%@ WebHandler Language="C#" Class="UpLoadFileByDate" %>

using System;
using System.Web;
using MediaServer;

public class UpLoadFileByDate : IHttpHandler {
    public void ProcessRequest(HttpContext context)
    {
        var request = context.Request;
        try
        {
            string fileBase64 = request["fileBase64"];
            string fileUrl =HttpUtility.UrlDecode(request["fileUrl"]);
            string originalFileName = request["originalName"];
            string fileType = request["fileType"];
            string localSavePathRoot = context.Server.MapPath("/media/");
            string savedFilename = "";
            if (string.IsNullOrEmpty (fileUrl))
            {
                savedFilename = FileUploader.Upload(fileBase64, originalFileName ?? string.Empty, localSavePathRoot, DateTime.Now, (FileType)Enum.Parse(typeof(FileType), fileType));
            }
            else
            {
                savedFilename = FileUploader.UploadFromUrl(fileUrl, originalFileName ?? string.Empty, localSavePathRoot, DateTime.Now, (FileType)Enum.Parse(typeof(FileType), fileType));
            }
            context.Response.Write(savedFilename);
        }
        catch (Exception ex)
        {
            //context.Response.StatusCode = 400;
            context.Response.Write("上传失败！" + ex.Message);
            Console.WriteLine(ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}