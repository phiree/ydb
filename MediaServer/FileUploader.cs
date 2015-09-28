using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
namespace MediaServer
{
    public class FileUploader
    {
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
            return  fileName;
        }
    }
}



 
