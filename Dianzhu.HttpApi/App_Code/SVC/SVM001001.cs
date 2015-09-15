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
/// 服务信息总数获取.
/// </summary>
public class ResponseSVM001001 : BaseResponse
{
    public ResponseSVM001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVM001001 requestData = this.request.ReqData.ToObject<ReqDataSVM001001>();

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
                enum_OrderSearchType searchType = (enum_OrderSearchType)Enum.Parse(typeof(enum_OrderSearchType), srvTarget);
               
                int rowCount = bllServiceOrder.GetServiceOrderCount(uid,searchType);
                RespDataSVM001001 respData=new RespDataSVM001001{ sum=rowCount.ToString()};
                this.RespData =  respData ;
                this.state_CODE = Dicts.StateCode[0];
                
            }
            catch (Exception ex)
            { 
                this.state_CODE=Dicts.StateCode[2];
                this.err_Msg = ex.Message;
            }
           
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;

        }

    }
    public override string BuildJsonResponse()
    {

        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}

public class ReqDataSVM001001
{
    public string uid { get; set; }
    public string userPWord { get; set; }
    public string srvTarget { get; set; }
 
}
public class RespDataSVM001001
{
    public string sum { get; set; }
}
 