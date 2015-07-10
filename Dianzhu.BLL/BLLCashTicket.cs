using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
using PHSuit;
using Dianzhu.Model.Enums;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 现金券边界类，负责各种操作
    /// </summary>
    public class BLLCashTicket
    {
       
        public DALCashTicket DALCashTicket=DALFactory.DALCashTicket;

        public DALCashTicketCreateRecord DALCashTicketCreateRecord = DALFactory.DALCashTicketCreateRecord;
        
        public BLLCashTicket()
        {
        }
        public string CreateBatch(Business_Abs owner, int amount, CashTicketTemplate tt)
        {
            string msg = string.Empty;
            try
            {
                CashTicketGenerator generator = new CashTicketGenerator((Business)owner, tt, amount);

                IList<CashTicket> ticketListCreated = generator.Generate();

                CashTicketCreateRecord record = new CashTicketCreateRecord();
                record.Amount = amount;
                record.Business = (Business)owner;
                record.CashTickets = ticketListCreated;
                record.CashTicketTemplate = tt;
                record.TimeCreated = DateTime.Now;
                DALCashTicketCreateRecord.Save(record);


            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }

        public IList<CashTicket> GetListForBusiness(Guid businessId)
        {
            return DALCashTicket.GetCashTicketListForBusiness(businessId);
        }
        public void SaveOrUpdate(CashTicket cashticket)
        {
            DALCashTicket.SaveOrUpdate(cashticket);
        }
        public IList<CashTicket> GetListForCustomer(Guid memebrId)
        {
            return DALCashTicket.GetListForCustomer(memebrId);
        }
        public int GetCount(Guid memberid, enum_CashTicketSearchType searchType)
        {
            return DALCashTicket.GetCount(memberid, searchType);
        }
        public IList<CashTicket> GetCashTicketList(Guid memberId, enum_CashTicketSearchType searchType, int pageIndex, int pageSize)
        {
            return DALCashTicket.GetCashTicketList(memberId, searchType, pageIndex, pageSize);
        }

        
    }

}
