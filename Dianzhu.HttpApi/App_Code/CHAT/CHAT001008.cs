using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
using agsXMPP.protocol.client;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.InstantMessage.Application;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseCHAT001008:BaseResponse
{
    IMessageAdapter messageAdapter = Bootstrap.Container.Resolve<IMessageAdapter>();
    IChatService chatService = Bootstrap.Container.Resolve<IChatService>();

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
            if (chat.FromResource != XmppResource.YDBan_DianDian)
            {
                chatService.Add(chat);
                //离线推送
                if(chat.ToResource != XmppResource.YDBan_CustomerService)
                {
                    bllPush.Push(chat, new Guid(chat.ToId), chat.SessionId);
                }
               
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