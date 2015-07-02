using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using PHSuit;
using System.Device.Location;
using log4net;
namespace Dianzhu.BLL
{
    
    /// <summary>
    ///优惠券分配,供计时器调用,
    /// </summary>
    public class CashTicketAssigner_Task
    {
        private static readonly ILog log = LogManager.GetLogger("dz");
        public CashTicketAssigner_Task()
        {
            log.Debug("task_fired");
        }
         
    }
    /// <summary>
    /// 分配现金券到商圈.
    /// </summary>
    public class CashTicketAssigner
    {

        /// <summary>
        /// 参与现金券分配的商家列表.
        /// </summary>
        public IList<Business> BusinessList { get; set; }


        private CashTicketAssignRecord cashTicketAssignRecord;
        public CashTicketAssigner()
        { }
        public CashTicketAssigner(CashTicketAssignRecord cashTicketAssignRecord)
        {
            this.cashTicketAssignRecord = cashTicketAssignRecord;
        }

        
        /// <summary>
        /// 准备需要分配的现金券,(尚未使用的,未被停用的),同一个城市的
 
        /// </summary>

        public void Assign()
        {
            Dictionary<Business, IList<Business>> all_neighbours = FindNeighbour(BusinessList);
             
            foreach (Business b in this.BusinessList)
            {
                AssignForOne(b, all_neighbours[b], 0.2m);
            }
        }
        /// <summary>
        /// 将一个商家的优惠券分配给推广范围内的邻居商家.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="neighbours">邻居商家</param>
        /// <param name="percentKeepForSelf">自己保留的比例</param>
        public void AssignForOne(Business b, IList<Business> neighbours,decimal percentKeepForSelf)
        {
            foreach (CashTicketTemplate t in b.CashTicketTemplates)
            {
                //重新分配 已经启用 而且还未被领取的券
                var cashTickets = t.CashTickets.Where(x=>x.CashTicketTemplate.Enabled==true&&
                    x.UserAssigned==null
                    ).ToList();
                //为自己保留
                int amountKeep =(int)(cashTickets.Count * percentKeepForSelf);
                var cashTicketsForSelf = cashTickets.Take<CashTicket>(amountKeep).ToList();
                cashTicketsForSelf.ForEach(x => x.BusinessAssigned = b);
                
                foreach (CashTicket ct in cashTicketsForSelf)
                {
                    ct.BusinessAssigned = b;
                    ct.CashTicketAssigneRecord = this.cashTicketAssignRecord;
                    
                }
                //分配给邻居商户
                if (neighbours.Count == 0)
                {
                    continue;
                }
                int amountToBeAssign = cashTickets.Count - amountKeep;
                //每个邻居要分配的数量
                
                IList<int> assign_amounts = PHSuit.PHNumber.Divid(amountToBeAssign, neighbours.Count);
                for (int i=0;i< assign_amounts.Count;i++)
                {
                    var neighbour = neighbours[i];
                    var tickets_for_the_neighbour = cashTickets
                        .Where(x => x.BusinessAssigned == null).Take<CashTicket>(assign_amounts[i]).ToList();;
                    tickets_for_the_neighbour.ForEach(x => x.BusinessAssigned = neighbour);
                }
                cashTickets.ForEach(x => x.CashTicketAssigneRecord = this.cashTicketAssignRecord);
                
            }
        }

        /// <summary>
        /// 构建商家的邻居列表
        /// </summary>
        /// <param name="area">涉及的范围</param>
        public Dictionary<Business, IList<Business>> FindNeighbour(IList<Business> businessList)
        {

            //  每两个商家进行比较 
            Dictionary<Business, IList<Business>> all_neighbours = new Dictionary<Business, IList<Business>>();
            for (int i = 0; i < businessList.Count - 1; i++)
            {
                Business b1 = businessList[i];
                if (!all_neighbours.ContainsKey(b1))
                {
                    all_neighbours.Add(b1, new List<Business>());
                }

                for (int j = i + 1; j <= businessList.Count - j; j++)
                {
                    Business b2 = businessList[j];
                    if (!all_neighbours.ContainsKey(b2))
                    {
                        all_neighbours.Add(b2, new List<Business>());
                    }
                    GeoCoordinate geo = new GeoCoordinate(b1.Latitude, b1.Longitude);
                    GeoCoordinate geo2 = new GeoCoordinate(b2.Latitude, b2.Longitude);
                    double distance = geo.GetDistanceTo(geo2);
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
