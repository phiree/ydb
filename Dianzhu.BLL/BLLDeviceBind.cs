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
        public DALDeviceBind DALDeviceBind = DALFactory.DALDeviceBind;
 
        public void UpdateDeviceBindStatus(DZMembership member,string appToken,string appName)
        { 
            
     
         DALDeviceBind.UpdateBindStatus(member, appToken, appName);
        }

        public void SaveOrUpdate(DeviceBind db)
        {
            DALDeviceBind.SaveOrUpdate(db);
        }

        public void Delete(DeviceBind db)
        {
            DALDeviceBind.Delete(db);
        }

        public DeviceBind getDevBindByUUID(Guid uuid)
        {
            return DALDeviceBind.getDevBindByUUID(uuid);
        }
    }
}
