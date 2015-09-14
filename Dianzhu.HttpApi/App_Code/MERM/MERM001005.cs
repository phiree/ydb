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
public class ResponseMERM001005 : BaseResponse
{
    public ResponseMERM001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataMERM001005 requestData = this.request.ReqData.ToObject<ReqDataMERM001005>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
       

        try
        {

            DZMembership member = p.GetUserByEmail(requestData.email);
            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[8];
                this.err_Msg = "用户不存在,可能是传入的Email有误";
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
                
                //todo. 这个接口需要讨论.
                
                RespDataMERM respData = new RespDataMERM();
                RespDataMERM_merObj merObj = new RespDataMERM_merObj().Adapt(member);
                respData.merObj = merObj;
                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];

            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
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

public class ReqDataMERM001005
{
    public string email { get; set; }
    public string phone { get; set; }
    public string pWord { get; set; }

}
/*
public class RespDataMERM001005
{
    public RespDataMERM001005_merObj merObj { get; set; }
}
public class RespDataMERM001005_merObj
{
    public string merID { get; set; }
    public string alias { get; set; }
    public string area { get; set; }
    public string doc { get; set; }
    public string imgUrl { get; set; }
    public string linkMan { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string showImgUrls { get; set; }
    public RespDataMERM001005_merObj Adapt(Dianzhu.Model.Business business)
    {
        this.alias = business.Name;
        this.merID = business.Id.ToString();
        this.area = business.AreaBelongTo==null?string.Empty:business.AreaBelongTo.Name;
        this.doc = business.Description;
        this.imgUrl = business.BusinessAvatar.ImageName;
        this.linkMan = business.Contact;
        this.address = business.Address;
        this.phone = business.Phone;

        this.showImgUrls = string.Empty;
        foreach (BusinessImage img in business.BusinessShows)
        {
            showImgUrls += img.ImageName + ",";
        }
        this.showImgUrls.TrimEnd(',');

        return this;
    }
}
*/