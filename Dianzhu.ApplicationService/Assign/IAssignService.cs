using System.Collections.Generic;

namespace Dianzhu.ApplicationService.Assign
{
    public interface IAssignService
    {
        /// <summary>
        /// 新建指派
        /// </summary>
        /// <param name="assignobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        assignObj PostAssign(assignObj assignobj, Customer customer);

        /// <summary>
        /// 条件读取指派
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        IList<assignObj> GetAssigns(common_Trait_Filtering filter, common_Trait_AssignFiltering assign, Customer customer);

        /// <summary>
        /// 统计指派的数量
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        countObj GetAssignsCount(common_Trait_AssignFiltering assign, Customer customer);

        /// <summary>
        /// 取消指派
        /// </summary>
        /// <param name="assignobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        object DeleteAssign(assignObj assignobj, Customer customer);
    }
}