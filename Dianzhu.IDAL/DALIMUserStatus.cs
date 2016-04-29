using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.DAL
{
    public interface IDALIMUserStatus  
    {



          IMUserStatus GetIMUSByUserId(Guid userId);
 
          IList<IMUserStatus> GetOnlineListByClientName(string name);

    }
}
