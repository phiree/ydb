using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.WorkTime
{
    public interface IWorkTimeService
    {

        /// <summary>
        /// 新建工作时间
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="worktimeobj"></param>
        /// <returns></returns>
        workTimeObj PostWorkTime(string storeID, string serviceID, workTimeObj worktimeobj);

        /// <summary>
        /// 条件读取工作时间
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="worktime"></param>
        /// <returns></returns>
        IList<workTimeObj> GetWorkTimes(string storeID, string serviceID, common_Trait_WorkTimeFiltering worktime);

        /// <summary>
        /// 统计工作时间的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="worktime"></param>
        /// <returns></returns>
        countObj GetWorkTimesCount(string storeID, string serviceID, common_Trait_WorkTimeFiltering worktime);

        /// <summary>
        /// 读取工作时间
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <returns></returns>
        workTimeObj GetWorkTime(string storeID, string serviceID, string workTimeID);

        /// <summary>
        /// 更新工作时间信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <param name="worktimeobj"></param>
        /// <returns></returns>
        workTimeObj PatchWorkTime(string storeID, string serviceID, string workTimeID, workTimeObj worktimeobj);

        /// <summary>
        /// 删除服务 工作时间信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <returns></returns>
        object DeleteWorkTime(string storeID, string serviceID, string workTimeID);
    }
}
