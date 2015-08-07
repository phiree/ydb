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
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseSVM001002 : BaseResponse
{
    public ResponseSVM001002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVM001002 requestData = this.request.ReqData.ToObject<ReqDataSVM001002>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        string raw_id = requestData.uid;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[8];
                this.err_Msg = "用户不存在,可能是传入的uid有误";
                return;
            }
            //验证用户的密码
            if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.userPWord, "MD5"))
            {
                this.state_CODE = Dicts.StateCode[9];
                this.err_Msg = "用户密码错误";
                return;
            }
            try
            {
                string srvTarget = requestData.srvTarget;
                string strPageSize = requestData.pageSize;
                string strPageNum = requestData.pageNum;//base on 1
                int pageSize, pageNum;
                if (!int.TryParse(strPageSize, out pageSize) ||
                 !int.TryParse(strPageNum, out pageNum))
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "分页大小或者分页索引不是数值格式";
                    return;
                }
                enum_OrderSearchType searchType = (enum_OrderSearchType)Enum.Parse(typeof(enum_OrderSearchType), srvTarget);

                IList<ServiceOrder> orderList = bllServiceOrder.GetServiceOrderList(uid, searchType, pageNum, pageSize);

                RespDataSVM001002 respData = new RespDataSVM001002();

                respData.AdapList(orderList);

                this.RespData = respData ;
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

public class ReqDataSVM001002
{
    public string uid { get; set; }
    public string userPWord { get; set; }
    public string srvTarget { get; set; }
    public string pageSize { get; set; }
    public string pageNum { get; set; }

}
public class RespDataSVM001002
{
    public IList<RespDataSVM001002_Order> arrayData { get; set; }
    public RespDataSVM001002()
    {

        arrayData = new List<RespDataSVM001002_Order>();
    }

    public void AdapList(IList<ServiceOrder> serviceOrderList)
    {
        foreach (ServiceOrder order in serviceOrderList)
        {
            RespDataSVM001002_Order adapted_order = new RespDataSVM001002_Order().Adap(order);
            arrayData.Add(adapted_order);
        }

    }
}
public class RespDataSVM001002_Order
{
    public string srvID { get; set; }
    public string srvBiz { get; set; }
    public string srvBizID { get; set; }
    public string srvType { get; set; }
    public string srvStartTime { get; set; }
    public string srvEndTime { get; set; }
    public string srvMoney { get; set; }
    public string srvStatus { get; set; }
    public string srvAdress { get; set; }
    public string srvExdes { get; set; }
    public RespDataSVM001002_Order Adap(ServiceOrder order)
    {
        this.srvID = order.Service.Id.ToString().Replace("-",string.Empty);
        this.srvBiz = order.Service.Business.Name;
        this.srvBizID = order.Service.Business.Id.ToString().Replace("-", string.Empty);
        this.srvType = order.Service.ServiceType.ToString();
        this.srvStartTime = order.Service.ServiceTimeBegin??string.Empty;
        this.srvEndTime = order.Service.ServiceTimeEnd??string.Empty;
        ///这个是服务单价
        this.srvMoney = order.TotalPrice.ToString("#.#");
        this.srvStatus = order.OrderStatus.ToString();
        this.srvAdress = order.TargetAddress??string.Empty;
        this.srvExdes = order.Service.Description??string.Empty;
        return this;
    }
}


