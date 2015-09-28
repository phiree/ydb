<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;
using MediaServer;
public class UploadFile : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        var request = context.Request;
        try
        {
            string fileBase64 = request["fileBase64"];
            string originalFileName = request["originalName"];
            string domainType = request["domainType"];
            string fileType = request["fileType"];

            string localSavePathRoot = context.Server.MapPath("/media/");


            string savedFilename = FileUploader.Upload(fileBase64, originalFileName ?? string.Empty, localSavePathRoot, domainType
                , (FileType)Enum.Parse(typeof(FileType), fileType));
            context.Response.Write(savedFilename);
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}