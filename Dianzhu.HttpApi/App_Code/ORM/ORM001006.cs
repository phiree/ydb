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
public class ResponseORM001006 : BaseResponse
{
    public ResponseORM001006(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataORM001006 requestData = this.request.ReqData.ToObject<ReqDataORM001006>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
          PushService bllPushService = new PushService();
        BLLDZService bllDZService = new BLLDZService();
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis = new BLLServiceOrderStateChangeHis();
        string user_id = requestData.userID;

        try
        {
            Guid userId;
            bool isUserId = Guid.TryParse(user_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            DZMembership member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(userId, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = p.GetUserById(userId);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[8];
                    this.err_Msg = "用户不存在,可能是传入的uid有误";
                    return;
                }
            }
            try
            {
                string srvTarget = requestData.target;
                string strPageSize = requestData.pageSize;
                string strPageNum = requestData.pageNum;//base on 1
                int pageSize, pageNum;
                if (!int.TryParse(strPageSize, out pageSize) || !int.TryParse(strPageNum, out pageNum))
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "分页大小或者分页索引不是数值格式";
                    return;
                }

                enum_OrderSearchType searchType;
                bool isSearchType = Enum.TryParse(srvTarget, out searchType);
                if (!isSearchType)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "订单状态格式有误";
                    return;
                }

                IList<ServiceOrder> orderList = bllServiceOrder.GetServiceOrderList(userId, searchType, pageNum, pageSize);
                Dictionary<ServiceOrder, ServiceOrderPushedService> dicList = new Dictionary<ServiceOrder, ServiceOrderPushedService>();
                IList<ServiceOrderPushedService> pushServiceList = new List<ServiceOrderPushedService>();
                foreach(ServiceOrder order in orderList)
                {
                    pushServiceList = bllPushService.GetPushedServicesForOrder(order);
                    if (pushServiceList.Count > 0)
                    {
                        dicList.Add(order, pushServiceList[0]);
                    }
                    else
                    {
                        dicList.Add(order, null);
                    }
                }

                RespDataORM001006 respData = new RespDataORM001006();         
                respData.AdapList(dicList);

                ServiceOrderStateChangeHis orderHis;
                IList<DZTag> tagsList = new List<DZTag>();//标签
                foreach (KeyValuePair<ServiceOrder, ServiceOrderPushedService> item in dicList)
                {
                    orderHis = bllServiceOrderStateChangeHis.GetOrderHis(item.Key);

                    if (item.Key.Details.Count > 0)
                    {
                        tagsList = bllDZService.GetServiceTags(item.Key.Details[0].OriginalService);
                    }
                    else
                    {
                        tagsList = bllDZService.GetServiceTags(item.Value.OriginalService);
                    }

                    foreach (var obj in respData.arrayData)
                    {
                        if (obj.orderID == item.Key.Id.ToString())
                        {
                            obj.svcObj.SetTag(obj.svcObj, tagsList);
                            obj.SetOrderStatusObj(obj, orderHis);
                            break;
                        }
                    }
                }

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
 


