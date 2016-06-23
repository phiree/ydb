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
public class ResponseDRM001006 : BaseResponse
{
    public ResponseDRM001006(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataDRM001006 requestData = this.request.ReqData.ToObject<ReqDataDRM001006>();
        
        bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        BLLDZTag bllDZTag = Bootstrap.Container.Resolve<BLLDZTag>();

        string order_id = requestData.orderID;

        try
        {
            Guid orderID;
            bool isOrderId = Guid.TryParse(order_id, out orderID);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "orderId格式有误";
                return;
            }

            try
            {
                ServiceOrder order = bllServiceOrder.GetOne(orderID);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "订单为空!";
                    return;
                }

                IList<RespDataDRM_deliveryObj> arrayData = new List<RespDataDRM_deliveryObj>();

                if (order.Details.Count > 0)
                {
                    RespDataDRM_svcOBj svcObj;
                    IList<DZTag> tagList;
                    RespDataDRM_workTimeObj workTimeObj;
                    RespDataDRM_deliveryObj deliveryObj;
                    foreach (ServiceOrderDetail detail in order.Details)
                    {
                        tagList = bllDZTag.GetTagForService(detail.OriginalService.Id);
                        svcObj = new RespDataDRM_svcOBj().Adapt(detail.ServieSnapShot, detail.OriginalService, tagList);
                        workTimeObj = detail.OpenTimeSnapShot != null ? new RespDataDRM_workTimeObj().Adapt(detail.OpenTimeSnapShot) : null;

                        deliveryObj = new RespDataDRM_deliveryObj();
                        deliveryObj.deliveryID = detail.Id.ToString();
                        deliveryObj.orderID = order_id;
                        deliveryObj.startTime = order.OrderServerStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                        deliveryObj.endTime = order.OrderServerFinishedTime.ToString("yyyy-MM-dd HH:mm:ss");
                        deliveryObj.svcObj = svcObj;
                        deliveryObj.workTimeObj = workTimeObj;

                        arrayData.Add(deliveryObj);
                    }
                }

                RespDataDRM001006 respData = new RespDataDRM001006();
                respData.arrayData = arrayData;

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
 


