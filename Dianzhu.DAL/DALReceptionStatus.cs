using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    /// <summary>
    ///
    /// </summary>
    public class DALReceptionStatus : DALBase<Model.ReceptionStatus>
    {
         public DALReceptionStatus()
        {
            Session = new HybridSessionBuilder().GetSession();
        }
         public DALReceptionStatus(string fortestonly)
         { }

        //注入依赖,供测试使用;
         public IList<ReceptionStatus> GetListByCustomerService(DZMembership customerService)
         {
           return   Session.QueryOver<ReceptionStatus>().Where(x => x.CustomerService.Id == customerService.Id).List();
         }
         public IList<ReceptionStatus> GetListByCustomer(DZMembership customer)
         {
             return Session.QueryOver<ReceptionStatus>().Where(x => x.Customer.Id == customer.Id).List();
      
         }
   
    }
}
