using System;
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
                Owner=owner,
                 ValidDate=PHCore.GetNextDay(DateTime.Now),
                 Enabled=true,
            };
            DALCashTicketTemplate.Save(ctt);
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
            return DALCashTicketTemplate.GetOne(id);
        }

        public void SaveOrUpdate(CashTicketTemplate ctt)
        {
            DALCashTicketTemplate.SaveOrUpdate(ctt);
        }
        
    }
}
