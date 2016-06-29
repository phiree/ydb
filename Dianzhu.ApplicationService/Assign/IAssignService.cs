using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Assign
{
    public interface IAssignService
    {
        /// <summary>
        /// 新建指派
        /// </summary>
        /// <param name="assignobj"></param>
        /// <returns></returns>
        assignObj PostAssign(assignObj assignobj);

        /// <summary>
        /// 条件读取指派
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <returns></returns>
        IList<assignObj> GetAssigns(common_Trait_Filtering filter, common_Trait_AssignFiltering assign);

        /// <summary>
        /// 统计指派的数量
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        countObj GetAssignsCount(common_Trait_AssignFiltering assign);
    }
}
