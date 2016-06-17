using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLDeviceBind
    {
        public IDAL.IDALDeviceBind DALDeviceBind;

        public BLLDeviceBind(IDAL.IDALDeviceBind dal)
        {
            DALDeviceBind = dal;
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
            return DALDeviceBind.getDevBindByUserID(user);
        }
    }
}
