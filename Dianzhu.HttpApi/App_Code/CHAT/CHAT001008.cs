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
    Dianzhu.IDAL.IDALIMUserStatus dalIMStatus = Bootstrap.Container.Resolve<Dianzhu.IDAL.IDALIMUserStatus>();
    BLLPush bllPush = Bootstrap.Container.Resolve<BLLPush>();
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
            if (chat.FromResource != enum_XmppResource.YDBan_DianDian)
            {
                dalChat.Add(chat);
                //离线推送
               
                bllPush.Push(chat, new Guid( chat.ToId), chat.SessionId);
            }
            else
            {
                Log.Info("点点自动发出的消息不保存");
            }
            this.state_CODE = Dicts.StateCode[0];
            this.RespData = "true";
        }
        catch (Exception ex)
        {
            PHSuit.ExceptionLoger.ExceptionLog(Log, ex);
        }



    }

}