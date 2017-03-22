using System;
using System.Collections.Generic;
using Ydb.InstantMessage.Application.Dto;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.Application
{
    public interface IReceptionService
    {
        IList<ReceptionStatusDto> AssignCSLogin(string csId, string areaId, int amount);
        void AssignCSLogoff(string csId, IList<MemberArea> onlineCsList);
        ReceptionStatusDto AssignCustomerLogin(string customerId, string areaId, out string errorMessage, IList<MemberArea> onlineCsList);
        void DeleteReception(string customerId);
        IList<string> GetOnlineUserList(string resouceName);
        void SendCSLoginMessageToDD();
        void SendCSLogoffMessageToDD();
        void UpdateOrderId(Guid Id, string newOrderId);
        void UpdateOrderId(string customerId, string csId, string newOrderId);
    }
}