using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ydb.MediaServer.Application;
using Ydb.MediaServer.DomainModel;
namespace Dianzhu.ApplicationService.Storage
{
    public class StorageService : IStorageService
    {
      //  BLL.BLLStorageFileInfo bllFileInfo;
        IStorageFileInfoService storageService;
        public StorageService(IStorageFileInfoService storageService)
        {
            this.storageService = storageService;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileBase4"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public imageObj PostImages(FileBase64 fileBase4, Customer customer)
        {
            if (string.IsNullOrEmpty(fileBase4.data))
            {
                throw new FormatException("图片的base64不能为空！");
            }
            //utils.DownloadToMediaserver("C:\\Users\\Public\\Pictures\\Sample Pictures\\Chrysanthemum.jpg", "", "Image");
            string strFileName=utils.DownloadToMediaserver1(fileBase4.data, fileBase4.originalName, "image");
            if (string.IsNullOrEmpty(strFileName))
            {
                throw new Exception("上传失败！");
            }
            string strHeight = "";
            string strWidth = "";
            string strSize = "";
            utils.Base64ToImage(fileBase4.data, out strHeight, out strWidth,out strSize);
            
            StorageFileInfo fileinfo=storageService.Save("",strFileName,"image",strHeight,strWidth,string.Empty, strSize,DateTime.Now,customer.UserID);
            imageObj imageobj = Mapper.Map<StorageFileInfo, imageObj>(fileinfo);
            return imageobj;
        }

        /// <summary>
        /// /上传语音
        /// </summary>
        /// <param name="fileBase4"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public audioObj PostAudios(FileBase64 fileBase4, Customer customer)
        {
            //utils.DownloadToMediaserver(@"G:\file\2.mp3", "", "voice");//1.wav//ring.mp3
            if (string.IsNullOrEmpty(fileBase4.data))
            {
                throw new FormatException("语音的base64不能为空！");
            }
            
            string strFileName = utils.DownloadToMediaserver1(fileBase4.data, "", "voice");
            if (string.IsNullOrEmpty(strFileName))
            {
                throw new Exception("上传失败！");
            }
            string strSize = "";
            utils.Base64ToAudio(fileBase4.data, out strSize);
            string[] strs = strFileName.Split(new string[]{"_length_"}, StringSplitOptions.None);
            
            StorageFileInfo fileinfo = storageService.Save("", strFileName, "voice", string.Empty, string.Empty, strs[1], strSize, DateTime.Now, customer.UserID);
            audioObj audioobj = Mapper.Map<StorageFileInfo, audioObj>(fileinfo);
            return audioobj;
        }
    }
}
