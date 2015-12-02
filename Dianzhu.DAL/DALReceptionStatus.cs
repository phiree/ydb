using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using NHibernate.Criterion;

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

        public virtual IList<DZMembership> GetCSMinCount(DZMembership diandian)
        {
            var result = Session.QueryOver<ReceptionStatus>().Select(
                Projections.Group<ReceptionStatus>(e => e.CustomerService),
                Projections.Count<ReceptionStatus>(e => e.CustomerService)).
                Where(e => e.CustomerService != diandian).
                OrderBy(Projections.Count<ReceptionStatus>(e => e.CustomerService)).Asc.List<object[]>();

            IList<DZMembership> dzList = new List<DZMembership>();
            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    dzList.Add((DZMembership)result[i][0]);
                }
            }            
          
            return dzList;
        }

        public virtual ReceptionStatus GetReceptionStatusByDiandian(DZMembership diandian)
        {
            ReceptionStatus re = null;
            var result = Session.QueryOver<ReceptionStatus>().Where(x => x.CustomerService == diandian).OrderBy(x => x.LastUpdateTime).Asc.List();
            if (result.Count > 0)
            {
                re = result[0];
            }

            return re;
        }

        public virtual ReceptionStatus GetOrder(DZMembership c,DZMembership cs)
        {
            return Session.QueryOver<ReceptionStatus>().Where(x => x.Customer == c).And(x => x.CustomerService == cs).List()[0];
        }
    }
}
