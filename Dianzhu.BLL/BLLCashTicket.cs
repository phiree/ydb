using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 现金券边界类，负责各种操作
    /// </summary>
    public class BLLCashTicket
    {
        public string BatchCreate(Business_Abs owner, int amount, CashTicketTemplate tt)
        { 
            
        }
    }
    public class CashTicketBatchCreator {
        public Business_Abs Owner { get; set; }
        public int Amount { get; set; }
        public CashTicketTemplate TT { get; set; }
        DalCashTicket dal = new DalCashTicket();
        public CashTicketBatchCreator(Business_Abs owner,int amount,CashTicketTemplate tt)
        {
            this.Owner = owner;
            this.Amount = amount;
            this.TT = tt;
        }
        public IList<CashTicket> BatchCreate()
        { 
            
        }
        private int[] GenerateCodeList()
        { 
            
        }
    }
}
