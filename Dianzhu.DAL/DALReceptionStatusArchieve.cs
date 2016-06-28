using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System.Linq.Expressions;
using DDDCommon;
namespace Dianzhu.DAL
{
    /// <summary>
    ///
    /// </summary>
    public class DALReceptionStatusArchieve : NHRepositoryBase<ReceptionStatusArchieve,Guid>,IDAL.IDALReceptionStatusArchieve
    {
         

        public virtual IList<DZMembership> GetCustomerListByCS(DZMembership cs, int pageNum, int pageSize, out int totalAmount)
        {
            
            var rsalist = Find(x => x.CustomerService.Id == cs.Id).OrderByDescending(x=>x.ArchieveTime);
            totalAmount = rsalist.Count();


         //   return iquery.Skip((pageNum - 1) * pageSize).Take(pageSize).List().Select(x => x.Customer).ToList();
            return rsalist.Skip((pageNum - 1) * pageSize).Take(pageSize).Select(x => x.Customer).ToList();

        }
        public int GetReceptionAmount(DZMembership member)
        {
           var  where = DDDCommon.PredicateBuilder.True<ReceptionStatusArchieve>();
            
            
            
            switch (member.UserType)
            {


                case Model.Enums.enum_UserType.customer:
                    where = where.And(x => x.Customer == member);
                        //ReduceAndCheck Andquery.Where(x => x.Customer == member);
                    break;
                case Model.Enums.enum_UserType.customerservice:
                    //  query = query.Where(x => x.CustomerService == member);
                    where = where.And(x => x.CustomerService == member);
                    break;

                default: throw new Exception("不支持的用户类型");
            }
            // var query = BuildReceptionQuery(member);
            //return query.RowCount();
            return (int) GetRowCount(where);
        }
      
        public int GetReceptionDates(DZMembership member)
        {
            //todo: 需要根据日期部分做distinct.研究了半天 未果. 搜索关键字 datepart in hql,  customerdiact.
          
            var querystr = @"SELECT Count(DISTINCT  r.ArchieveTime) from ReceptionStatusArchieve r
                        WHERE r.Customer.Id = '" + member.Id+"'";
            var query = Session.CreateQuery(querystr);
            int result = 0;
            
             result=   (int)query.FutureValue<long>().Value;
              
            return result;


        }
    }
}
