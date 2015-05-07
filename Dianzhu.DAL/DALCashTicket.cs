using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.IDAL;
namespace Dianzhu.DAL
{
   public class DalCashTicket:IDALCashTicket
    {
     
        
       public DalCashTicket()
       { 
        
       }
       IDALBase<CashTicket> dalBase = null;
       public IDALBase<CashTicket> DalBase
       {
           get { return new DalBase<CashTicket>(); }
           set { dalBase = value; }
       }
       public  bool CheckTicketCodeExists(string code)
       {
           
         var result= DalBase.GetList(DalBase.Session.QueryOver<CashTicket>().Where(x=>x.TicketCode==code));
         bool exists = result.Count > 0;
         return exists;
       }
    }
}
