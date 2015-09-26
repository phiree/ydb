<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;
using MediaServer;
public class UploadFile : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        var request = context.Request;

        string fileBase64 = request["fileBase64"];
        string originalFileName = request["originalName"];
        string domainType = request["domainType"];
        string fileType = request["fileType"];

         string localSavePathRoot = context.Server.MapPath("/media/");

       
        string savedFilename= FileUploader.Upload(fileBase64, originalFileName??string.Empty, localSavePathRoot, domainType
            , ( FileType)Enum.Parse(typeof( FileType), fileType));
        context.Response.Write(savedFilename);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}