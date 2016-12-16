using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order.DomainModel;
namespace Ydb.Order.DomainModel.Repository
{

    public interface IDALServiceOrderPushedService:IRepository<ServiceOrderPushedService,Guid>

    {
        IList<ServiceOrderPushedService> FindByOrder(ServiceOrder order);
    }
}
