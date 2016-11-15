using System.Collections.Generic;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Repository;
namespace Ydb.BusinessResource.DomainModel
{
    public interface IRepositoryServiceOpenTime : IRepository<ServiceOpenTime,System.Guid>
    {
        
    }
    public interface IRepositoryServiceOpenTimeForDay : IRepository<ServiceOpenTimeForDay, System.Guid>
    {

    }
}