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
using Dianzhu.Push;
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
                PushType  pushType;
                //发送给用户的 : 用证书1, 发送给商户的 用证书2
                
                switch (chat.To.UserType)
                {
                    case  enum_UserType.customer:pushType = PushType.PushToUser;
                        Log.Debug("推送给用户");
                        break;
                    case  enum_UserType.business:pushType = PushType.PushToBusiness;
                        Log.Debug("推送给商家");
                        break;
                    default:
                        string errMsg = "尚未处理的推送类型" + chat.To.UserType;
                        Log.Error(errMsg);
                        this.state_CODE = Dicts.StateCode[1];
                        return;
                }
                bllPush.Push(pushType, chat.To.Id, chat.ServiceOrder.Id.ToString());
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

        }



    }

}