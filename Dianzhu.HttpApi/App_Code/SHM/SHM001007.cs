using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using Ydb.Common.Enums;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
using Ydb.BusinessResource.Application;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.BusinessResource.DomainModel;
using Ydb.Membership.Application;
using Ydb.Common;
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseSHM001007 : BaseResponse
{
    public ResponseSHM001007(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataSHM001007 requestData = this.request.ReqData.ToObject<ReqDataSHM001007>();

        IServiceOrderService bllServiceOrder = Bootstrap.Container.Resolve<IServiceOrderService>();
        //todo:用户验证的复用
        IDZTagService bllDZTag = Bootstrap.Container.Resolve<IDZTagService>();
        IDZServiceService bllService = Bootstrap.Container.Resolve<IDZServiceService>();
        IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

        string start_Time = requestData.startTime;
        string end_Time = requestData.endTime;
        string service_id = requestData.svcID;

        try
        {
            DateTime startTime, endTime;
            if (!DateTime.TryParse(start_Time, out startTime))
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "startTime不是正确的日期格式!";
                return;
            }

            if (!DateTime.TryParse(end_Time, out endTime))
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "endTime不是正确的日期格式!";
                return;
            }

            if (startTime > endTime)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "startTime不得小于endTime!";
                return;
            }




           

            DZService dzService = bllService.GetOne2(new Guid(service_id));

            IList<ServiceOrder> orderList = bllServiceOrder.GetOrders(new Ydb.Common.Specification.TraitFilter(), "all"
                , string.Empty, Guid.Empty, null, startTime, endTime,Guid.Empty,string.Empty,string.Empty) //bllServiceOrder.GetOrderListByDateRange( startTime, endTime)

                    .Where(x => x.Details[0].OriginalServiceId.ToString() == service_id).ToList();
          //  RespDataSHM001007 respData = new RespDataSHM001007();
          
            this.RespData = new RespDataSHM_snapshots(memberService,bllService).Adap(orderList);
            this.state_CODE = Dicts.StateCode[0];

        }



        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            PHSuit.ExceptionLoger.ExceptionLog(Log, e);
            return;
        }

    }

}



