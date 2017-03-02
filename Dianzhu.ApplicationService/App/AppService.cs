using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Push.DomainModel;
using Ydb.Push.Application;
namespace Dianzhu.ApplicationService.App
{
    public class AppService : IAppService
    {

       IDeviceBindService blldevicebind;
        IDZMembershipService memberService;
        public AppService(IDeviceBindService blldevicebind, IDZMembershipService memberService)
        {
            this.blldevicebind = blldevicebind;
            this.memberService = memberService;
        }

        /// <summary>
        /// 注册设备,userID 为空，表示匿名注册
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appobj"></param>
        /// <returns></returns>
        public object PostDeviceBind(string id ,appObj appobj)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new FormatException("appUUID不能为空");
            }
            Guid uuId= utils.CheckGuidID(id, "appUUID");
            //if (appobj.appToken.Length > 64)
            //{
            //    throw new FormatException("Token长度超过64");
            //}
            //else if (appobj.appToken.Length < 64)
            //{
            //    throw new FormatException("Token长度不够64");
            //}
            //Model.DeviceBind devicebind = Mapper.Map<appObj, Model.DeviceBind>(appobj);
             DeviceBind devicebind = new  DeviceBind();
            if (!string.IsNullOrEmpty(appobj.userID))
            {
                Guid userId = utils.CheckGuidID(appobj.userID, "UserId");
                MemberDto dzmembership = memberService.GetUserById(userId.ToString());
                if (dzmembership == null)
                {
                    throw new Exception("该用户不存在!");
                }
                devicebind.DZMembershipId = dzmembership.Id.ToString();
            }
            else
            {
                devicebind.DZMembershipId = "匿名用户";
            }
            DateTime dt= DateTime.Now;
            devicebind.IsBinding = true;
            devicebind.SaveTime = dt;
            devicebind.AppUUID = uuId;
            devicebind.BindChangedTime = dt;
            devicebind.AppName = appobj.appName.ToString();
            devicebind.AppToken = appobj.appToken;
            blldevicebind.UpdateAndSave(devicebind);
            //Model.DeviceBind devicebinduuid = blldevicebind.getDevBindByUUID(uuId);
            //if (devicebinduuid == null)
            //{
            //    devicebind.IsBinding = true;
            //    devicebind.SaveTime = dt;
            //    devicebind.AppUUID = uuId;
            //    devicebind.BindChangedTime = dt;
            //    devicebind.DZMembership = dzmembership;
            //    devicebind.AppName = appobj.appName.ToString();
            //    devicebind.AppToken = appobj.appToken;
            //    blldevicebind.Save(devicebind);
            //}
            //else
            //{
            //devicebinduuid.IsBinding = true;
            //devicebinduuid.BindChangedTime = dt;
            //devicebinduuid.DZMembership = dzmembership;
            //blldevicebind.Update(devicebinduuid);
            //}
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            ////NHibernateUnitOfWork.UnitOfWork.Start();
            //devicebind = blldevicebind.getDevBindByUUID(uuId);
            //if (devicebind!=null && devicebind.BindChangedTime == dt)
            //{
            return new string[] { "注册成功！" };
            //不用进行判断是否修改成功，若是不出异常，还是没有提交成功那就是底层的错误了。
            //}
            //else
            //{
            //    throw new Exception("注册失败");
            //}
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object DeleteDeviceBind(string id)
        {
            Guid uuId=utils.CheckGuidID(id, "appUUID"); 
       DeviceBind obj = blldevicebind.getDevBindByUUID(uuId);
            if (obj == null)
            {
                throw new Exception("该设备没有注册！");
            }
            blldevicebind.Delete(uuId);
            //obj.PushAmount = 0;
            //obj.IsBinding = false;
            //obj.BindChangedTime = DateTime.Now;
            //blldevicebind.SaveOrUpdate(obj);
            return new string[] { "删除成功！" };
        }

        /// <summary>
        /// 更新设备推送计数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pushCount"></param>
        /// <returns></returns>
        public object PatchDeviceBind(string id,string pushCount)
        {
            Guid uuId = utils.CheckGuidID(id, "appUUID");
        DeviceBind obj = blldevicebind.getDevBindByUUID(uuId);
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
            blldevicebind.UpdatePushAmount(uuId, c);
            //obj.PushAmount = c;
            //obj.BindChangedTime = DateTime.Now;
            //blldevicebind.Update(obj);
            //Model.DeviceBind obj1 = blldevicebind.getDevBindByUUID(uuId);
            //blldevicebind.SaveOrUpdate(obj);
            return new string[] { "更新成功！" };
        }
    }
}
