using System;
using System.Collections.Generic;
using System.Linq;
using Dianzhu.Model;
 
using Dianzhu.Model.Enums;

namespace Dianzhu.DAL
{
    public interface IDALReception  

    {

            IList<ReceptionChat> GetReceptionChatList(DZMembership from, DZMembership to, Guid orderId, DateTime timeBegin, DateTime timeEnd,
            int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount
            );

            IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(DZMembership from, DZMembership to, Guid orderId, DateTime timeBegin, DateTime timeEnd,
             int pageSize, ReceptionChat targetChat, string low, enum_ChatTarget target);

 
      
    }
}
