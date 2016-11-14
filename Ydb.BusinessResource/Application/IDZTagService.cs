using System;
using System.Collections.Generic;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.BusinessResource.Application
{
    public interface IDZTagService
    {
        DZTag AddTag(string text, string serviceId, string businessId, string serviceTypeId);
        void DeleteByServiceId(Guid serviceId);
        void DeleteTag(Guid tagId);
        IList<DZTag> GetTagForService(Guid serviceId);
    }
}