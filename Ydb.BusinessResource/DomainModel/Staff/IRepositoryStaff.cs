using System.Collections.Generic;
using System;
using Ydb.Common.Repository;
namespace Ydb.BusinessResource.DomainModel
{
    public interface IRepositoryStaff:IRepository<Staff,Guid>
    {
        IList<Staff> GetAllListByBusiness(Business business);
        int GetEnableSum(Business business);
    }
}