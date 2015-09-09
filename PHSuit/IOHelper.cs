using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
namespace PHSuit
{
    public class IOHelper
    {

        public static string[] ReadAllLinesFromFile(string filePath)
        {

            FileInfo file = EnsureFile(filePath);
            string[] result = File.ReadAllLines(filePath);

            return result;
        }
        /// <summary>
        /// 保证文件存在
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static FileInfo EnsureFile(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);
            if (fi.Exists)
            {
                return fi;
            }
            EnsureFileDirectory(filepath);
            FileStream fs = fi.Create();
            fs.Close();
            return fi;


        }

        /// <summary>
        /// 保证路径中的目录都存在
        /// </summary>
        /// <param name="filePath"></param>
        public static void EnsureFileDirectory(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
           
            string directory = fi.Directory.FullName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                
            }
        }
        public static DirectoryInfo EnsureDirectory(string directory)
        {
            DirectoryInfo dir = new DirectoryInfo(directory);
            if (!dir.Exists)
            {
                dir = Directory.CreateDirectory(directory);

            }
            return dir;
        }
        public static string EnsureFoldEndWithSlash(string directoryPath)
        {
            if (!directoryPath.EndsWith("\\"))
            {
                return directoryPath + "\\";
            }
            else return directoryPath;
        }

        /// <summary>
        /// 使用http方式 上传一个base64编码过的文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string UploadFileHttp(string url, string base64,byte[] fileBytes,string fileExteion)
        {
            string uploadedPath = string.Empty;
            System.Net.WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = fileBytes.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(fileBytes,0,fileBytes.Length);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader sr = new StreamReader(data);
            string result = sr.ReadToEnd();
            return result;
            
        }
        /// <summary>
        /// 保存base64编码过的文件
        /// </summary>
        /// <param name="base64">编码</param>
        /// <param name="targetFullFileName">完整的本地保存路径</param>
 
        public static void SaveFileFromBase64(string base64, string targetFullFileName)
        {
            byte[] bytefile = Convert.FromBase64String(base64);
            FileStream fs = new FileStream(targetFullFileName, FileMode.Create, FileAccess.Write);
            fs.Write(bytefile, 0, bytefile.Length);
            fs.Flush();
            fs.Close();
 
        }
    }


    public static class ExtentedMethoed
    {
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, SearchOption searchOption, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = dir.GetFiles("*", searchOption);
            return files.Where(f => extensions.Contains(f.Extension.ToLower()));
        }
        public static IEnumerable<FileInfo> GetImageFiles(this DirectoryInfo dir,SearchOption searchOption)
        {
            return GetFilesByExtensions(dir, searchOption, new string[]{".bmp"
                                                        ,".gif"
                                                        ,".jpg"
                                                        ,".png"
                                                        ,".psd"
                                                        ,".pspimage"
                                                        ,".thm"
                                                        ,".tif"
                                                        ,".yuv"});
        }
        public static IEnumerable<FileInfo> GetImageFiles(this DirectoryInfo dir)
        {
            return GetFilesByExtensions(dir,SearchOption.AllDirectories, new string[]{".bmp"
                                                        ,".gif"
                                                        ,".jpg"
                                                        ,".png"
                                                        ,".psd"
                                                        ,".pspimage"
                                                        ,".thm"
                                                        ,".tif"
                                                        ,".yuv"});
        }
    }


}
