using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Apps
{
    public class AppsService : IAppsService
    {

        BLL.BLLDeviceBind blldevicebind;
        BLL.DZMembershipProvider dzm;
        public AppsService(BLL.BLLDeviceBind blldevicebind, BLL.DZMembershipProvider dzm)
        {
            this.blldevicebind = blldevicebind;
            this.dzm = dzm;
        }

        /// <summary>
        /// 注册设备,userID 为空，表示匿名注册
        /// </summary>
        /// <returns>area实体list</returns>
        public object PostDeviceBind(string id ,appObj appobj)
        {
            Guid uuId;
            Guid userId;
            bool uuidisGuid = Guid.TryParse(id, out uuId);
            if (!uuidisGuid)
            {
                throw new Exception("appUUID格式有误");
            }
            if (appobj.appToken.Length > 64)
            {
                throw new Exception("Token长度超过64");
            }
            else if (appobj.appToken.Length < 64)
            {
                throw new Exception("Token长度不够64");
            }
            Model.DeviceBind devicebind = Mapper.Map<appObj, Model.DeviceBind>(appobj);
            devicebind.IsBinding = true;
            if (appobj.userID != null && appobj.userID != "")
            {
                bool isGuid = Guid.TryParse(appobj.userID, out userId);
                if (!isGuid)
                {
                    throw new Exception("UserId格式有误");
                }
                devicebind.DZMembership = dzm.GetUserById(userId);
            }
            devicebind.AppUUID = uuId;
            DateTime dt= DateTime.Now;
            devicebind.SaveTime = dt;
            devicebind.BindChangedTime = dt;
            blldevicebind.SaveOrUpdate1(devicebind);
            devicebind = blldevicebind.getDevBindByUUID(uuId);
            if (devicebind!=null && devicebind.SaveTime == dt)
            {
                return "注册成功！";
            }
            else
            {
                throw new Exception("注册失败");
            }
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <returns>area实体list</returns>
        public object DeleteDeviceBind(string id)
        {
            Guid uuId;
            bool uuidisGuid = Guid.TryParse(id, out uuId);
            if (!uuidisGuid)
            {
                throw new Exception("appUUID格式有误");
            }
            Model.DeviceBind obj = blldevicebind.getDevBindByUUID(uuId);
            if (obj == null)
            {
                throw new Exception("该设备没有注册！");
            }
            obj.PushAmount = 0;
            obj.IsBinding = false;
            obj.BindChangedTime = DateTime.Now;
            blldevicebind.SaveOrUpdate(obj);
            return "删除成功！";
        }
    }
}
