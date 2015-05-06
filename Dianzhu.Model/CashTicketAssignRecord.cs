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
            
        }
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 分派时间
        /// </summary>
        public virtual DateTime TimeAssigned { get; set; }
        /// <summary>
        /// 商家总数
        /// </summary>
        public virtual int AmountBusiness{ get; set; }
        /// <summary>
        ///现金券总数
        /// </summary>
        public virtual int  AmountCashTicket{ get; set; }
        
       
        
    }
    
   
}
