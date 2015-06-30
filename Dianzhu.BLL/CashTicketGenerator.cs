using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using PHSuit;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 根据现金券模板批量生成现金券,该类应该位于 dal层之上,bll层之下,
    /// </summary>
   public class CashTicketGenerator
    {
       Business business;
       CashTicketTemplate cashticketTemplate;
       int amount;
      public DALCashTicketCreateRecord DALCashTicketCreateRecord=DALFactory.DALCashTicketCreateRecord;


      public DALCashTicket DALCashTicket = DALFactory.DALCashTicket;
       public CashTicketGenerator(Business business, CashTicketTemplate cashticketTemplate, int amount)
       {
           this.amount = amount;
           this.cashticketTemplate = cashticketTemplate;
           this.business = business;
           
       }
       private bool CheckRule(int year, int month, int amountCreated, CashTicketTemplate cashticketTemplate, out string message)
       {
           message = string.Empty;
           IList<CashTicketCreateRecord> recordList = DALCashTicketCreateRecord.GetMonthRecord(business, year, month);
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

       public List<CashTicket> Generate()
       {
           string message;
          bool result= CheckRule(DateTime.Now.Year, DateTime.Now.Month, this.amount, this.cashticketTemplate, out message);
          if (!result)
          {
              throw new Exception(message);
          }
           string[] code_list = GenerateCodeList();

           List<CashTicket> ct_list = new List<CashTicket>();
           for (int i = 0; i < amount; i++)
           {
               CashTicket ct = new CashTicket();
               ct.CashTicketTemplate = cashticketTemplate;
               ct.TicketCode = code_list[i];
               ct_list.Add(ct);

           }
           DALCashTicket.SaveList(ct_list);
           return ct_list;
       }
       private string[] GenerateCodeList()
       {
           List<string> valid_codes = new List<string>();
           for (int i = 0; i < amount; i++)
           {
               string code = PHCore.GetRandom(12);
               if (!DALCashTicket.CheckTicketCodeExists(code))
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
