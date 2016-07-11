using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
/// <summary>
/// 获取一条服务信息的详情
/// </summary>
public class ResponseORM001005 : BaseResponse
{
    public ResponseORM001005(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataORM001005 requestData = this.request.ReqData.ToObject<ReqDataORM001005>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
       
        PushService bllPushService =  Bootstrap.Container.Resolve<PushService>();
        BLLDZService bllDZService = Bootstrap.Container.Resolve<BLLDZService>();
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis = Bootstrap.Container.Resolve<BLLServiceOrderStateChangeHis>();
        string raw_id = requestData.userID;

        try
        {
            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            try
            {
                ServiceOrder order = bllServiceOrder.GetOne(new Guid(requestData.orderID));

                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的服务,请检查传入的orderID";
                    return;
                }
                IList<ServiceOrderPushedService> pushServiceList = bllPushService.GetPushedServicesForOrder(order);
                RespDataORM_Order respData = new RespDataORM_Order();
                IList<DZTag> tagsList = new List<DZTag>();//标签
                if (pushServiceList.Count > 0)
                {
                    if (order.Details.Count > 0)
                    {
                        tagsList = bllDZService.GetServiceTags(order.Details[0].OriginalService);
                    }
                    else
                    {
                        tagsList = bllDZService.GetServiceTags(pushServiceList[0].OriginalService);
                    }

                    respData.Adap(order, pushServiceList[0]);
                }
                else
                {
                    if (order.Details.Count > 0)
                    {
                        tagsList = bllDZService.GetServiceTags(order.Details[0].OriginalService);
                    }

                    respData.Adap(order, null);
                }

                respData.orderObj.svcObj.SetTag(respData.orderObj.svcObj, tagsList);

                ServiceOrderStateChangeHis orderHis = bllServiceOrderStateChangeHis.GetOrderHis(order);
                respData.orderObj.SetOrderStatusObj(respData.orderObj, orderHis);

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

