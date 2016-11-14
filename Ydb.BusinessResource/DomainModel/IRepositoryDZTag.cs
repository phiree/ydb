using System;
using System.Collections.Generic;
 
using Ydb.Common.Repository;
namespace Ydb.BusinessResource.DomainModel
{
    public interface IRepositoryDZTag : IRepository<DZTag, Guid>
    {
        IList<DZTag> GetTagsForBusiness(Guid businessId);
        IList<DZTag> GetTagsForBusinessAndTypeId(Guid businessId, Guid typeId);
        IList<DZTag> GetTagsForService(Guid serviceId);
    }
}