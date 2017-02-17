using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.IDAL
{
    public interface IDALDeviceBind  :IRepository<DeviceBind,Guid>
    {

          void UpdateBindStatus(string memberId, string appToken, string appName);

        /// <summary>
        /// 解除之前所有 apptoken  和 member的绑定,然后保存新的绑定
        /// </summary>
        /// <param name="devicebind"></param>
        void UpdateAndSave(DeviceBind devicebind);

        DeviceBind getDevBindByUUID(Guid uuid);
        DeviceBind getDevBindByUserID(Guid userId);
    }
}
