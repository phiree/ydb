using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
namespace Ydb.BusinessResource.DomainModel
{
   
    public interface IRepositoryDZService : IRepository<DZService, Guid>
    {
   
        IList<DZService> GetList(Guid businessId, int pageindex, int pagesize, out int totalRecord);
        DZService GetOneByBusAndId(Business business, Guid svcId);
          IList<DZService> GetOtherList(Guid businessId, Guid serviceId, int pageindex, int pagesize, out int totalRecord);
         int GetSumByBusiness(Business business);
        IList<DZService> SearchService(string name, decimal priceMin, decimal priceMax, Guid serviceTypeId, DateTime preOrderTime, double lng, double lat, int pageindex, int pagesize, out int totalRecord);
    }

}
