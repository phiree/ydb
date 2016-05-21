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
                    domainPath["BusinessImage"] = "/BusinessImage/";//商家后台上传图片
                    domainPath["UserAvatar"] = "/Avatar/";//用户头像
                    domainPath["ChatAudio"] = "/ChatAudio/";//聊天语音
                    domainPath["ChatVideo"] = "/ChatVideo/";//聊天视频
                    domainPath["ChatImage"] = "/ChatImage/";//聊天图片
                    domainPath["Advertisement"] = "/Advertisement/";//广告
                    domainPath["StaffAvatar"] = "/StaffAvatar/";//职工头像
                    domainPath["BusinessAvatar"] = "/BusinessAvatar/";//店铺logo
                    domainPath["BusinessShow"] = "/BusinessShow/";//店铺展示图片
                    domainPath["BusinessLicense"] = "/BusinessLicense/";//商家营业执照
                    domainPath["BusinessLicenseB"] = "/BusinessLicenseB/";//商家营业执照
                    domainPath["BusinessChargePersonIdCard"] = "/BusinessChargePersonIdCard/";//负责人证件照片
                    domainPath["BusinessChargePersonIdCardB"] = "/BusinessChargePersonIdCardB/";//负责人证件照片
                }
                return domainPath;
            }
        }
        private static Dictionary<FileType, string> fileExtension = new Dictionary<FileType, string>();//文件扩展名

        /// <summary>
        /// 文件名生成（字符串），无文件类型，通过解析字符串可获取文件类型
        /// </summary>
        /// <param name="originalName"></param>
        /// <param name="domainType"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string FileNameBuilder(string originalName, string domainType, FileType fileType)
        {
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
        /// <summary>
        /// 解析文件名字符串，获取文件类型domainPath，如果是图片，有缩略图大小
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <param name="domainType"></param>
        /// <param name="cleanedFileName"></param>
        /// <param name="isImageThumbnail"></param>
        /// <param name="thumbnailWidth"></param>
        /// <param name="thumbnailHeight"></param>
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