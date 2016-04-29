using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.DAL
{
    public interface IDALCashTicketCreateRecord  
    {

          IList<CashTicketCreateRecord> GetMonthRecord(Business_Abs business, int year, int month);

         
    }
}
