using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;

namespace MediaServer
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class FileUploader
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.MediaServer.FileUploader");
        /// <summary>
        /// 上传Base64文件,按上传的类型生成保存路径
        /// </summary>
        /// <param name="fileBase64">文件的base64编码</param>
        /// <param name="originalName">文件原名</param>
        /// <param name="localSavePathRoot">本地存储地址</param>
        /// <param name="domainType">文件的域类型</param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string Upload(string fileBase64, string originalName, string localSavePathRoot, string domainType, FileType fileType)
        {
            originalName = Path.GetFileName(originalName);
            fileBase64 = fileBase64.Replace(" ", "+");
            byte[] fileData = Convert.FromBase64String(fileBase64);
            string savedPath = string.Empty;
            string relativePath = ServerSettings.DomainPath[domainType];
            string fileName = ServerSettings.FileNameBuilder(originalName, domainType, fileType);
            string fullLocalPath = localSavePathRoot + relativePath + fileName;
            PHSuit.IOHelper.EnsureFileDirectory(fullLocalPath);
            FileStream fs = new FileStream(fullLocalPath, FileMode.Create, FileAccess.Write);
            fs.Write(fileData, 0, fileData.Length);
            fs.Flush();
            fs.Close();

            if(fileType== FileType.voice)
            {
                string targetFileName = localSavePathRoot + relativePath + fileName + ".mp3";
                PHSuit.IOHelper.EnsureFileDirectory(targetFileName);
                PHSuit.MediaConvert tomp3 = new PHSuit.MediaConvert();
                tomp3.ConvertToMp3(HttpContext.Current.Server.MapPath("\\files\\"), fullLocalPath, targetFileName);
                log.Debug("appliacitonPath:" + HttpContext.Current.Server.MapPath("\\files\\"));
                log.Debug("fullLocalPath:" + fullLocalPath);
                log.Debug("targetFileName:" + targetFileName);
            }

            return fileName;
        }

        /// <summary>
        /// 上传Base64文件,按上传日期生成保存路径
        /// </summary>
        /// <param name="fileBase64"></param>
        /// <param name="originalName"></param>
        /// <param name="localSavePathRoot"></param>
        /// <param name="dateToday"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string Upload(string fileBase64, string originalName, string localSavePathRoot, DateTime dateToday, FileType fileType)
        {
            //originalName = Path.GetFileName(originalName);
            //fileBase64 = fileBase64.Replace(" ", "+");
            //byte[] fileData = Convert.FromBase64String(fileBase64);
            //string savedPath = string.Empty;
            //string relativePath = dateToday.Year.ToString() + "\\" + dateToday.Month.ToString() + "\\" + dateToday.Day.ToString() + "\\";
            //string fileName = ServerSettings.FileNameBuilder(originalName, dateToday, fileType);
            //string fullLocalPath = localSavePathRoot + relativePath + fileName;
            //PHSuit.IOHelper.EnsureFileDirectory(fullLocalPath);
            //FileStream fs = new FileStream(fullLocalPath, FileMode.Create, FileAccess.Write);

            //fs.Write(fileData, 0, fileData.Length);
            //fs.Flush();
            //fs.Close();

            //if (fileType == FileType.voice)
            //{
            //    string targetFileName = localSavePathRoot + relativePath + fileName + ".mp3";
            //    PHSuit.IOHelper.EnsureFileDirectory(targetFileName);
            //    PHSuit.MediaConvert tomp3 = new PHSuit.MediaConvert();
            //    tomp3.ConvertToMp3(HttpContext.Current.Server.MapPath("\\files\\"), fullLocalPath, targetFileName);
            //    log.Debug("appliacitonPath:" + HttpContext.Current.Server.MapPath("\\files\\"));
            //    log.Debug("fullLocalPath:" + fullLocalPath);
            //    log.Debug("targetFileName:" + targetFileName);
            //}
            string strR = GetMediaInfo.GetMediaInfoByType("G:\\IHelper_Server\\MediaServerWeb\\media\\2016\\7\\29\\5_$_f2e21016-d6f3-43a0-b26e-af9cc9340df6_$_2016_$_7_$_29_$_image", fileType, originalName);
            return strR;// +fileName;
        }

        /// <summary>
        /// 通过URl上传文件(一般为网络文件),按上传的类型生成保存路径
        /// </summary>
        /// <param name="url"></param>
        /// <param name="originalName"></param>
        /// <param name="localSavePathRoot"></param>
        /// <param name="domainType"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string UploadFromUrl(string url, string originalName, string localSavePathRoot, string domainType, FileType fileType)
        {
            originalName = Path.GetFileName(originalName);
            var client = new WebClient();
            byte[] fileData = client.DownloadData(url); ; 
            string savedPath = string.Empty;
            string relativePath = ServerSettings.DomainPath[domainType];
            string fileName = ServerSettings.FileNameBuilder(originalName, domainType, fileType);
            string fullLocalPath = localSavePathRoot + relativePath + fileName;
            PHSuit.IOHelper.EnsureFileDirectory(fullLocalPath);
            FileStream fs = new FileStream(fullLocalPath, FileMode.Create, FileAccess.Write);
            fs.Write(fileData, 0, fileData.Length);
            fs.Flush();
            fs.Close();
            return fileName;
        }

        /// <summary>
        /// 通过URl上传文件(一般为网络文件),按上传日期生成保存路径
        /// </summary>
        /// <param name="url"></param>
        /// <param name="originalName"></param>
        /// <param name="localSavePathRoot"></param>
        /// <param name="dateToday"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string UploadFromUrl(string url, string originalName, string localSavePathRoot, DateTime dateToday, FileType fileType)
        {
            originalName = Path.GetFileName(originalName);
            var client = new WebClient();
            byte[] fileData = client.DownloadData(url); ;
            string savedPath = string.Empty;
            string relativePath = dateToday.Year.ToString() + "\\" + dateToday.Month.ToString() + "\\" + dateToday.Day.ToString() + "\\";
            string fileName = ServerSettings.FileNameBuilder(originalName, dateToday, fileType);
            string fullLocalPath = localSavePathRoot + relativePath + fileName;
            PHSuit.IOHelper.EnsureFileDirectory(fullLocalPath);
            FileStream fs = new FileStream(fullLocalPath, FileMode.Create, FileAccess.Write);
            fs.Write(fileData, 0, fileData.Length);
            fs.Flush();
            fs.Close();

            string strR = GetMediaInfo.GetMediaInfoByType(fullLocalPath, fileType, originalName);
            return strR +  fileName;
        }
    }
}



 
