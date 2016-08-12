using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PHSuit
{
    public class DownloadSoft
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("PHSuit.DownloadSoft");
        public static readonly string DownloadPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Download\";
        public DownloadSoft()
        {

        }

        public static bool DownLoad(string version,string urlPath,string fileName)
        {
            bool flag = false;

            string path = DownloadPath.Remove(DownloadPath.Length - 1);

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fs = new FileStream(DownloadPath + fileName, FileMode.Create, FileAccess.Write))
                {
                    //创建请求
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPath);
                    //接收响应
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //输出流
                    Stream responseStream = response.GetResponseStream();
                    byte[] bufferBytes = new byte[10000];//缓冲字节数组
                    int bytesRead = -1;
                    while ((bytesRead = responseStream.Read(bufferBytes, 0, bufferBytes.Length)) > 0)
                    {
                        fs.Write(bufferBytes, 0, bytesRead);
                    }
                    if (fs.Length > 0)
                    {
                        flag = true;
                    }
                    //关闭写入
                    fs.Flush();
                    fs.Close();
                }
            }
            catch(Exception e)
            {
                PHSuit.ExceptionLoger.ExceptionLog(log, e);
            }

            return flag;
        }
    }
}
