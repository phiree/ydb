using System;
using Ydb.Push.DomainModel;

namespace Ydb.Push.Application
{
    public interface IDeviceBindService
    {
        void Delete(Guid uuid);
        void UpdatePushAmount(Guid uuid, int pushAmount);
        DeviceBind getDevBindByUserID(string userId);
        DeviceBind getDevBindByUUID(Guid uuid);
        void SetPushAmountZero(Guid userId);
        void Save(DeviceBind db);
        void SaveOrUpdate(DeviceBind db);
        void Update(DeviceBind db);
        void UpdateAndSave(DeviceBind devicebind);
        void UpdateDeviceBindStatus(string memberId, string appToken, string appName);
    }
}