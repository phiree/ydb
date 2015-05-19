using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
 
namespace Dianzhu.Model
{
 
    /// <summary>
    /// 现金券生成操作的记录 
    /// </summary>
    public  class CashTicketCreateRecord
    {
        public CashTicketCreateRecord()
        {
            
        }
        
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 生成时间
        /// </summary>
        public virtual DateTime TimeCreated { get; set; }
        /// <summary>
        /// 生成数量
        /// </summary>
        public virtual int Amount{ get; set; }
        /// <summary>
        ///操作员
        /// </summary>
        public virtual string  Operator { get; set; }
        /// <summary>
        /// 所属商家
        /// </summary>
        public virtual Business_Abs Business { get; set; }
        /// <summary>
        /// 现金券模板
        /// </summary>
        public virtual CashTicketTemplate CashTicketTemplate { get; set; }

        public virtual IList<CashTicket> CashTickets { get; set; }

        

        
       
        
    }
    
  
}
