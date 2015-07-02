using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
 
namespace Dianzhu.Model
{
 
    /// <summary>
    /// 现金券分派给商家的记录 
    /// </summary>
    public  class CashTicketAssignRecord
    {
        public CashTicketAssignRecord()
        {
            AssignDetail = new List<CashTIcketAssignDetail>();
        }
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 分派开始时间
        /// </summary>
        public virtual DateTime TimeBegin { get; set; }

        /// <summary>
        /// 分发的范围(行政区域)
        /// </summary>
        public virtual Area Area { get; set; }
        /// <summary>
        /// 分派结束时间
        /// </summary>
        public virtual DateTime TimeEnd { get; set; }
        /// <summary>
        /// 商家总数
        /// </summary>
        public virtual int AmountBusiness{ get; set; }
        /// <summary>
        ///现金券总数
        /// </summary>
        public virtual int  AmountCashTicket{ get; set; }

        /// <summary>
        /// 分派结果
        /// </summary>
        public virtual bool IsSuccess { get; set; }
        public virtual IList<CashTIcketAssignDetail> AssignDetail { get; set; }
        
       
        
    }
    
   
}
