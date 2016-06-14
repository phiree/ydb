using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
   public class DALCashTicketAssignRecord:NHRepositoryBase<CashTicketAssignRecord,Guid>
    {
         public DALCashTicketAssignRecord()
        {
             
        }
        //注入依赖,供测试使用;
        
    }
}
