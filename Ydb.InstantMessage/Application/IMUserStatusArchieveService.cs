using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.InstantMessage.DomainModel.DataStatistics;
using Ydb.Common;

namespace Ydb.InstantMessage.Application
{
    public class IMUserStatusArchieveService: IIMUserStatusArchieveService
    {
        IRepositoryIMUserStatusArchieve repositoryIMUserStatusArchieve;
        IStatisticsInstantMessage statisticsInstantMessage;
        public IMUserStatusArchieveService(IRepositoryIMUserStatusArchieve repositoryIMUserStatusArchieve, IStatisticsInstantMessage statisticsInstantMessage)
        {
            this.repositoryIMUserStatusArchieve = repositoryIMUserStatusArchieve;
            this.statisticsInstantMessage = statisticsInstantMessage;
        }

        public void Save(IMUserStatusArchieve im)
        {
            repositoryIMUserStatusArchieve.Add(im);
        }

        /// <summary>
        /// 获取用户累计在线时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserTotalOnlineTime(string userId)
        {
            IList<IMUserStatusArchieve> imUserStatusArchieveList = repositoryIMUserStatusArchieve.GetAllUserStatusArchieveById(userId);
            return statisticsInstantMessage.StatisticsUserTotalOnlineTime(imUserStatusArchieveList);
        }
    }
}
