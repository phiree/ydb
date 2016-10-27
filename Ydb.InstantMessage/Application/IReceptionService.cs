using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.Application.Dto;

namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 分配关系
    /// </summary>
    public interface IReceptionService
    {
        /// <summary>
        /// 通过id更新orderId
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="csId"></param>
        void UpdateOrderId(Guid Id, string newOrderId);

        /// <summary>
        /// 通过客户id与客服id更新orderId
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="csId"></param>
        void UpdateOrderId(string customerId, string csId, string newOrderId);

        /// <summary>
        /// 用户超时未回复，删除分配关系，并给该用户发送客服离线通知
        /// </summary>
        /// <param name="customerId"></param>
        void DeleteReception(string customerId);

        ReceptionStatusDto AssignCustomerLogin(string customerId,out string errorMessage);

        /// <summary>
        /// 客服上线：给点点发通知；拉取分配给点点的用户，如果有用户，给拉取到的用户发送重新分配客服的通知
        /// </summary>
        /// <param name="csId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        IList<ReceptionStatusDto> AssignCSLogin(string csId, int amount);

        /// <summary>
        /// 客服下线：给点点发通知；将分配给客服的用户重新分配给其他客服，如果没有客服，分配给点点
        /// </summary>
        /// <param name="csId"></param>
        void AssignCSLogoff(string csId);
    }
}
