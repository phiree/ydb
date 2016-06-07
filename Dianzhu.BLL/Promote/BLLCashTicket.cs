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

        IDAL.IDALCashTicket dalCashTicket;
        public BLLCashTicket(IDAL.IDALCashTicket dalCashTicket)
        {
            this.dalCashTicket = dalCashTicket;
        }
        public DALCashTicketCreateRecord DALCashTicketCreateRecord = DALFactory.DALCashTicketCreateRecord;

        public CashTicket GetOne(Guid id)
        {
            return dalCashTicket.FindById(id);
        }
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
            return dalCashTicket.GetCashTicketListForBusiness(businessId);
        }
        public void Update(CashTicket cashticket)
        {
            dalCashTicket.Update(cashticket);
        }
        public IList<CashTicket> GetListForCustomer(Guid memebrId)
        {
            return dalCashTicket.GetListForCustomer(memebrId);
        }
        public int GetCount(Guid memberid, enum_CashTicketSearchType searchType)
        {
            return dalCashTicket.GetCount(memberid, searchType);
        }
        public IList<CashTicket> GetCashTicketList(Guid memberId, enum_CashTicketSearchType searchType, int pageIndex, int pageSize)
        {
            return dalCashTicket.GetCashTicketList(memberId, searchType, pageIndex, pageSize);
        }

        
    }

}
