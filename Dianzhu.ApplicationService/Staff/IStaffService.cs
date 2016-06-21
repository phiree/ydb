using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Staff
{
    public interface IStaffService
    {
        /// <summary>
        /// 新建员工 店铺的员工
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffobj"></param>
        /// <returns></returns>
        staffObj PostStaff(string storeID, staffObj staffobj);

        /// <summary>
        /// 条件读取员工
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="filter"></param>
        /// <param name="stafffilter"></param>
        /// <returns></returns>
        IList<staffObj> GetStaffs(string storeID, common_Trait_Filtering filter, common_Trait_StaffFiltering stafffilter);

        /// <summary>
        /// 统计服务员工的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="stafffilter"></param>
        /// <returns></returns>
        countObj GetStaffsCount(string storeID, common_Trait_StaffFiltering stafffilter);

        /// <summary>
        /// 读取员工 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        staffObj GetStaff(string storeID, string staffID);

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <param name="staffobj"></param>
        /// <returns></returns>
        staffObj PatchStaff(string storeID, string staffID, staffObj staffobj);

        /// <summary>
        /// 删除员工 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        object DeleteStaff(string storeID, string staffID);
    }
}
