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
/// 获取用户的服务订单列表
/// </summary>
public class ResponseSHM001007 : BaseResponse
{
    public ResponseSHM001007(BaseRequest request) : base(request) { }
    
    protected override void BuildRespData()
    {
        ReqDataSHM001007 requestData = this.request.ReqData.ToObject<ReqDataSHM001007>();

        IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        //todo:用户验证的复用
        BLLDZTag bllDZTag = Bootstrap.Container.Resolve<BLLDZTag>();
        BLLDZService bllService = Bootstrap.Container.Resolve<BLLDZService>();

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




           

            DZService dzService = bllService.GetOne(new Guid(service_id));

            IList<ServiceOrder> orderList = bllServiceOrder.GetOrderListOfServiceByDateRange(new Guid(service_id), startTime, endTime);
             
          //  RespDataSHM001007 respData = new RespDataSHM001007();
          
            this.RespData = new RespDataSHM_snapshots().Adap(orderList);
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



