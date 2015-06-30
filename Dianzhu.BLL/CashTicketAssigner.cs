using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;

using PHSuit;
using System.Device.Location;
namespace Dianzhu.BLL
{

    /// <summary>
    /// 分配现金券到商圈.
    /// </summary>
    public class CashTicketAssigner
    {
        BLLCashTicket bllCashTicket = new BLLCashTicket();
        BLLBusiness bllBusiness = new BLLBusiness();
        
        public CashTicketAssigner()
        { }
        /// <summary>
        /// 准备需要分配的现金券,(尚未使用的,未被停用的),同一个城市的
        /// </summary>

        private void PrepareCashTickets()
        {
            //
            IList<Business> businessList = new List<Business>();
            Dictionary<Business, List<Business>> all_neighbours=FindNeighbour(businessList);
            IList<CashTicket> tickets = new List<CashTicket>();

            foreach (Business b in businessList)
            {
                IList<CashTicket> businessCashTickts = bllCashTicket.GetListForBusiness(b.Id);
                int amount_tickets = businessCashTickts.Count;
                //随机分配到相邻的商户
                int amount_neighbours = all_neighbours[b].Count;
                IList<int> assign_amounts = PHSuit.PHNumber.Divid(amount_tickets, amount_neighbours);
                for(int i=0;i<amount_neighbours;i++)
                {
                    IList<CashTicket> for_a_neighbour = businessCashTickts.Take<CashTicket>(assign_amounts[i]).ToList();

                    foreach (CashTicket t in for_a_neighbour)
                    {
                        businessCashTickts.Remove(t);
                    }
                }

            }
        }
        /// <summary>
        /// 构建商家的邻居列表
        /// </summary>
        /// <param name="area">涉及的范围</param>
        private Dictionary<Business, List<Business>> FindNeighbour(IList<Business> businessList)
        {
          
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
                    GeoCoordinate geo = new GeoCoordinate(b1.Latitude, b1.Longitude);
                    GeoCoordinate geo2 = new GeoCoordinate(b2.Latitude, b2.Longitude);
                    double distance= geo.GetDistanceTo(geo2);
                    if (distance <= b1.PromoteScope)
                    {
                        all_neighbours[b1].Add(b2);
                    }
                    if (distance <= b2.PromoteScope)
                    {
                        all_neighbours[b2].Add(b1);
                    }
                    
                }
            }
            return all_neighbours;

        }
    }
}
