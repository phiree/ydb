using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.IDAL.Finance
{
    public interface IDALBalanceFlow:IRepository<Dianzhu.Model.Finance.BalanceFlow,Guid>
    {
        /// <summary>
        /// 统计账单结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="serviceTypeLevel"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        IList<Model.Finance.BalanceFlow> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string dateType);


    }
}
