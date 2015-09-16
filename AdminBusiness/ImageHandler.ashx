<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using PHSuit;
using System.Text.RegularExpressions;
/// <summary>
/// 根据需要制作/显示需要的图片
/// </summary>
public class ImageHandler : IHttpHandler {

   // NBiz.ThumbnailMaker thumbnailMaker = new NBiz.ThumbnailMaker();
    public void ProcessRequest (HttpContext context) {

        context.Response.ContentType = "image/png";
        string imageName =context.Request["imagename"];
 
        string regp=@"(_(\d+)[x|X](\d+))\.";
        Regex re = new Regex(regp);
        Match regexMatch = re.Match(imageName);

        string paramWidth = context.Request["width"];
        string paramHeight = context.Request["height"];
        string paramType = context.Request["tt"];

        if (regexMatch.Success)
        {
            paramWidth = regexMatch.Groups[2].Value;
            paramHeight = regexMatch.Groups[3].Value;
            imageName = imageName.Replace(regexMatch.Groups[1].Value, string.Empty);
            paramType = "3";

        } string physicalPath = context.Server.MapPath(Config.BusinessImagePath);
        if (paramWidth == null||paramHeight==null)
        {
            context.Response.WriteFile(physicalPath + "original\\" + imageName);
        }
        else
        {


            int width = Convert.ToInt32(paramWidth);
            int height = Convert.ToInt32(paramHeight);
            ThumbnailType tt = ThumbnailType.GeometricScalingByHeight;
            try
            {
                tt = (ThumbnailType)Convert.ToInt32(paramType);
            }
            catch { }
            string thumbnailName = ThumbnailMaker.Make(physicalPath + "original\\", physicalPath + "thumbnail\\", imageName, width, height, tt);
            if (string.IsNullOrEmpty(thumbnailName)) { return; }
            context.Response.WriteFile(thumbnailName);
        }
      // context.Response.TransmitFile(physicalPath + imageName);
       
      //  context.Response.WriteFile(@"E:\workspace\code\resources\VirtualDirectory\NTSBase\ProductImages\1080271.JPG");
    }
    
 
    
     
    public bool IsReusable {
        get {
            return false;
        }
    }

}