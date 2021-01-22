using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Ydb.Push.DomainModel.IRepository;
using Ydb.Push.DomainModel;
using Ydb.Push.Infrastructure.NHibernate.UnitOfWork;

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
        [UnitOfWork]
        public void UpdateDeviceBindStatus(string memberId,string appToken,string appName)
        {
            repoDeviceBind.UpdateBindStatus(memberId, appToken, appName);
        }
        [UnitOfWork]
        public void SetPushAmountZero(Guid userId)
        {
            DeviceBind dBind = getDevBindByUserID(userId.ToString());
            if (dBind != null)
            {
                dBind.SetPushAmountZero();
            }

        }
        [UnitOfWork]
        public void Save(DeviceBind db)
        {
            repoDeviceBind.Add(db);
        }
        [UnitOfWork]
        public void Update(DeviceBind db)
        {
            repoDeviceBind.Update(db);
        }
        [UnitOfWork]
        public void SaveOrUpdate(DeviceBind db)
        {
            repoDeviceBind.Update(db);
        }


        /// <summary>
        /// 解除之前所有 apptoken  和 member的绑定,然后保存新的绑定
        /// </summary>
        /// <param name="devicebind"></param>
        [UnitOfWork]
        public void UpdateAndSave(DeviceBind devicebind)
        {
            repoDeviceBind.UpdateAndSave(devicebind);
        }



        [UnitOfWork]
        public void Delete(Guid uuid)
        {
            DeviceBind db= repoDeviceBind.getDevBindByUUID(uuid);
            db.PushAmount = 0;
            db.IsBinding = false;
            db.BindChangedTime = DateTime.Now;
            Update(db);
            //repoDeviceBind.Delete(db);
        }

        [UnitOfWork]
        public void UpdatePushAmount(Guid uuid,int pushAmount)
        {
            DeviceBind db = repoDeviceBind.getDevBindByUUID(uuid);
            db.PushAmount = pushAmount;
            db.BindChangedTime = DateTime.Now;
            Update(db);
            //repoDeviceBind.Delete(db);
        }

        [UnitOfWork]
        public DeviceBind getDevBindByUUID(Guid uuid)
        {
            return repoDeviceBind.getDevBindByUUID(uuid);
        }
        [UnitOfWork]
        public DeviceBind getDevBindByUserID(string userId)
        {
            return repoDeviceBind.getDevBindByUserID(new Guid( userId));
        }
    }
}
