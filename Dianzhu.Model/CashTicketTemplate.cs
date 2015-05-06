using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
 
namespace Dianzhu.Model
{
 
    /// <summary>
    /// 现金券模板
    /// </summary>
    public  class CashTicketTemplate
    {
         
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 所属商家
        /// </summary>
        public virtual Business Owner { get; set; }
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
        public int Amount { get; set; }
        /// <summary>
        /// 使用条件
        /// </summary>
        public string Conditions { get; set; }
        /// <summary>
        /// 覆盖范围。单位公里。
        /// </summary>
        public float Coverage { get; set; }
    }
}
