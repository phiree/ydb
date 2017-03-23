using System;
using System.Collections.Generic;
using Ydb.InstantMessage.Application.Dto;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.Application
{
    public interface IReceptionService
    {
        IList<ReceptionStatusDto> AssignCSLogin(string csId, string areaCode, int amount);
        void AssignCSLogoff(string csId, IList<MemberArea> onlineCsList);
        ReceptionStatusDto AssignCustomerLogin(string customerId, string areaCode, out string errorMessage, IList<MemberArea> onlineCsList);
        void DeleteReception(string customerId);
        IList<string> GetOnlineUserList(string resouceName);
        void SendCSLoginMessageToDD(string areaCode);
        void SendCSLogoffMessageToDD(string areaCode);
        void UpdateOrderId(Guid Id, string newOrderId);
        void UpdateOrderId(string customerId, string csId, string newOrderId);
        /// <summary>
        /// 用户更换地区之后,重新申请客服.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="onlineCsList"></param>
        void AssignCustomerChangeLocation(string customerId,string newAreaCode, IList<MemberArea> onlineCsList);

    }
}