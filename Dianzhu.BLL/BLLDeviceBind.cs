using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.IDAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLDeviceBind
    {
 
        //20150615_longphui_modify
        //public DALDeviceBind DALDeviceBind = DALFactory.DALDeviceBind;
        private IDALDeviceBind DALDeviceBind;
        public BLLDeviceBind(IDALDeviceBind DALDeviceBind)
        {
            this.DALDeviceBind = DALDeviceBind;
        }
 
        public void UpdateDeviceBindStatus(DZMembership member,string appToken,string appName)
        { 
         DALDeviceBind.UpdateBindStatus(member, appToken, appName);
        }

        public void Save(DeviceBind db)
        {
            DALDeviceBind.Add(db);
        }

        public void Update(DeviceBind db)
        {
            DALDeviceBind.Update(db);
        }

        public void SaveOrUpdate(DeviceBind db)
        {
            DALDeviceBind.Update(db);
        }


        /// <summary>
        /// 解除之前所有 apptoken  和 member的绑定,然后保存新的绑定
        /// </summary>
        /// <param name="devicebind"></param>
        public void UpdateAndSave(DeviceBind devicebind)
        {
            DALDeviceBind.UpdateAndSave(devicebind);
        }



        public void Delete(DeviceBind db)
        {
            DALDeviceBind.Delete(db);
        }

        public DeviceBind getDevBindByUUID(Guid uuid)
        {
            return DALDeviceBind.getDevBindByUUID(uuid);
        }
        public DeviceBind getDevBindByUserID(DZMembership user)
        {
            return DALDeviceBind.getDevBindByUserID(user.Id);
        }
    }
}
