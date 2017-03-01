using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.Application
{
    public interface IIMUserStatusArchieveService
    {
        void Save(IMUserStatusArchieve im);

        /// <summary>
        /// 获取用户累计在线时间（秒）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetUserTotalOnlineTime(string userId);
    }
}
