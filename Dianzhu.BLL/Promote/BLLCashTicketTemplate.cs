﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using PHSuit;
using Dianzhu.DAL;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 现金券生产者。
    /// </summary>
    public class BLLCashTicketTemplate
    {
        /// <summary>
        /// 数据库操作接口应该作为参数传入,便于测试.
        /// </summary>
        public DALCashTicketTemplate DALCashTicketTemplate=DALFactory.DALCashTicketTemplate;
        public BLLCashTicket BLLCashTicket = new BLLCashTicket();
        

        /// <summary>
        /// 创建一张现金券模板
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="owner">所属商家</param>
        /// <param name="validDate">生效日期(次日生效）</param>
        /// <param name="ExpiredDate">失效日期</param>
        /// <param name="amount">面额</param>
        /// <param name="conditions">使用条件</param>
        /// <param name="coverage">覆盖范围</param>
        /// <returns></returns>
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
                Business=owner,
                 ValidDate=PHCore.GetNextDay(DateTime.Now),
                 Enabled=true,
            };
            DALCashTicketTemplate.Add(ctt);
            return ctt;
        }
        public void Update(CashTicketTemplate t)
        {
            DALCashTicketTemplate.Update(t);
        }
        public IList<CashTicketTemplate> GetTemplateList(Business owner)
        {
            return DALCashTicketTemplate.GetListByBusiness(owner);
        }
        public CashTicketTemplate GetOne(Guid id)
        {
            return DALCashTicketTemplate.FindById(id);
        }

        public void SaveOrUpdate(CashTicketTemplate ctt)
        {
            DALCashTicketTemplate.Update(ctt);
        
        }
        public IList<CashTicketTemplate> GetAll()
        {
            return DALCashTicketTemplate.Find(x=>true);
        }

        /// <summary>
        /// 用户领取一张优惠券
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="template"></param>
        public CashTicket ClaimForAnCashTicket(DZMembership customer, Guid templateId,out string errMsg)
        {
            CashTicketTemplate template = GetOne(templateId);
              DateTime now=DateTime.Now;
              errMsg = string.Empty;
              if (!template.Enabled)
              {
                  errMsg = "现金券已禁用";
                  return null;
              }
              if (now > template.ExpiredDate)
              {
                  errMsg = "现金券已过期";
                  return null;
              }
              if (now < template.ValidDate)
              {
                  errMsg = "现金券尚未开启";
                  return null;
              }
              var validCashTickets = template.CashTicketsReadyForClaim;
              if (validCashTickets.Count == 0)
              {
                  errMsg = "已被领完";
                  return null;
              }
             CashTicket ticket= validCashTickets.First();
             ticket.UserAssigned = customer;

             BLLCashTicket.Update(ticket);
             return ticket;
        }
        
    }
}
