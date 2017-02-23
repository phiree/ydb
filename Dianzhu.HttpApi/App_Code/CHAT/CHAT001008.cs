using Dianzhu.Api.Model;
using System;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.Membership.Application;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.Push.Application;

/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseCHAT001008 : BaseResponse
{
    private IChatService chatService = Bootstrap.Container.Resolve<IChatService>();
    private log4net.ILog log;
    private IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    private IMessageAdapter messageAdapter = Bootstrap.Container.Resolve<IMessageAdapter>();
    private IServiceOrderService orderService = Bootstrap.Container.Resolve<IServiceOrderService>();
    private IPushService pushService = Bootstrap.Container.Resolve<IPushService>();

    public ResponseCHAT001008(BaseRequest request) : base(request)
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
            ReqDataCHAT001008 requestData = this.request.ReqData.ToObject<ReqDataCHAT001008>();

            ReceptionChat chat = messageAdapter.RawXmlToChat(requestData.message);
            IReceptionSession receptionSession = Bootstrap.Container.Resolve<IReceptionSession>();
            if (chat.FromResource != XmppResource.YDBan_DianDian)
            {
                chatService.Add(chat);
                //离线推送
                if (chat.ToResource != XmppResource.YDBan_CustomerService)
                {
                    //是否在线
                    var isOnline = receptionSession.IsUserOnline(chat.ToId);// dalIMStatus.FindOne(x => x.UserID.ToString() == chat.ToId);
                    if (isOnline)
                    {
                        log.Debug("用户在线,不推送");
                        return;
                    }
                    chatService.SetChatUnread(chat.Id.ToString());
                    var fromUser = memberService.GetUserById(chat.FromId);
                    //todo: 需要订单领域的配合.
                    ServiceOrder order = orderService.GetOne(new Guid(chat.SessionId));

                    pushService.Push(chat.MessageBody, chat.GetType().ToString(), chat.ToId,
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