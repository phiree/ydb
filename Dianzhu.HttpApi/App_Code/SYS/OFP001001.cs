using System;
using Dianzhu.Model;
using Dianzhu.BLL;

/// <summary>
/// 实时汇报用户的状态
/// </summary>
public class ResponseOFP001001 : BaseResponse
{
    public ResponseOFP001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataOFP001001 requestData = this.request.ReqData.ToObject<ReqDataOFP001001>();

        try
        {
            if (requestData.jid != "")
            {
                string uid = requestData.jid.Split('@')[0];
            }
            else
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "JId为空";
                return;
            }

            //bool uuidisGuid = Guid.TryParse(requestData.AppObj.appUUID, out uuId);
            //if (!uuidisGuid)
            //{
            //    this.state_CODE = Dicts.StateCode[1];
            //    this.err_Msg = "UUId格式有误";
            //    return;
            //}

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

    public class ReqDataOFP001001
    {
        public string jid { get; set; }
        public string status { get; set; }
        public string ipaddress { get; set; }
    }

}