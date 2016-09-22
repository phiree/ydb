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
                Session.Flush();
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
            
            
             var  result = Session.QueryOver<ReceptionStatus>().Select(
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

        public virtual IList<ReceptionStatus> GetRSListByDiandian(DZMembership diandian,int num)
        {
           

            return Find(x => x.CustomerService.Id == diandian.Id).OrderBy(x => x.LastUpdateTime).Take(num).ToList();
        }
        public virtual IList<ReceptionStatus> GetRSListByDiandianAndUpdate(DZMembership diandian, int num,DZMembership customerService)
        {
            //string queryList = @"select rs from ReceptionStatus rs "
            //                   + " inner join rs.CustomerService cs "
            //                   + " where cs.Id='" + diandian.Id + "' ";
            //var rsList = Session.CreateQuery(queryList).SetFirstResult(0).SetMaxResults(num).List<ReceptionStatus>();
            //if(rsList.Count()>0)
            //{ 
            //var qryUpdate = Session.CreateQuery( @"update   ReceptionStatus   set  CustomerService.Id=:csId"
            //        + " where  Customer.Id in (:customerList)");
            //qryUpdate.SetParameter("csId", customerService.Id);
            //qryUpdate.SetParameterList("customerList", rsList.Select(x => x.Customer.Id));
            //qryUpdate.ExecuteUpdate();


            //    string queryList2 = @"select rs from ReceptionStatus rs "
            //                   + " inner join rs.CustomerService cs "
            //                   + " where cs.Id='" + customerService.Id + "' ";
            //    rsList = Session.CreateQuery(queryList2).SetFirstResult(0).SetMaxResults(num).List<ReceptionStatus>();

            //}

            string sql = string.Format(@"UPDATE receptionstatus SET customerservice_id='{0}'
                    WHERE customerservice_id = '{1}' LIMIT {2};
            SELECT* FROM receptionstatus WHERE customerservice_id = '{0}'"
            , customerService.Id,diandian.Id,num);

            IQuery pureQry= Session.CreateSQLQuery(sql).AddEntity(typeof(ReceptionStatus));

            IList<ReceptionStatus> list2= pureQry.List<ReceptionStatus>();

            
            return list2;
           // return Find(x => x.CustomerService.Id == diandian.Id).OrderBy(x => x.LastUpdateTime).Take(num).ToList();
        }

        public virtual ReceptionStatus GetOrder(DZMembership c,DZMembership cs)
        {
            IList<ReceptionStatus> list = Find(x => x.Customer.Id == c.Id && x.CustomerService.Id == cs.Id).Take(1).ToList();
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
            //return FindOne(x => x.Customer.Id == c.Id && x.CustomerService.Id == cs.Id);
        }

        public virtual ReceptionStatus GetOneByCustomer(Guid customerId)
        {
            IList<ReceptionStatus> list = Find(x => x.Customer.Id == customerId).Take(1).ToList();
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
            //return FindOne(x => x.Customer.Id == customerId);
        }
    }
}
