using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.IDAL
{
    public interface IDALCashTicketCreateRecord
    {
        IDALBase<CashTicketCreateRecord> DALBase { get; set; }
        IList<CashTicketCreateRecord> GetMonthRecord(Business_Abs business, int year,int month);
    }
}
