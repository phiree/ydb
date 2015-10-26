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

        }
        //注入依赖,供测试使用;
        public DALReceptionStatus(string fortestonly): base(fortestonly)
         { }

         public IList<ReceptionStatus> GetListByCustomerService(DZMembership customerService)
         {
           return   Session.QueryOver<ReceptionStatus>().Where(x => x.CustomerService.Id == customerService.Id).List();
         }
         public IList<ReceptionStatus> GetListByCustomer(DZMembership customer)
         {
             return Session.QueryOver<ReceptionStatus>().Where(x => x.Customer.Id == customer.Id).List();
      
         }
        public ReceptionStatus GetOneByCustomerAndCS(DZMembership customerService, DZMembership customer)
        {
          var result=  Session.QueryOver<ReceptionStatus>().Where(x => x.Customer == customer)
                .And(x => x.CustomerService == customerService).SingleOrDefault();
            return result;
        }
   
    }
}
