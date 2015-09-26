<%@ WebHandler Language="C#" Class="GetFile" %>

using System;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

using System.Collections.Generic;
using MediaServer;
using System.Linq;
public class GetFile : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var request = context.Request;
        string originalFileName = request["fileName"];

        string mediaRootDir = context.Server.MapPath("/media/");

        var geter = new MediaServer.FileGetter(originalFileName, mediaRootDir);

        FileType fileType;
        string relativeFileName = geter.GetRelativeFileName(out fileType);
        context.Response.ContentType = "text/plain";
        switch (fileType)
        {
            case MediaServer.FileType.image:
                context.Response.ContentType = "image/png";

                break;
            case MediaServer.FileType.audio:
                context.Response.ContentType = "audio/mpeg";

                break;
            case FileType.video:  context.Response.ContentType = "video/x-msvideo";break;
        }
        string returnFilePath = mediaRootDir + relativeFileName.TrimStart('/').Replace("/", "\\");
        //  context.Response.Redirect("/media/" + relativeFileName, false);
        context.Response.WriteFile(returnFilePath);

    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}