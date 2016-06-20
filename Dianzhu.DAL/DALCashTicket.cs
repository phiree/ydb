using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.Model.Enums;
using DDDCommon;
namespace Dianzhu.DAL
{
   public class DALCashTicket:DAL.NHRepositoryBase<CashTicket,Guid>,IDAL.IDALCashTicket
    {


       
        public  bool CheckTicketCodeExists(string code)
       {
           
         var result=Find(x=>x.TicketCode==code);
         bool exists = result.Count > 0;
         return exists;
       }
       public IList<CashTicket> GetCashTicketListForBusiness(Guid businessId)
       {
           var result = Find(x => x.BusinessAssigned.Id == businessId);
           return result;
       }
       public IList<CashTicket> GetListForCustomer(Guid memebrId)
       {
           var result = Find(x => x.UserAssigned.Id == memebrId);
           return result;
       }
       private IList<CashTicket> GetList(Guid userId, enum_CashTicketSearchType searchType)
       {
          System.Linq.Expressions.Expression<Func<CashTicket, bool>> expUser = x => x.UserAssigned.Id == userId;
            System.Linq.Expressions.Expression<Func<CashTicket, bool>> expSearchType=x=>true;

            switch (searchType)
           {

               case enum_CashTicketSearchType.Nt:
                    expSearchType = x => x.UserUsedTime <= DateTime.MinValue;
                   break;
               case enum_CashTicketSearchType.Ps:
                    expSearchType = x => x.CashTicketTemplate.ExpiredDate < DateTime.Now;
                   break;
               case enum_CashTicketSearchType.Us:
                    expSearchType = x => x.UserUsedTime < DateTime.Now;
                   break;
               default:
               case enum_CashTicketSearchType.All:
                   break;
           }
            System.Linq.Expressions.Expression<Func<CashTicket, bool>> expFinal = PredicateBuilder.And(expUser, expSearchType);
            
            return Find(expFinal) ;
       }
       public IList<CashTicket> GetCashTicketList(Guid userId, enum_CashTicketSearchType searchType, int pageNum, int pageSize)
       {

           var result = GetList(userId, searchType);
           return result;
       }

       public int GetCount(Guid userId,enum_CashTicketSearchType searchType)
       {
           var listQuery = GetList(userId, searchType);
           return listQuery.Count;
       }
    }
}
