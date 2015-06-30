using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.IDAL;
namespace Dianzhu.DAL
{
   public class DALCashTicket:DALBase<CashTicket>
    {
     
        
       public DALCashTicket()
       { 
        
       }
        
       public  bool CheckTicketCodeExists(string code)
       {
           
         var result= GetList(Session.QueryOver<CashTicket>().Where(x=>x.TicketCode==code));
         bool exists = result.Count > 0;
         return exists;
       }
       public IList<CashTicket> GetCashTicketListForBusiness(Guid businessId)
       {
           var result = GetList(Session.QueryOver<CashTicket>().Where(x => x.BusinessAssigned.Id == businessId));
           return result;
       }
    }
}
