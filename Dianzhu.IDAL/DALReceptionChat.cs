using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;

using System.Collections;
using Dianzhu.Model.Enums;

namespace Dianzhu.IDAL
{
    public interface IDALReceptionChat : IRepository<ReceptionChat, Guid>
    {
        /// <summary>
        /// 根据订单获取聊天记录列表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        IList<ReceptionChat> GetChatByOrder(ServiceOrder order);

        /// <summary>
        /// 查询是否有该订单的聊天记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        IList<ReceptionChat> FindChatByOrder(ServiceOrder order);

        IList<ReceptionChat> GetReceptionChatList(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd,
                    int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount
                    );

        IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(Guid fromId, Guid toId, Guid orderId, DateTime timeBegin, DateTime timeEnd,
         int pageSize, ReceptionChat targetChat, string low, enum_ChatTarget target);
    }
}
