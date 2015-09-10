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
public class ResponseORM001002 : BaseResponse
{
    public ResponseORM001002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM001002 requestData = this.request.ReqData.ToObject<ReqDataORM001002>();

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
                RespDataORM001002 respData = new RespDataORM001002();


                RespDataORM001002_Order order_adapted = new RespDataORM001002_Order().Adap(order);
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

public class ReqDataORM001002
{
    public string uid { get; set; }
    public string userPWord { get; set; }
    public string srvID { get; set; }
    

}
public class RespDataORM001002
{
    public  RespDataORM001002_Order srvObj { get; set; }
}
public class RespDataORM001002_Order
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
    public RespDataORM001002_Order Adap(ServiceOrder order)
    {
        this.srvID = order.Service.Id.ToString();
        this.srvBiz = order.Service.Business.Name;
        this.srvBizID = order.Service.Business.Id.ToString();
        this.srvType = order.Service.ServiceType.ToString();
        this.srvStartTime = order.OrderCreated.ToString("yyyyMMddhhmm");
        this.srvEndTime = order.OrderFinished.ToString("yyyyMMddhhmm");
        ///这个是服务单价
        this.srvMoney = order.ServiceUnitPrice.ToString("#.#");
        this.srvStatus = order.OrderStatus.ToString();
        this.srvAdress = order.TargetAddress??string.Empty;
        this.srvExdes = order.Service.Description??string.Empty;
        return this;
    }
}


