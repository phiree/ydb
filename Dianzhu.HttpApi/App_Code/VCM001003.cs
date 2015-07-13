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
public class ResponseVCM001003 : BaseResponse
{
    public ResponseVCM001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataVCM001003 requestData = this.request.ReqData.ToObject<ReqDataVCM001003>();

       
        BLLCashTicket bLLCashTicket = new BLLCashTicket();
        string raw_id = requestData.vcsID;

      
            try
            {
                string vcsID =PHSuit.StringHelper.InsertToId(requestData.vcsID);

                CashTicket ticket = bLLCashTicket.GetOne(new Guid(vcsID));

                if (ticket == null)
                 {
                     this.state_CODE = Dicts.StateCode[4];
                     this.err_Msg ="没有对应的现金券,请检查传入的vcsID";
                     return;
                 }
                RespDataVCM001003 respData = new RespDataVCM001003();


                RespDataVCM001002_Cashticket cash_adapted = new RespDataVCM001002_Cashticket().Adap(ticket);
                respData.vcsObj = cash_adapted;

                this.RespData = JsonConvert.SerializeObject(respData);
                this.state_CODE = Dicts.StateCode[0];

            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

        }
        

    }
    


public class ReqDataVCM001003
{
   
    public string vcsID { get; set; }
    
}
public class RespDataVCM001003
{
    public  RespDataVCM001002_Cashticket vcsObj { get; set; }
}
 


