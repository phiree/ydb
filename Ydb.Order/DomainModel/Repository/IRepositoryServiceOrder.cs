using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;

namespace Ydb.Order.DomainModel.Repository
{
    public interface IDALServiceOrder: IRepository<ServiceOrder,Guid>
    {
        IList<ServiceOrder> GetOrderListOfServiceByDateRange(Guid serviceId, DateTime timeBegin, DateTime dateEnd);
        IList<ServiceOrder> GetAll(int pageIndex, int pageSize, out long totalRecords);


    }
}
