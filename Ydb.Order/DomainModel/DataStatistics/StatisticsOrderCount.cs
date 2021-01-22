using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;


namespace Ydb.Order.DomainModel.DataStatistics
{
    public class StatisticsOrderCount: IStatisticsOrderCount
    {
        public IList<StatisticsInfo<ServiceOrderStateChangeHis>> GetOrderStateTimeLine(IList<ServiceOrderStateChangeHis> orderStatusList)
        {
            IList<StatisticsInfo<ServiceOrderStateChangeHis>> StatusTimeLineList = new List<StatisticsInfo<ServiceOrderStateChangeHis>>();
            orderStatusList = orderStatusList.OrderByDescending(x => x.CreatTime).ToList();
            DateTime date = DateTime.MinValue;
            ServiceOrder order = new ServiceOrder();
            foreach (ServiceOrderStateChangeHis orderstate in orderStatusList)
            {
                orderstate.NewStatusCon = order.GetStatusContextFriendly(orderstate.NewStatus);
                orderstate.NewStatusStr = order.GetStatusTitleFriendly(orderstate.NewStatus);
                DateTime dt = DateTime.Parse(orderstate.CreatTime.ToString("yyyy-MM-dd"));
                if (date != dt)
                {
                    date = dt;
                    StatisticsInfo<ServiceOrderStateChangeHis> StatusTimeLine = new StatisticsInfo<ServiceOrderStateChangeHis> { Date = date, List = new List<ServiceOrderStateChangeHis>() };
                    StatusTimeLineList.Add(StatusTimeLine);
                }
                StatusTimeLineList[StatusTimeLineList.Count - 1].List.Add(orderstate);
            }
            return StatusTimeLineList;
        }

    }
}
