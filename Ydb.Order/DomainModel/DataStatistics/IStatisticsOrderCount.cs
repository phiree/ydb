using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Order.DomainModel.DataStatistics
{
    public interface IStatisticsOrderCount
    {
        IList<StatisticsInfo<ServiceOrderStateChangeHis>> GetOrderStateTimeLine(IList<ServiceOrderStateChangeHis> orderStatusList);
    }
}
