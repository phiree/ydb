using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
using Dianzhu.BLL;

namespace Dianzhu.CSClient.Presenter
{
    public class PShelfService
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PShelfService");
        IInstantMessage.InstantMessage iIM;
        IViewSearch viewSearch;
        IViewShelfService viewShelfService;
        IViewChatList viewChatList;
        IViewIdentityList viewIdentityList;
        PushService bllPushService;
        BLLReceptionChat bllReceptionChat;
        IBLLServiceOrder bllServiceOrder;
        BLLReceptionStatus bllReceptionStatus;

        public PShelfService(IInstantMessage.InstantMessage iIM,IViewSearch viewSearch, IViewShelfService viewShelfService, IViewChatList viewChatList, IViewIdentityList viewIdentityList, IBLLServiceOrder bllServiceOrder) 
            : this(iIM, viewSearch, viewShelfService, viewChatList, viewIdentityList, bllServiceOrder, new PushService(),new BLLReceptionChat(),new BLLReceptionStatus())
        { }

        public PShelfService(IInstantMessage.InstantMessage iIM,IViewSearch viewSearch, IViewShelfService viewShelfService, IViewChatList viewChatList, IViewIdentityList viewIdentityList, IBLLServiceOrder bllServiceOrder, PushService bllPushService, BLLReceptionChat bllReceptionChat,  BLLReceptionStatus bllReceptionStatus)
        {
            this.iIM = iIM;
            this.viewSearch = viewSearch;
            this.viewShelfService = viewShelfService;
            this.viewChatList = viewChatList;
            this.viewIdentityList = viewIdentityList;
            this.bllPushService = bllPushService;
            this.bllReceptionChat = bllReceptionChat;
            this.bllServiceOrder = bllServiceOrder;
            this.bllReceptionStatus = bllReceptionStatus;

            viewShelfService.PushShelfService += ViewShelfService_PushShelfService;
        }

        private void ViewShelfService_PushShelfService(DZService pushedService)
        {
            if (pushedService == null)
            {
                return;
            }
            if (IdentityManager.CurrentIdentity == null)
            {
                return;
            }

            //禁用推送按钮
            //viewSearchResult.BtnPush = false;

            ServiceOrderPushedService serviceOrderPushedService = new ServiceOrderPushedService(IdentityManager.CurrentIdentity, pushedService, viewSearch.UnitAmount, viewSearch.ServiceAddress, viewSearch.SearchKeywordTime);
            bllPushService.Push(IdentityManager.CurrentIdentity, serviceOrderPushedService, viewSearch.ServiceAddress, viewSearch.SearchKeywordTime);

            //iim发送消息
            ReceptionChat chat = new ReceptionChatPushService
            {
                ServiceOrder = IdentityManager.CurrentIdentity,
                ChatTarget = Model.Enums.enum_ChatTarget.cer,
                From = GlobalViables.CurrentCustomerService,
                To = IdentityManager.CurrentIdentity.Customer,
                MessageBody = "推送的服务",
                PushedService = serviceOrderPushedService,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,

            };
            bllReceptionChat.Save(chat);
            iIM.SendMessage(chat);
            log.Debug("推送的订单：" + IdentityManager.CurrentIdentity.Id.ToString());

            //助理工具显示发送的消息
            viewChatList.AddOneChat(chat);

            //生成新的草稿单并发送给客户端
            ServiceOrder newOrder = ServiceOrderFactory.CreateDraft(GlobalViables.CurrentCustomerService, IdentityManager.CurrentIdentity.Customer);
            bllServiceOrder.Save(newOrder);
            log.Debug("新草稿订单的id：" + newOrder.Id.ToString());
            string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
            string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{0}"" from = ""{1}"">
                                                    <active xmlns = ""http://jabber.org/protocol/chatstates""></active><ext xmlns=""ihelper:notice:draft:new""><orderID>{3}</orderID></ext></message>",
                                                    IdentityManager.CurrentIdentity.Customer.Id + "@" + server, IdentityManager.CurrentIdentity.CustomerService.Id, Guid.NewGuid() + "@" + server, newOrder.Id);
            iIM.SendMessage(noticeDraftNew);

            //获取之前orderid
            Guid oldOrderId = IdentityManager.CurrentIdentity.Id;

            //更新当前订单
            IdentityTypeOfOrder type;
            log.Debug("更新当前订单");
            IdentityManager.UpdateIdentityList(newOrder, out type);
            log.Debug("当前订单的id：" + IdentityManager.CurrentIdentity.Id.ToString());

            //更新view
            viewIdentityList.UpdateIdentityBtnName(oldOrderId, IdentityManager.CurrentIdentity.Id);

            //更新接待分配表
            bllReceptionStatus.UpdateOrder(IdentityManager.CurrentIdentity.Customer, GlobalViables.CurrentCustomerService, newOrder);

            //清空搜索选项 todo:为了测试方便，先注释掉
            //viewSearch.ClearData();
        }
    }
}
