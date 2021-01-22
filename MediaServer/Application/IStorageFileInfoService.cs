using System;
using Ydb.MediaServer.DomainModel;

namespace Ydb.MediaServer.Application
{
    public interface IStorageFileInfoService
    {
        StorageFileInfo Save(string originalFileName, string fileName, string fileType, string height, string width,string length, string size, DateTime uploadTime, string uploadUser);


    }
}