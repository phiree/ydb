using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.IDAL
{
    public interface IDALCashTicketTemplate
    {
        IDALBase<CashTicketTemplate> DalBase { get; set; }
    }
}
