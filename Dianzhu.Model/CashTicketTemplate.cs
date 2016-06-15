﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
 
namespace Dianzhu.Model
{
 
    /// <summary>
    /// 现金券模板
    /// </summary>
    public  class CashTicketTemplate:DDDCommon.Domain.Entity<Guid>
    {
         
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 所属商家
        /// </summary>
        public virtual Business Business { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public virtual DateTime ValidDate { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public virtual DateTime ExpiredDate { get; set; }
        /// <summary>
        /// 面额
        /// </summary>
        public virtual int Amount { get; set; }
        /// <summary>
        /// 使用条件
        /// </summary>
        public virtual string Conditions { get; set; }
        /// <summary>
        /// 覆盖范围。单位公里。
        /// </summary>
        public virtual float Coverage { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool Enabled { get; set; }
        /// <summary>
        /// 自留比例 百分比数,如20 代表20%
        /// </summary>
        public virtual int KeepPercent { get; set; }

        /// <summary>
        /// 该模板生成的现金券.
        /// </summary>
        public virtual IList<CashTicket> CashTickets { get; set; }

        /// <summary>
        /// 可以被领用的现金券
        /// </summary>
        public virtual IList<CashTicket> CashTicketsReadyForClaim {

            get {
                var list = CashTickets.Where(x => x.BusinessAssigned != null
                    && x.UserAssigned == null).ToList();
                return list;
                
            }
        }
    }
}
