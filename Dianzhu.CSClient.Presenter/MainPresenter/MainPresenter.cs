using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.CSClient.IVew;
using Dianzhu.CSClient.IInstantMessage;
namespace Dianzhu.CSClient.Presenter
{
    public partial class MainPresenter
    {
        IVew.IMainFormView view;
        InstantMessage instantMessage;
        DZMembershipProvider bllMember;
        BLLDZService bllService;
        BLLReception bllReception;
        BLLServiceOrder bllOrder;
        BLLReceptionStatus bllReceptionStatus;
       
        public MainPresenter(IVew.IMainFormView view,
            InstantMessage instantMessage,
            IMessageAdapter.IAdapter messageAdapter,
            DZMembershipProvider bllMember, BLLReception bllReception,
            BLLDZService bllService, BLLServiceOrder bllOrder,
            BLLReceptionStatus bllReceptionStatus)
        {
            this.view = view;
            this.instantMessage = instantMessage;
            this.bllMember = bllMember;
            this.bllReception = bllReception;
            this.bllService = bllService;
            this.bllReceptionStatus = bllReceptionStatus;

            this.bllOrder = bllOrder;

            //  IM的委托
            this.instantMessage.IMPresent += new IMPresent(IMPresent);
            this.instantMessage.IMReceivedMessage += new IMReceivedMessage(IMReceivedMessage);
            this.instantMessage.IMClosed += new IMClosed(instantMessage_IMClosed);
            //iview的委托
            this.view.SendMessageHandler += new SendMessageHandler(view_SendMessageHandler);

            this.view.BeforeCustomerChanged += new BeforeCustomerChanged(view_BeforeCustomerChanged);
            this.view.ActiveCustomerHandler += new IVew.ActiveCustomerHandler(ActiveCustomer);
            this.view.ActiveCustomerHandler += new IVew.ActiveCustomerHandler(LoadChatHistory);
            this.view.ActiveCustomerHandler += new IVew.ActiveCustomerHandler(LoadSearchResult);
            this.view.ActiveCustomerHandler += new ActiveCustomerHandler(LoadCurrentOrder);

            this.view.PushExternalService += new PushExternalService(view_PushExternalService);
            this.view.PushInternalService += new PushInternalService(view_PushInternalService);
            this.view.ButtonNamePrefix = System.Configuration.ConfigurationManager.AppSettings["ButtonNamePrefix"];
            this.view.SearchService += new IVew.SearchService(view_SearchService);
            this.view.SendPayLink += new IVew.SendPayLink(view_SendPayLink);
            this.view.CreateOrder += new CreateOrder(view_CreateOrder);
            this.view.ViewClosed += new ViewClosed(view_ViewClosed);
            
        }

        void instantMessage_IMClosed()
        {
           
        }

        void view_ViewClosed()
        {
            IList<ReceptionStatus> reassignList = bllReceptionStatus.CustomerServiceLogout(customerService);
            //将新分配的客服发送给客户端.
            foreach (ReceptionStatus rs in reassignList)
            {
                ReceptionChat rc = new ReceptionChatReAssign
                {
                    From = customerService,
                    ChatType = Model.Enums.enum_ChatType.ReAssign,
                    ReassignedCustomerService = rs.CustomerService,
                    To = rs.Customer,
                    SendTime = DateTime.Now
                };
                SendMessage(rc);
            }
            this.instantMessage.Close();    
        }
      
        /// <summary>
        /// 保存消息.
        /// 此方法不知道消息方向.
        /// </summary>
        /// <param name="message"></param>
        /// 
        private void SaveMessage(ReceptionChat chat, bool isSend)
        {
            #region 保存聊天消息

            DZMembership fromCustomer = isSend ? chat.To : chat.From;
            string customerName = fromCustomer.UserName;
            string message = chat.MessageBody;

            string mediaUrl = chat.MessageMediaUrl;
            bool isIn = ReceptionList.ContainsKey(customerName);

            ReceptionBase re;

            if (isIn)
            {
                re = ReceptionList[customerName];
            }
            else
            {

                re = new ReceptionCustomer
                {
                    Sender = fromCustomer,//未点击前customer还未被赋值.为空
                    Receiver = customerService,
                    TimeBegin = DateTime.Now,

                };
                ReceptionList.Add(customerName, re);
            }
            DateTime now = DateTime.Now;

            if (isSend)
            {
                chat.SendTime = now;

            }
            else
            {
                chat.ReceiveTime = now;

            }
            re.ChatHistory.Add(chat);
            bllReception.Save(re);
            #endregion
        }

        private void LoadSearchResult(string customerName)
        {
            if (SearchResultForCustomer.ContainsKey(customerName))
            {
                view.SearchedService = SearchResultForCustomer[customerName];
            }
        }
        private void LoadChatHistory()
        {
            LoadChatHistory(customer.UserName);
        }
        private void LoadChatHistory(string customerName)
        {
            bool isContain = ReceptionList.ContainsKey(customerName);

            var chatHistory = bllReception.GetHistoryReceptionChat(
                customerList.Single(x => x.UserName == customerName),
                customerService, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10);

            view.ChatLog = chatHistory;
        }


 
    }

    

}

