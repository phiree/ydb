using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.IDAL
{
    public interface IDALDeviceBind  :IRepository<DeviceBind,Guid>
    {

          void UpdateBindStatus(DZMembership member, string appToken, string appName);

          DeviceBind getDevBindByUUID(Guid uuid);
          DeviceBind getDevBindByUserID(DZMembership user);
    }
}
