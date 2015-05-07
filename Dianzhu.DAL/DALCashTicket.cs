using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
   public class DalCashTicket:DalBase<CashTicket>
    {

       public  bool CheckTicketCodeExists(long code)
       {
         var result=   session.QueryOver<CashTicket>().Where(x => x.TicketCode == code.ToString());
         bool exists = result.RowCount() > 0;
         return exists;
       }
    }
}
