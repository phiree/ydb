using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
using System.Collections;
 

namespace Dianzhu.IDAL
{
    public interface IDALDZService :IRepository<DZService,Guid>
    {

          IList<DZService> GetList(Guid businessId, int pageindex, int pagesize, out int totalRecord);
          IList<DZService> GetOtherList(Guid businessId, Guid serviceId, int pageindex, int pagesize, out int totalRecord);
          IList<DZService> SearchService(string name, decimal priceMin, decimal priceMax, Guid serviceTypeId, DateTime preOrderTime, int pageindex, int pagesize, out int totalRecord);

          DZService GetOneByBusAndId(Business business, Guid svcId);

          int GetSumByBusiness(Business business);

    }
}
