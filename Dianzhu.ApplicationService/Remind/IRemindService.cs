using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Remind
{
    public interface IRemindService
    {

        /// <summary>
        /// 条件读取提醒
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="remind"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        IList<remindObj> GetReminds(common_Trait_Filtering filter, common_Trait_RemindFiltering remind, Customer customer);

        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <param name="remind"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        countObj GetRemindsCount(common_Trait_RemindFiltering remind, Customer customer);

        /// <summary>
        /// 根据ID获取提醒
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        remindObj GetRemindById(string remindID, Customer customer);

        /// <summary>
        /// 根据ID删除提醒
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        object DeleteRemindById(string remindID, Customer customer);
    }
}
