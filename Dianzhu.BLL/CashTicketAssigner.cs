using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.IDAL;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 分配现金券到商圈.
    /// </summary>
    public class CashTicketAssigner
    {
        public CashTicketAssigner()
        { }
        /// <summary>
        /// 准备需要分配的现金券,(尚未使用的,未被停用的),同一个城市的
        /// </summary>

        private void PrepareCashTickets()
        { 
            
        }
        /// <summary>
        /// 构建商家的邻居列表
        /// </summary>
        /// <param name="city"></param>
        private void FindNeighbour(string city)
        { }
    }
}
