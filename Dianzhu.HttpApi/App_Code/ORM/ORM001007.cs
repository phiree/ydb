using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseORM001007 : BaseResponse
{
    public ResponseORM001007(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataORM001007 requestData = this.request.ReqData.ToObject<ReqDataORM001007>();

      IBLLServiceOrder  bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
        
        PushService pushService = Bootstrap.Container.Resolve<PushService>();
        BLLDZTag bllDZTag = Bootstrap.Container.Resolve<BLLDZTag>();

        string raw_id = requestData.userID;
        string order_id = requestData.orderID;

        try
        {
            Guid uid, orderID;
            bool isUserId = Guid.TryParse(raw_id, out uid);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(order_id, out orderID);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            MemberDto member = null;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberService).ValidateUser(uid, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = memberService.GetUserById(uid.ToString());
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该用户！";
                    return;
                }
            }
            try
            {
                string strPageSize = requestData.pageSize;
                string strPageNum = requestData.pageNum;//base on 1

                int pageSize, pageNum;
                if (!int.TryParse(strPageSize, out pageSize) || !int.TryParse(strPageNum, out pageNum))
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "分页大小或者分页索引不是数值格式";
                    return;
                }

                ServiceOrder order = bllServiceOrder.GetOne(orderID);

                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单不存在！";
                    return;
                }

                Dictionary<DZService, IList<DZTag>> dic = new Dictionary<DZService, IList<DZTag>>();

                IList<ServiceOrderPushedService> pushOrderList = pushService.GetPushedServicesForOrder(order);
                IList<DZTag> tagList = new List<DZTag>();
                foreach(ServiceOrderPushedService push in pushOrderList)
                {
                    tagList = bllDZTag.GetTagForService(push.OriginalService.Id);
                    dic.Add(push.OriginalService, tagList);
                }
                RespDataORM001007 respData = new RespDataORM001007().AdaptList(dic);

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



