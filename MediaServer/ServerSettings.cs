using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace MediaServer
{
    /// <summary>
    /// 服务器的配置
    /// 1)filedomain 对应的存储位置
    /// 2)filetype保存的后缀名
    /// 3)文件名的生成规则.
    /// </summary>
    public class ServerSettings
    {
        private static Dictionary<string, string> domainPath = new Dictionary<string, string>();
        public static readonly string seperator = "_$_"; //构建文件名的分隔符.
        public static Dictionary<string, string> DomainPath
        {
            get
            {
                if (domainPath.Count == 0)
                {
                    domainPath.Add("BusinessImage", "/BusinessImage/");
                    domainPath.Add("UserAvatar", "/Avatar/");
                    domainPath.Add("ChatAudio", "/ChatAudio/");
                    domainPath.Add("ChatVideo", "/ChatVideo/");
                    domainPath.Add("ChatImage", "/ChatImage/");
                }
                return domainPath;
            }
        }
        private static Dictionary<FileType, string> fileExtension = new Dictionary<FileType, string>();

        public static Dictionary<FileType, string> FileExtension
        {
            get
            {
                if (fileExtension.Count == 0)
                {
                    fileExtension.Add(FileType.voice, ".mp3");
                    fileExtension.Add(FileType.doc, ".docx");
                    fileExtension.Add(FileType.image, ".png");
                    fileExtension.Add(FileType.pdf, ".pdf");
                    fileExtension.Add(FileType.txt, ".txt");
                    fileExtension.Add(FileType.video, ".mp4");
                    fileExtension.Add(FileType.other, "");

                }
                return fileExtension;
            }
        }

     
        public static string FileNameBuilder(string originalName, string domainType, FileType fileType)
        {
            
            string fileExtension = FileExtension[fileType];
            string fileNameGuid = Guid.NewGuid().ToString();
            string fileName = string.Format("{1}{0}{2}{0}{3}{0}{4}",
                seperator,
                originalName.Replace(".","_"),
                fileNameGuid,
                domainType,
                 fileType.ToString()
                );
            return fileName;

        }

        public static void FileNameParser(string fileName, out FileType fileType
            , out string domainType,out string cleanedFileName
            ,  out bool isImageThumbnail
            ,out int thumbnailWidth,out int thumbnailHeight)
        {
            cleanedFileName =  fileName;
            thumbnailWidth = thumbnailHeight = 0;
            string regp = @"(_(\d+)[x|X](\d+))$";
            Regex re = new Regex(regp);
              isImageThumbnail = re.IsMatch(fileName);

            if (isImageThumbnail)
            {
                Match regexMatch = re.Match(fileName);
                thumbnailWidth = Convert.ToInt32(regexMatch.Groups[2].Value);
                thumbnailHeight = Convert.ToInt32(regexMatch.Groups[3].Value);
                cleanedFileName = fileName.Replace(regexMatch.Groups[1].Value, string.Empty);
            }

  
            string fileNameRelative = string.Empty;
            domainType = string.Empty;
           
            string[] arr  = cleanedFileName.Split(new string[] {seperator },  StringSplitOptions.None);
             
            if(arr.Length!=4)
            { //文件名格式有误
                throw new FormatException("文件名格式有误");
            }
            domainType = arr[2];
            string strFileType = arr[3];
            strFileType = Path.GetFileNameWithoutExtension(strFileType);
            if (!Enum.TryParse(strFileType, out fileType))
            {
                throw new FormatException("文件格式有误");
            }
             
            
        }
    }

}