using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common;
using Dianzhu.Api.Model;
using agsXMPP.protocol.client;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.Push.Application;

using Ydb.Push;
using Ydb.Order.DomainModel;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseCHAT001008:BaseResponse
{
    log4net.ILog log = log4net.LogManager.GetLogger("Ydb.ResponseChat001008");
    IMessageAdapter messageAdapter = Bootstrap.Container.Resolve<IMessageAdapter>();
    IChatService chatService = Bootstrap.Container.Resolve<IChatService>();
    IPushService pushService = Bootstrap.Container.Resolve<IPushService>();
    IDZMembershipService memberService= Bootstrap.Container.Resolve<IDZMembershipService>();
    Ydb.Order.Application.IServiceOrderService orderService = Bootstrap.Container.Resolve<Ydb.Order.Application.IServiceOrderService>();
    public ResponseCHAT001008(BaseRequest request):base(request)
    {
        log = log4net.LogManager.GetLogger(this.GetType().ToString());
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        try
        {
            log.Debug("a");
            ReqDataCHAT001008 requestData = this.request.ReqData.ToObject<ReqDataCHAT001008>();
           
            ReceptionChat chat = messageAdapter.RawXmlToChat(requestData.message);
            log.Debug("b");
            IReceptionSession receptionSession = Bootstrap.Container.Resolve<IReceptionSession>();
            if (chat.FromResource != XmppResource.YDBan_DianDian)
            {
                log.Debug("c");
                chatService.Add(chat);
                //离线推送
                log.Debug("d");
                if (chat.ToResource != XmppResource.YDBan_CustomerService)
                {
                    //是否在线
                    var isOnline = receptionSession.IsUserOnline(chat.ToId);// dalIMStatus.FindOne(x => x.UserID.ToString() == chat.ToId);
                    if (isOnline)
                    {
                        log.Debug("用户在线,不推送");
                        return;
                    }
                    log.Debug("1");
                    chatService.SetChatUnread(chat.Id.ToString());
                    log.Debug("2");
                    var fromUser= memberService.GetUserById(chat.FromId);
                    log.Debug("3");
                    //todo: 需要订单领域的配合.
                    ServiceOrder order = orderService.GetOne(new Guid(chat.SessionId));
                    log.Debug("4");
                    pushService.Push(chat.MessageBody, chat.GetType().Name, chat.ToId,
                        chat.FromResource.ToString(), fromUser.UserName,
                        chat.SessionId, order.SerialNo, order.OrderStatus.ToString(), order.OrderStatusStr,
                        order.ServiceBusinessName, chat.ToResource.ToString());


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