using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    public class BLLCashticketCreateRecord
    {
        IDALCashTicketCreateRecord iDAL = null;
        public BLLCashticketCreateRecord(IDALCashTicketCreateRecord iDAL)
        {
            this.iDAL = iDAL;
        }
        public BLLCashticketCreateRecord():this(new DALCashTicketCreateRecord()){}


        
        public IList<CashTicketCreateRecord> GetMonthRecord(Business business,int  year, int month)
        {
            return iDAL.GetMonthRecord(business, year, month);
        }
        public CashTicketCreateRecord GetOne(Guid id)
        {
            return iDAL.DALBase.GetOne(id);
        }

        private bool CheckRule(Business business,int year,int month,int amountCreated, CashTicketTemplate cashticketTemplate, out string message)
        {
            message = string.Empty;
            bool result = false;
            IList<CashTicketCreateRecord> recordList = GetMonthRecord(business, year, month);
            if (recordList.Count >= 5) { message = "抱歉,本月的创建次数已经用完."; return false; }
            int cashTicketTotalAmount = 0;
            foreach (CashTicketCreateRecord r in recordList)
            {
                int amountEach = r.Amount * r.CashTicketTemplate.Amount;
                cashTicketTotalAmount += amountEach;
            }
            int currentTotalAmount = cashticketTemplate.Amount * amountCreated;
            if (currentTotalAmount + cashTicketTotalAmount > 100000)
            {
                message = "抱歉,每月创建的现金券总金额不能大于十万元.您已经创建了" + cashTicketTotalAmount + "元,本月只能再创建"
                    + (100000 - cashTicketTotalAmount) + "元";
                return false;
            }
            return true;
            
        }
  
    }

    
}
