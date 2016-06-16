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
        /// <returns>area实体list</returns>
        IList<remindObj> GetReminds(common_Trait_Filtering filter, common_Trait_RemindFiltering remind);

        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <returns>area实体list</returns>
        countObj GetRemindsCount(common_Trait_RemindFiltering remind);

        /// <summary>
        /// 根据ID获取提醒
        /// </summary>
        remindObj GetRemindById(string remindID);
    }
}
