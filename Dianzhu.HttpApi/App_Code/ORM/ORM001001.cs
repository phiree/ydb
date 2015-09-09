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
public class ResponseORM001001 : BaseResponse
{
    public ResponseORM001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM001001 requestData = this.request.ReqData.ToObject<ReqDataORM001001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        string raw_id = requestData.userID;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[8];
                this.err_Msg = "用户不存在,可能是传入的userID有误";
                return;
            }
            //验证用户的密码
            if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.pWord, "MD5"))
            {
                this.state_CODE = Dicts.StateCode[9];
                this.err_Msg = "用户密码错误";
                return;
            }
            try
            {
                string srvTarget = requestData.target;
                enum_OrderSearchType searchType = (enum_OrderSearchType)Enum.Parse(typeof(enum_OrderSearchType), srvTarget);
               
                int rowCount = bllServiceOrder.GetServiceOrderCount(uid,searchType);
                RespDataORM001001 respData=new RespDataORM001001{ sum=rowCount.ToString()};
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

public class ReqDataORM001001
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string target { get; set; }
 
}
public class RespDataORM001001
{
    public string sum { get; set; }
}
 