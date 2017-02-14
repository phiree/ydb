using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.BusinessResource.DomainModel;


namespace Ydb.BusinessResource.DomainModel.Service.DataStatistics
{
    public interface IStatisticsBusinessCount
    {
        StatisticsInfo StatisticsNewBusinessesCountListByTime(IList<Business> businessList, DateTime beginTime, DateTime endTime, bool IsHour);
        StatisticsInfo StatisticsAllBusinessesCountListByTime(IList<Business> businessList, DateTime beginTime, DateTime endTime, bool IsHour);
        StatisticsInfo StatisticsAllBusinessesCountListByLife(IList<Business> businessList);
        StatisticsInfo StatisticsAllBusinessesCountGroupByArea(IList<Business> businessList, IList<Area> areaList);
        StatisticsInfo StatisticsAllBusinessesCountGroupByStaff(IList<Business> businessList);
    }
}
