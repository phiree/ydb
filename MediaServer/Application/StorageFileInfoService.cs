using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ydb.MediaServer.DomainModel;
using Ydb.MediaServer.DomainModel.Repository;
using Ydb.MediaServer.Infrastructure;

namespace Ydb.MediaServer.Application
{
    public class  StorageFileInfoService : IStorageFileInfoService
    {
      IRepositoryStorageFileInfo repoStorageFileInfo;

        public StorageFileInfoService(IRepositoryStorageFileInfo repoStorageFileInfo)
        {
            this.repoStorageFileInfo = repoStorageFileInfo;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="c"></param>


        /*
         Model.StorageFileInfo fileinfo = new Model.StorageFileInfo();
            fileinfo.OriginalFileName = "";
            fileinfo.FileName = strFileName;
            fileinfo.FileType = "image";
            fileinfo.Height = strHeight;
            fileinfo.Width = strWidth;
            fileinfo.Size = strSize;
            fileinfo.UploadTime = DateTime.Now;
            fileinfo.UploadUser = customer.UserID;
         */
        [UnitOfWork]
        public StorageFileInfo Save(string originalFileName, string fileName, string fileType, string height, string width,string length, string size, DateTime uploadTime, string uploadUser)
        {
            StorageFileInfo fileinfo = new StorageFileInfo();
            fileinfo.OriginalFileName = "";
            fileinfo.FileName = fileName;
            fileinfo.FileType = fileType ;
            fileinfo.Height = height;
            fileinfo.Width = width;
            fileinfo.Length = length;
            fileinfo.Size = size;
            fileinfo.UploadTime = DateTime.Now;
            fileinfo.UploadUser = uploadUser;
            

            repoStorageFileInfo.Add(fileinfo);
            return fileinfo;
        }
    }
}
