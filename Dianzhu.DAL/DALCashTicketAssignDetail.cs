using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
   public class DALCashTicketAssignDetail:NHRepositoryBase<CashTicketAssignDetail,Guid>
    {
        public DALCashTicketAssignDetail()
        {
            
        }
        //注入依赖,供测试使用;
        
    }
}
