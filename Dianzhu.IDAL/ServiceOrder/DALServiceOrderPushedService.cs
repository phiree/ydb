using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.IDAL
{

    public interface IDALServiceOrderPushedService:IRepository<ServiceOrderPushedService,Guid>

    {
        IList<ServiceOrderPushedService> FindByOrder(ServiceOrder order);
    }
}
