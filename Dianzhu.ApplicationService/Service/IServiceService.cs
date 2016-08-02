using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Service
{
    public interface IServiceService
    {
        /// <summary>
        /// 新建服务
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="servicesobj"></param>
        /// <returns></returns>
        servicesObj PostService(string storeID, servicesObj servicesobj);

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="filter"></param>
        /// <param name="servicefilter"></param>
        /// <returns></returns>
        IList<servicesObj> GetServices(string storeID, common_Trait_Filtering filter, common_Trait_ServiceFiltering servicefilter);

        /// <summary>
        /// 统计服务的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="servicefilter"></param>
        /// <returns></returns>
        countObj GetServicesCount(string storeID, common_Trait_ServiceFiltering servicefilter);

        /// <summary>
        /// 读取服务 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        servicesObj GetService(string storeID, string serviceID);

        /// <summary>
        /// 更新服务信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="servicesobj"></param>
        /// <returns></returns>
        servicesObj PatchService(string storeID, string serviceID, servicesObj servicesobj);

        /// <summary>
        /// 删除服务 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        object DeleteService(string storeID, string serviceID);

        /// <summary>
        /// 查询 superID 的下级服务类型列表数组,当 superID 为空时，默认查询顶层服务类型列表
        /// </summary>
        /// <param name="superID"></param>
        /// <returns></returns>
        IList<serviceTypeObj> GetAllServiceTypes(string superID);
    }
}
