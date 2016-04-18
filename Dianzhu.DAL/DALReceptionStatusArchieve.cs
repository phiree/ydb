using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Dianzhu.DAL
{
    /// <summary>
    ///
    /// </summary>
    public class DALReceptionStatusArchieve : DALBase<Model.ReceptionStatusArchieve>
    {
        public DALReceptionStatusArchieve()
        {

        }
        //注入依赖,供测试使用;
        public DALReceptionStatusArchieve(string fortestonly) : base(fortestonly)
        { }

        public virtual IList<DZMembership> GetCustomerListByCS(DZMembership cs, int pageNum, int pageSize, out int totalAmount)
        {
            var iquery = Session.QueryOver<ReceptionStatusArchieve>().Where(x => x.CustomerService == cs).OrderBy(x => x.ArchieveTime).Desc;
            totalAmount = iquery.RowCount();
            return iquery.Skip((pageNum - 1) * pageSize).Take(pageSize).List().Select(x => x.Customer).ToList();
        }
        public int GetReceptionAmount(DZMembership member)
        {

            var query = BuildReceptionQuery(member);
            return query.RowCount();
        }
        private IQueryOver<ReceptionStatusArchieve, ReceptionStatusArchieve> BuildReceptionQuery(DZMembership member)
        {
            var query = Session.QueryOver<ReceptionStatusArchieve>();
            switch (member.UserType)
            {
               
                 
                case Model.Enums.enum_UserType.customer:
                    query = query.Where(x => x.Customer == member);
                    break;
                case Model.Enums.enum_UserType.customerservice:
                    query = query.Where(x => x.CustomerService == member);
                    break;
                 
                default:throw new Exception("不支持的用户类型");
            }
             
            return query;
        }
        public int GetReceptionDates(DZMembership member)
        {
            //todo: 需要根据日期部分做distinct.研究了半天 未果. 搜索关键字 datepart in hql,  customerdiact.
          
            var querystr = @"SELECT Count(DISTINCT  r.ArchieveTime) from ReceptionStatusArchieve r
                        WHERE r.Customer.Id = '" + member.Id+"'";
            var query = Session.CreateQuery(querystr);
            return (int)query.FutureValue<long>().Value;
        }
    }
}
