using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.DAL
{

   public class DALServiceOrderPushedService:NHRepositoryBase<ServiceOrderPushedService,Guid>,IDAL.IDALServiceOrderPushedService
    {
        public virtual IList<ServiceOrderPushedService> FindByOrder(ServiceOrder order)
        {
            return Find(x => x.ServiceOrder.Id == order.Id);
        }
    }
}
