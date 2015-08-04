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
/// 获取一条服务信息的详情
/// </summary>
public class ResponseSVM001003 : BaseResponse
{
    public ResponseSVM001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVM001003 requestData = this.request.ReqData.ToObject<ReqDataSVM001003>();

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
                string srvID =PHSuit.StringHelper.InsertToId(requestData.srvID);
                
                 ServiceOrder order = bllServiceOrder.GetOne(new Guid(srvID));

                 if (order == null)
                 {
                     this.state_CODE = Dicts.StateCode[4];
                     this.err_Msg ="没有对应的服务,请检查传入的srvID";
                     return;
                 }
                RespDataSVM001003 respData = new RespDataSVM001003();


                RespDataSVM001003_Order order_adapted = new RespDataSVM001003_Order().Adap(order);
                respData.srvObj = order_adapted;

                this.RespData =  respData ;
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

public class ReqDataSVM001003
{
    public string uid { get; set; }
    public string userPWord { get; set; }
    public string srvID { get; set; }
    

}
public class RespDataSVM001003
{
    public  RespDataSVM001003_Order srvObj { get; set; }
}
public class RespDataSVM001003_Order
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
    public RespDataSVM001003_Order Adap(ServiceOrder order)
    {
        this.srvID = order.Service.Id.ToString().Replace("-", string.Empty);
        this.srvBiz = order.Service.Business.Name;
        this.srvBizID = order.Service.Business.Id.ToString().Replace("-", string.Empty);
        this.srvType = order.Service.ServiceType.ToString();
        this.srvStartTime = order.OrderCreated.ToString("yyyyMMddhhmm");
        this.srvEndTime = order.OrderFinished.ToString("yyyyMMddhhmm");
        ///这个是服务单价
        this.srvMoney = order.TotalPrice.ToString("#.#");
        this.srvStatus = order.OrderStatus.ToString();
        this.srvAdress = order.TargetAddress;
        this.srvExdes = order.Service.Description;
        return this;
    }
}


