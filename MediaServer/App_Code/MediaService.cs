using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
/// <summary>
/// Summary description for MediaServer
/// </summary>
public class MediaService
{
    //资源文件存放地址根目录
    public static string MediaRoot = ConfigurationManager.AppSettings["mediaroot"];
    //资源文件访问根地址 http
    public static string MediaUrl = ConfigurationManager.AppSettings["mediaurl"];
    /// <summary>
    /// 媒体文件服务器.
    /// </summary>
    public MediaService()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Upload(string originalFileName)
    {
        return string.Empty;
    }
  
}
/// <summary>
/// 上传资源.
/// </summary>
public class UploaderBase64:BaseUploader
{
    public string Base64File {
        set {
            bytefile = Convert.FromBase64String(value);
        }
    }
    
}
public  class BaseUploader
{
   public string originalFileName { get; set; }
    public string savedFileName { get; set; }
    public string targetSavePath { get; set; }
    protected byte[] bytefile { get; set; }
     
    
    protected void Upload()
    {
        if (bytefile == null)
            throw new Exception("must implentioned");
        FileStream fs = new FileStream(targetSavePath + savedFileName, FileMode.Create, FileAccess.Write);
        fs.Write(bytefile, 0, bytefile.Length);
        fs.Flush();
        fs.Close();
    }
}
public interface FileConverter
{
    
}