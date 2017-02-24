﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Ydb.Push.DomainModel.IRepository;
using Ydb.Push.DomainModel;

namespace Ydb.Push.Application
{
    public class DeviceBindService : IDeviceBindService
    {
 
        //20150615_longphui_modify
        //public repoDeviceBind repoDeviceBind = DALFactory.repoDeviceBind;
        private IRepositoryDeviceBind repoDeviceBind;
        public DeviceBindService(IRepositoryDeviceBind repoDeviceBind)
        {
            this.repoDeviceBind = repoDeviceBind;
        }
 
        public void UpdateDeviceBindStatus(string memberId,string appToken,string appName)
        {
            repoDeviceBind.UpdateBindStatus(memberId, appToken, appName);
        }

        public void Save(DeviceBind db)
        {
            repoDeviceBind.Add(db);
        }

        public void Update(DeviceBind db)
        {
            repoDeviceBind.Update(db);
        }

        public void SaveOrUpdate(DeviceBind db)
        {
            repoDeviceBind.Update(db);
        }


        /// <summary>
        /// 解除之前所有 apptoken  和 member的绑定,然后保存新的绑定
        /// </summary>
        /// <param name="devicebind"></param>
        public void UpdateAndSave(DeviceBind devicebind)
        {
            repoDeviceBind.UpdateAndSave(devicebind);
        }



        public void Delete(DeviceBind db)
        {
            repoDeviceBind.Delete(db);
        }

        public DeviceBind getDevBindByUUID(Guid uuid)
        {
            return repoDeviceBind.getDevBindByUUID(uuid);
        }
        public DeviceBind getDevBindByUserID(string userId)
        {
            return repoDeviceBind.getDevBindByUserID(new Guid( userId));
        }
    }
}