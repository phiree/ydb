using System.Collections.Generic;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Repository;
namespace Ydb.BusinessResource.DomainModel
{
    public interface IRepositoryServiceType:IRepository<ServiceType,System.Guid>
    {
        ServiceType GetOneByCode(string code);
        ServiceType GetOneByName(string name, int level);
        IList<ServiceType> GetTopList();
        void SaveList(IList<ServiceType> typeList);
          void DeleteAll();
    }
}