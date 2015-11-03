using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using PHSuit;
namespace MediaServer
{
    /// <summary>
    /// 获取文件的相对地址.
    /// God bless me, let the unittest Greens.
    /// 
    /// thank god.
    /// </summary>
    public class FileGetter
    {

        private string cleanedFileName = string.Empty;
        private string originalFileName;
        string mediaRootDir;
        int imageWidth, imageHeight;
        bool isImageThumbnail;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalFileName">原始文件名称(不包括缩略图名称规则部分)</param>
        /// <param name="mediaRootDir">存储文件的根目录(相对路径)</param>
        public FileGetter(string originalFileName, string mediaRootDir)
        {
            this.originalFileName = originalFileName;
            this.mediaRootDir = mediaRootDir;
        }

        /// <summary>
        /// 获取文件的相对路径.
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public string GetRelativeFileName(out FileType fileType)
        {
            string result = string.Empty;
            string domainType;
            string extension = Path.GetExtension(originalFileName);
            KeyValuePair<FileType, string> fileExtensionPair = MediaServer.ServerSettings.FileExtension.SingleOrDefault(x => x.Value == extension);
            fileType = fileExtensionPair.Key;
            ServerSettings.FileNameParser(originalFileName, out fileType
                , out domainType, out cleanedFileName
                , out isImageThumbnail, out imageWidth, out imageHeight);

            string relativePath = ServerSettings.DomainPath[domainType];
            result = relativePath + cleanedFileName;
            if (fileType == FileType.image && isImageThumbnail)
            {
                result = GetImageRelativePath(relativePath);
            }
            if (fileType == FileType.voice)
            {
                result = GetCompiledAudio(relativePath);
            }
            //如果是图片,则需要处理压缩图.

            return result;
        }

        private string GetCompiledAudio(string relativePath)
        {
            //todo: 音频文件解码.
            return relativePath + originalFileName;
        }

        private string GetImageRelativePath(string originalRelativePath)
        {

            string thumbnailName = ThumbnailMaker.Make(
                mediaRootDir + originalRelativePath,
                mediaRootDir + originalRelativePath + "thumbnail\\",
                cleanedFileName, imageWidth, imageHeight, ThumbnailType.GeometricScalingByMax);
            return thumbnailName.Replace(mediaRootDir, string.Empty);
        }



    }


}




