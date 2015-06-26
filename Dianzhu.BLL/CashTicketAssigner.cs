using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.IDAL;
using PHSuit;
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
        /// <param name="area">涉及的范围</param>
        private void FindNeighbour(string area)
        {
            IList<Business> businessList = new List<Business>();
            //  每两个商家进行比较 
          Dictionary<Business, List<Business>> all_neighbours = new  Dictionary<Business, List<Business>>();
            for (int i = 0; i < businessList.Count; i++)
            {
                Business b1 = businessList[i];
                if (!all_neighbours.ContainsKey(b1))
                {
                    all_neighbours.Add(b1, new List<Business>());
                }
                
                for (int j = 0; j < businessList.Count-i; j++)
                {
                    Business b2 = businessList[j];
                    if (!all_neighbours.ContainsKey(b2))
                    {
                        all_neighbours.Add(b2, new List<Business>());
                    }

                    
                }
            }

        }
    }
}
