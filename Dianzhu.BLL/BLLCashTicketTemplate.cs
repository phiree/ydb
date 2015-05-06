using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 现金券生产者。
    /// </summary>
    public class BLLCashTicketTemplate
    {
        DalCashTicketTemplate dal = new DalCashTicketTemplate();

        public CashTicketTemplate Create(string name,Business owner,
                                    DateTime validDate,DateTime ExpiredDate,
                                    int amount,string conditions,float coverage )
        {
            CashTicketTemplate ctt = new CashTicketTemplate { 
            Amount=amount,
            Conditions=conditions,
             Coverage=coverage,
              ExpiredDate=ExpiredDate,
               Name=name,
                Owner=owner,
                 ValidDate=validDate
            };
            dal.Save(ctt);
            return ctt;
        }
        
    }
}
