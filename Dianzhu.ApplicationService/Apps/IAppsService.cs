using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Apps
{
    public interface IAppsService
    {
        /// <summary>
        /// 注册设备,userID 为空，表示匿名注册
        /// </summary>
        /// <returns>area实体list</returns>
        object PostDeviceBind(string id, appObj appobj);

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <returns>area实体list</returns>
        object DeleteDeviceBind(string id);
    }
}
