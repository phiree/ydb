using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
   public class DALCashTicketAssignRecord:DALBase<CashTicketAssignRecord>
    {
         public DALCashTicketAssignRecord()
        {
             
        }
        //注入依赖,供测试使用;
         public DALCashTicketAssignRecord(string fortest):base(fortest)
        {
            
        }
    }
}
