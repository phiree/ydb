using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.App
{
    public class AppService : IAppService
    {

        BLL.BLLDeviceBind blldevicebind;
        BLL.DZMembershipProvider dzm;
        public AppService(BLL.BLLDeviceBind blldevicebind, BLL.DZMembershipProvider dzm)
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
            Guid uuId= utils.CheckGuidID(id, "appUUID");
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
                Guid userId=utils.CheckGuidID(appobj.userID, "UserId");
                devicebind.DZMembership = dzm.GetUserById(userId);
            }
            devicebind.AppUUID = uuId;
            DateTime dt= DateTime.Now;
            devicebind.SaveTime = dt;
            devicebind.BindChangedTime = dt;
            blldevicebind.SaveOrUpdate1(devicebind);
            devicebind = blldevicebind.getDevBindByUUID(uuId);
            if (devicebind!=null && devicebind.BindChangedTime == dt)
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
            Guid uuId=utils.CheckGuidID(id, "appUUID"); 
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

        /// <summary>
        /// 更新设备推送计数
        /// </summary>
        /// <returns>area实体list</returns>
        public object PatchDeviceBind(string id,string pushCount)
        {
            Guid uuId = utils.CheckGuidID(id, "appUUID");
            Model.DeviceBind obj = blldevicebind.getDevBindByUUID(uuId);
            if (obj == null)
            {
                throw new Exception("该设备没有注册！");
            }
            int c = 0;
            try
            {
                c = int.Parse(pushCount);
            }
            catch
            {
                throw new Exception("推送计数应该为正整数！");
            }
            obj.PushAmount = c;
            obj.BindChangedTime = DateTime.Now;
            blldevicebind.SaveOrUpdate(obj);
            return "更新成功！";
        }
    }
}
