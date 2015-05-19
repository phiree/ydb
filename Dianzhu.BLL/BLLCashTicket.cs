using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
using PHSuit;
using Dianzhu.IDAL;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 现金券边界类，负责各种操作
    /// </summary>
    public class BLLCashTicket

    {
        IDALCashTicket dal = null;
        public IDALCashTicket DalCashTicket
        {
            get { return dal ?? new DalCashTicket(); }
            set { dal = value; }
        }
        IDALCashTicketCreateRecord idalCashticketCreateRecord = null;
        public IDALCashTicketCreateRecord IDALCashTicketCreateRocord
        {
            get { return idalCashticketCreateRecord ?? new DALCashTicketCreateRecord(); }
            set { idalCashticketCreateRecord = value; }
        }
        public BLLCashTicket()
        { 
        }
        public string CreateBatch(Business_Abs owner, int amount, CashTicketTemplate tt)
        {
            string msg = string.Empty;
            try
            {
                CashTicketGenerator generator = new CashTicketGenerator((Business)owner, tt, amount, IDALCashTicketCreateRocord, DalCashTicket);
               
                 IList<CashTicket> ticketListCreated=   generator.Generate();

                 CashTicketCreateRecord record = new CashTicketCreateRecord();
                 record.Amount = amount;
                 record.Business = (Business)owner;
                 record.CashTickets = ticketListCreated;
                 record.CashTicketTemplate = tt;
                 record.TimeCreated = DateTime.Now;
                 IDALCashTicketCreateRocord.DALBase.Save(record);
                
                
            }
            catch(Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
       
    }
     
}
