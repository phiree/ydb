using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
using Dianzhu.Model.Enums;
namespace Dianzhu.IDAL
{
   public interface IDALCashTicket :IDAL.IRepository<CashTicket, Guid>
    {



          bool CheckTicketCodeExists(string code);
          IList<CashTicket> GetCashTicketListForBusiness(Guid businessId);
          IList<CashTicket> GetListForCustomer(Guid memebrId);
            IList<CashTicket> GetCashTicketList(Guid userId, enum_CashTicketSearchType searchType, int pageNum, int pageSize);

          int GetCount(Guid userId, enum_CashTicketSearchType searchType);
    }
}
