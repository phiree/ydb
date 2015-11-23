using System;
using Dianzhu.Model;
using Dianzhu.BLL;

/// <summary>
/// 上传选择城市
/// </summary>
public class ResponseLCT001007 : BaseResponse
{
    public ResponseLCT001007(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataLCT001007 requestData = this.request.ReqData.ToObject<ReqDataLCT001007>();

        DZMembershipProvider p = new DZMembershipProvider();
        string raw_id = requestData.userID;

        try
        {
            DZMembership member;
            bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }

            try
            {


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

    public class ReqDataLCT001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string code { get; set; }
    }

}