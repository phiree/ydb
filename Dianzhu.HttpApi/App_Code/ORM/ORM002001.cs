using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;

using Dianzhu.Api.Model;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.Application.Dto;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.Common;

/// <summary>
/// 获取用户的服务订单列表
/// </summary>

public class ResponseORM002001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Ydb.HttpApi");
    public ResponseORM002001(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataORM002001 requestData = this.request.ReqData.ToObject<ReqDataORM002001>();
        
        IReceptionService receptionService = Bootstrap.Container.Resolve<IReceptionService>();

        IServiceOrderService bllServiceOrder = Bootstrap.Container.Resolve<IServiceOrderService>();
        Ydb.Common.Infrastructure.ISerialNoBuilder serialNoBuilder = Bootstrap.Container.Resolve<Ydb.Common.Infrastructure.ISerialNoBuilder>();
        IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

        string user_id = requestData.userID;

        Guid userId;
        bool isUser = Guid.TryParse(user_id, out userId);
        if (!isUser)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "id有误";
            return;
        }

        try
        {
            MemberDto member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberService).ValidateUser(userId, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = memberService.GetUserById(userId.ToString());
            }
            try
            {
                if(member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该用户不存在";
                    return;
                }

                ilog.Debug("1");
                RespDataORM002001 respData = new RespDataORM002001();               
                
                ilog.Debug("开始分配客服");
                string errorMessage = string.Empty;

               IList<string> onlineCsIdList= receptionService.GetOnlineUserList("ydb_customerservice");

               IList<MemberDto> onlineCsList = memberService.GetUsersByIdList(onlineCsIdList);

                ReceptionStatusDto rsDto = receptionService.AssignCustomerLogin(userId.ToString(),member.AreaId,
                    out errorMessage,onlineCsList.Select(x=>new MemberArea(x.Id.ToString(),x.AreaId)).ToList() );
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = errorMessage;
                    return;
                }

                MemberDto cs = memberService.GetUserById( rsDto.CustomerServiceId);


                //ilog.Debug("开始分配客服");
                ServiceOrder orderToReturn = null;//分配的订单
                //ReceptionStatus rs = bllReceptionStatus.GetOneByCustomer(userId);
                //Dictionary<MemberDto, MemberDto> assignedPair = new Dictionary<MemberDto, MemberDto>();
                //if (rs != null && rs.CustomerService.UserType == enum_UserType.customerservice)
                //{
                //    assignedPair.Add(rs.Customer, rs.CustomerService);

                //    orderToReturn = rs.Order;
                //}
                //else if (rs != null && rs.CustomerService.UserType == enum_UserType.diandian)
                //{
                //    bllReceptionStatus.Delete(rs);
                //    assignedPair = ra.AssignCustomerLogin(member);
                //}
                //else
                //{
                //    assignedPair = ra.AssignCustomerLogin(member);
                //}

                //if (assignedPair.Count == 0)
                //{
                //    this.state_CODE = Dicts.StateCode[4];
                //    this.err_Msg = "没有在线客服";
                //    return;
                //}
                //ilog.Debug("4");
                //if (assignedPair.Count > 1)
                //{
                //    this.state_CODE = Dicts.StateCode[4];
                //    this.err_Msg = "返回了多个客服";
                //    return;
                //}
                ilog.Debug("5");
                Guid order_ID;
                if (Guid.TryParse(rsDto.OrderId, out order_ID))
                {
                    orderToReturn = bllServiceOrder.GetOne(order_ID);
                }

                ilog.Debug("6");
                if (orderToReturn == null)
                {
                    orderToReturn = bllServiceOrder.GetDraftOrder(member.Id.ToString(), cs.Id.ToString());
                }

                ilog.Debug("7");
                if (orderToReturn == null)
                {
                    orderToReturn = new ServiceOrder()
                    {
                        CustomerId = member.Id.ToString(),
                        CustomerServiceId = cs.Id.ToString()
                    };

                    bllServiceOrder.Save(orderToReturn);
                }
                
                // if (!hasOrder||needNewOrder)
                // {
                //     /* string serviceName,string serviceBusinessName,string serviceDescription,decimal serviceUnitPrice,string serviceUrl,
                //MemberDto member,
                //string targetAddress, int unitAmount, decimal orderAmount*/
                //       orderToReturn = ServiceOrder.Create(
                //          enum_ServiceScopeType.OSIM
                //        , string.Empty //serviceName
                //        , string.Empty//serviceBusinessName
                //        , string.Empty//serviceDescription
                //        , 0//serviceUnitPrice
                //        , string.Empty//serviceUrl
                //        , member //member
                //        , string.Empty
                //        , 0
                //        , 0);
                //     orderToReturn.CustomerService = assignedPair[member];
                //     bllServiceOrder.SaveOrUpdate(orderToReturn);
                // }
                respData.orderID = orderToReturn.Id.ToString();
                ilog.Debug("8");
                //更新 ReceptionStatus 中订单
                receptionService.UpdateOrderId(rsDto.Id, respData.orderID);

                ilog.Debug("9");
                RespDataORM002001_cerObj cerObj = new RespDataORM002001_cerObj().Adap(cs);
                ilog.Debug("10");
                respData.cerObj = cerObj;
                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];

            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }

    }

}

 


