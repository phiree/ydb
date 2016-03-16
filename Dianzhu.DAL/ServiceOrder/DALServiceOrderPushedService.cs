using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.DAL
{

   public class DALServiceOrderPushedService:DAL.DALBase<ServiceOrderPushedService>
    {
        public virtual IList<ServiceOrderPushedService> FindByOrder(ServiceOrder order)
        {
            var query = Session.QueryOver<ServiceOrderPushedService>().Where(x => x.ServiceOrder == order);
            return GetList(query);
        }
    }
}
