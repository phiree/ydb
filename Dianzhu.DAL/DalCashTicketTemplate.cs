using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
   public class DALCashTicketTemplate:DALBase<CashTicketTemplate>
    {
        public DALCashTicketTemplate()
        {
            
        }
        //注入依赖,供测试使用;
        public DALCashTicketTemplate(string fortest):base(fortest)
        {
            
        }
       public IList<CashTicketTemplate> GetListByBusiness(Business business)
       {
           return GetList("select t from CashTicketTemplate  t where  Owner.Id='" + business.Id+"'");

       }
    }
}
