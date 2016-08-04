using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Storage
{
    public class StorageService : IStorageService
    {
        BLL.BLLStorageFileInfo bllFileInfo;
        public StorageService(BLL.BLLStorageFileInfo bllFileInfo)
        {
            this.bllFileInfo = bllFileInfo;
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
            Model.StorageFileInfo fileinfo = new Model.StorageFileInfo();
            fileinfo.OriginalFileName = "";
            fileinfo.FileName = strFileName;
            fileinfo.FileType = "image";
            fileinfo.Height = strHeight;
            fileinfo.Width = strWidth;
            fileinfo.Size = strSize;
            fileinfo.UploadTime = DateTime.Now;
            fileinfo.UploadUser = customer.UserID;
            bllFileInfo.Save(fileinfo);
            imageObj imageobj = Mapper.Map<Model.StorageFileInfo, imageObj>(fileinfo);
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
            Model.StorageFileInfo fileinfo = new Model.StorageFileInfo();
            fileinfo.OriginalFileName = "";
            fileinfo.FileName = strs[0];
            fileinfo.FileType = "voice";
            fileinfo.Size = strSize;
            fileinfo.Length= strs[1];
            fileinfo.UploadTime = DateTime.Now;
            fileinfo.UploadUser = customer.UserID;
            bllFileInfo.Save(fileinfo);
            audioObj audioobj = Mapper.Map<Model.StorageFileInfo, audioObj>(fileinfo);
            return audioobj;
        }
    }
}
