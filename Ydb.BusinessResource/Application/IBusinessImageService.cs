using System;
using System.Web;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common;

namespace Ydb.BusinessResource.Application
{
    public interface IBusinessImageService
    {
        void Delete(Guid biId);
        bool DeleteBusImageByName(string imgName);
        BusinessImage FindBusImageByName(string imgName);
        string Save(Guid businessId, HttpPostedFileBase imageFile, enum_ImageType imageType);
    }
}