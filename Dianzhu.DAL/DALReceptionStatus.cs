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
    public class DALReceptionStatus : NHRepositoryBase<ReceptionStatus,Guid>,IDAL.IDALReceptionStatus
    {
        

         public virtual IList<ReceptionStatus> GetListByCustomerService(DZMembership customerService)
         {
          // 

            return Find(x => x.CustomerService.Id == customerService.Id);
         }
        public virtual DZMembership GetListByCustomerServiceId(Guid csid)
        {
          var result =FindOne(x => x.CustomerService.Id == csid) ;
            if (result != null)
            {
                return result.CustomerService;
            }
            else
            {
                return null;
            }            
        }
        public virtual IList<ReceptionStatus> GetListByCustomer(DZMembership customer)
         {
            return Find(x => x.Customer.Id == customer.Id);
      
         }
        public virtual ReceptionStatus GetOneByCustomerAndCS(DZMembership customerService, DZMembership customer)
        {

            ReceptionStatus result = null;
            
            var resultList = Find(x => x.Customer.Id == customer.Id && x.CustomerService.Id == customerService.Id);
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
            
            using(var tr = Session.BeginTransaction()) { 
             var  result = Session.QueryOver<ReceptionStatus>().Select(
                Projections.Group<ReceptionStatus>(e => e.CustomerService),
                Projections.Count<ReceptionStatus>(e => e.CustomerService)).
                Where(e => e.CustomerService != diandian).
                OrderBy(Projections.Count<ReceptionStatus>(e => e.CustomerService)).Asc.List<object[]>();
                tr.Commit();
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
         
        }

        public virtual IList<ReceptionStatus> GetRSListByDiandian(DZMembership diandian,int num)
        {
           

            return Find(x => x.CustomerService.Id == diandian.Id).OrderBy(x => x.LastUpdateTime).Take(num).ToList();
        }

        public virtual ReceptionStatus GetOrder(DZMembership c,DZMembership cs)
        {
         
            return FindOne(x => x.Customer.Id == c.Id && x.CustomerService.Id == cs.Id);
        }

        public virtual ReceptionStatus GetOneByCustomer(Guid customerId)
        {
            
            return FindOne(x => x.Customer.Id == customerId);
        }
    }
}
