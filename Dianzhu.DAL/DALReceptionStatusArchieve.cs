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
            
            var query = Session.QueryOver<ReceptionStatusArchieve>();
            if (member.UserType == Model.Enums.enum_UserType.customerservice)
            {
                query = query.Where(x => x.CustomerService == member);
            }
            else if (member.UserType == Model.Enums.enum_UserType.customer)
            {
                query = query.Where(x => x.Customer == member);
            }
            else
            {
                return 0;
            }
            return query.RowCount();
        }
    }
}
