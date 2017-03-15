using System;
using System.Collections.Generic;
using Ydb.Push.DomainModel;

namespace Ydb.Push.Application
{
    public interface IAdvertisementService
    {
        IEnumerable<Advertisement> GetADList(int pageIndex, int pageSize, out long totalRecords);
        IList<Advertisement> GetADListForUseful(string userType);
        Advertisement GetByUid(Guid uid);
        void Save(Advertisement ad);
        void Update(Advertisement ad);
    }
}