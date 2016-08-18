using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
using Dianzhu.CSClient.IMessageAdapter;
using agsXMPP.protocol.client;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseCHAT001008:BaseResponse
{
    Dianzhu.CSClient.IMessageAdapter.IAdapter messageAdapter = Bootstrap.Container.Resolve<Dianzhu.CSClient.IMessageAdapter.IAdapter>();
    Dianzhu.IDAL.IDALReceptionChat dalChat = Bootstrap.Container.Resolve<Dianzhu.IDAL.IDALReceptionChat>();

    public ResponseCHAT001008(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        try
        {
            ReqDataCHAT001008 requestData = this.request.ReqData.ToObject<ReqDataCHAT001008>();

            ReceptionChat chat = messageAdapter.RawXmlToChat(requestData.message);
            dalChat.Add(chat);
            this.state_CODE = Dicts.StateCode[0];
            this.RespData = "true";
        }
        catch (Exception ex)
        {

        }



    }

}