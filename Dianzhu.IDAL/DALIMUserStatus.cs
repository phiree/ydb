using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.IDAL
{
    public interface IDALIMUserStatus :IRepository<IMUserStatus,Guid>
    {
          IMUserStatus GetIMUSByUserId(Guid userId);
 
          IList<IMUserStatus> GetOnlineListByClientName(string name);

    }
}
