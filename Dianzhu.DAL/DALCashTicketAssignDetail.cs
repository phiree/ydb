using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
   public class DALCashTicketAssignDetail:DALBase<CashTIcketAssignDetail>
    {
        public DALCashTicketAssignDetail()
        {
            
        }
        //注入依赖,供测试使用;
        public DALCashTicketAssignDetail(string fortest):base(fortest)
        {
            
        }
    }
}
