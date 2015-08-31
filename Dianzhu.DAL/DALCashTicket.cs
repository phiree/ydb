using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.Model.Enums;
namespace Dianzhu.DAL
{
   public class DALCashTicket:DALBase<CashTicket>
    {
     
        
        public DALCashTicket()
        {
            Session = new HybridSessionBuilder().GetSession();
        }
        //注入依赖,供测试使用;
        public DALCashTicket(string fortest)
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
       public IList<CashTicket> GetListForCustomer(Guid memebrId)
       {
           var result = GetList(Session.QueryOver<CashTicket>().Where(x => x.UserAssigned.Id == memebrId));
           return result;
       }
       private IQueryOver<CashTicket, CashTicket> GetList(Guid userId, enum_CashTicketSearchType searchType)
       {
           IQueryOver<CashTicket, CashTicket> iqueryover = Session.QueryOver<CashTicket>().Where(x => x.UserAssigned.Id == userId);

           switch (searchType)
           {

               case enum_CashTicketSearchType.Nt:
                   iqueryover = iqueryover.Where(x => x.UserUsedTime <= DateTime.MinValue);
                   break;
               case enum_CashTicketSearchType.Ps:
                   iqueryover = iqueryover.Where(x => x.CashTicketTemplate.ExpiredDate < DateTime.Now);
                   break;
               case enum_CashTicketSearchType.Us:
                   iqueryover = iqueryover.Where(x => x.UserUsedTime < DateTime.Now);
                   break;
               default:
               case enum_CashTicketSearchType.All:
                   break;
           }
           return iqueryover;
       }
       public IList<CashTicket> GetCashTicketList(Guid userId, enum_CashTicketSearchType searchType, int pageNum, int pageSize)
       {

           var result = GetList(userId, searchType);
           return result.List();
       }

       public int GetCount(Guid userId,enum_CashTicketSearchType searchType)
       {
           var listQuery = GetList(userId, searchType);
           return listQuery.RowCount();
       }
    }
}
