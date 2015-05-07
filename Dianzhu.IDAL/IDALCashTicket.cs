using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.IDAL
{
    public interface IDALCashTicket
    {
        IDALBase<CashTicket> DalBase { get; set; }
        bool CheckTicketCodeExists(string code);
    }
}
