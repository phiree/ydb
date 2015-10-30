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
        public  DALReceptionStatus(string fortestonly): base(fortestonly)
         { }

         public virtual IList<ReceptionStatus> GetListByCustomerService(DZMembership customerService)
         {
           return   Session.QueryOver<ReceptionStatus>().Where(x => x.CustomerService.Id == customerService.Id).List();
         }
        public virtual IList<ReceptionStatus> GetListByCustomer(DZMembership customer)
         {
             return Session.QueryOver<ReceptionStatus>().Where(x => x.Customer.Id == customer.Id).List();
      
         }
        public virtual ReceptionStatus GetOneByCustomerAndCS(DZMembership customerService, DZMembership customer)
        {

            ReceptionStatus result = null;
            var resultList = Session.QueryOver<ReceptionStatus>().Where(x => x.Customer == customer)
                .And(x => x.CustomerService == customerService).List();
            if (resultList.Count >= 1)
            {
                result = resultList[0];
            }
            else
            { result = null; }
            return result;
        }

        public virtual void DeleteAllCustomerAssign(DZMembership customer)
        {
            IList<ReceptionStatus> rsList = GetListByCustomer(customer);
            foreach (ReceptionStatus rs in rsList)
            {
                Session.Delete(rs);
            }
        }
        public virtual void DeleteAllCustomerServiceAssign(DZMembership customerService)
        {
            IList<ReceptionStatus> rsList = GetListByCustomerService(customerService);
            foreach (ReceptionStatus rs in rsList)
            {
                Session.Delete(rs);
            }
        }
    }
}
