using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.IDAL
{
    /// <summary>
    ///
    /// </summary>
    public interface IDALReceptionStatus  :IRepository<ReceptionStatus,Guid>

    {


      IList<ReceptionStatus> GetListByCustomerService(DZMembership customerService);
   
    DZMembership GetListByCustomerServiceId(Guid csid);
   IList<ReceptionStatus> GetListByCustomer(DZMembership customer);
     ReceptionStatus GetOneByCustomerAndCS(DZMembership customerService, DZMembership customer);

     void DeleteAllCustomerAssign(DZMembership customer);
     void DeleteAllCustomerServiceAssign(DZMembership customerService);

     IList<DZMembership> GetCSMinCount(DZMembership diandian);

      IList<ReceptionStatus> GetRSListByDiandian(DZMembership diandian, int num);

     ReceptionStatus GetOrder(DZMembership c, DZMembership cs);

     ReceptionStatus GetOneByCustomer(Guid customerId);
    }
}
