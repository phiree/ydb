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
        public string CreateBatch(Business_Abs owner, int amount, CashTicketTemplate tt)
        {
            string msg = string.Empty;
            try
            {
                CashTicketBatchCreator batchCreateor = new CashTicketBatchCreator(owner, amount, tt);
            }
            catch(Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
    }
    public class CashTicketBatchCreator {
        public Business_Abs Owner { get; set; }
        public int Amount { get; set; }
        public CashTicketTemplate TT { get; set; }
        IDALCashTicket dal = null;
        public CashTicketBatchCreator(Business_Abs owner, int amount, CashTicketTemplate tt, IDALCashTicket idal)
        {
            this.Owner = owner;
            this.Amount = amount;
            this.TT = tt;
            dal = DalFactory.GetDalCashTicket();
        }
        public CashTicketBatchCreator(Business_Abs owner,int amount,CashTicketTemplate tt)
                                :this(owner,amount,tt,new DalCashTicket())
        {
        }
        /// <summary>
        /// 批量生成现金券.
        /// </summary>
        /// <returns></returns>
        public IList<CashTicket> BatchCreate()
        {
            string[] code_list=GenerateCodeList();
            List<CashTicket> ct_list = new List<CashTicket>();
            for (int i = 0; i < Amount; i++)
            {
                CashTicket ct = new CashTicket();
                ct.CashTicketTemplate = TT;
                ct.TicketCode = code_list[i];
                ct_list.Add(ct);
                
            }
            dal.DalBase.SaveList(ct_list);
            return ct_list;
        }
        /// <summary>
        /// 生成可以使用的优惠券编码。
        /// </summary>
        /// <returns></returns>
        private string[] GenerateCodeList()
        {
            List<string> valid_codes = new List<string>();
            for (int i = 0; i < Amount; i++)
            {
               string code =PHCore.GetRandom(12);
               if (!dal.CheckTicketCodeExists(code))
               {
                   valid_codes.Add(code);
               }
               else
               {
                   i--;
               }
              
            }
            return valid_codes.ToArray();
            

        }
    }
}
