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
    public class MainPresenter
    {
        IVew.IMainFormView view;
        InstantMessage instantMessage;
        DZMembershipProvider bllMember;
        BLLDZService bllService;
        BLLReception bllReception;
        BLLServiceOrder bllOrder;
        #region state
        DZMembership customerService = GlobalViables.CurrentCustomerService; //当前k客服
        DZMembership customer = null; //当前激活客户
        List<DZMembership> customerList = new List<DZMembership>(); // 客户列表
        Dictionary<string, ReceptionBase> ReceptionList = new Dictionary<string, ReceptionBase>();//接待列表
        Dictionary<string, IList<DZService>> SearchResultForCustomer
            = new Dictionary<string, IList<DZService>>();//客户名称搜索列表

        #endregion
        public MainPresenter(IVew.IMainFormView view,
            InstantMessage instantMessage,
            IMessageAdapter.IAdapter messageAdapter,
            DZMembershipProvider bllMember, BLLReception bllReception,
            BLLDZService bllService, BLLServiceOrder bllOrder)
        {
            this.view = view;
            this.instantMessage = instantMessage;
            this.bllMember = bllMember;
            this.bllReception = bllReception;
            this.bllService = bllService;

            this.bllOrder = bllOrder;

            //present insancemessage的委托
            this.instantMessage.IMPresent += new IMPresent(IMPresent);
            this.instantMessage.IMReceivedMessage += new IMReceivedMessage(IMReceivedMessage);
            //iview的委托
            this.view.SendMessageHandler += new SendMessageHandler(view_SendMessageHandler);
            this.view.ActiveCustomerHandler += new IVew.ActiveCustomerHandler(ActiveCustomer);
            this.view.PushExternalService += new PushExternalService(view_PushExternalService);
            this.view.PushInternalService += new PushInternalService(view_PushInternalService);
            this.view.ButtonNamePrefix = System.Configuration.ConfigurationManager.AppSettings["ButtonNamePrefix"];
            this.view.SearchService += new IVew.SearchService(view_SearchService);
        }

        void view_PushInternalService(DZService service)
        {
            ReceptionChatService chat = new ReceptionChatService {
             Service=service,
             From=customerService,
             To=customer,
              MessageBody="已推送服务"
            };
            SendMessage(chat);
        }

        void view_SearchService()
        {
            int total;
            var serviceList = bllService.Search(view.SerachKeyword, 0, 10, out total);
            view.SearchedService = serviceList;

            string pushServiceKey = customer == null ? "dianzhucs" : customer.UserName;
            if (SearchResultForCustomer.ContainsKey(pushServiceKey))
            {
                SearchResultForCustomer[pushServiceKey] = serviceList;
            }
            else
            {
                SearchResultForCustomer.Add(pushServiceKey, serviceList);
            }
        }

        /// <summary>
        /// 推送外部服务
        /// </summary>
        void view_PushExternalService()
        {
            ReceptionChatService chatService = new ReceptionChatService();
            chatService.ChatType = Model.Enums.enum_ChatType.PushedService;

            chatService.MessageBody = "已推送服务";

            chatService.ServiceBusinessName = view.ServiceBusinessName;
            chatService.ServiceDescription = view.ServiceDescription;
            chatService.ServiceName = view.ServiceName;
            chatService.UnitPrice = Convert.ToDecimal(view.ServiceUnitPrice);
            chatService.ServiceUrl = view.ServiceUrl;

            SaveMessage(chatService, true);
            instantMessage.SendMessage(chatService);
        }

        /// <summary>
        /// 1)判断该用户是否已在聊天会话中
        /// 2)如果在 则取出该会话, 没有 则创建会话
        /// </summary>
        /// <param name="chat"></param>
        void IMReceivedMessage(ReceptionChat chat)
        {

            //判断客户列表中是否有该用户
            bool isIn = AddCustomer(chat.From.UserName);
            if (customer != chat.From)
            {
                if (isIn)
                {
                    view.SetCustomerButtonStyle(chat.From.UserName, em_ButtonStyle.Unread);
                }
                else
                {
                    view.AddCustomerButtonWithStyle(chat.From.UserName, em_ButtonStyle.Unread);
                }
            }
            SaveMessage(chat, false);
            //当前激活客户是否等于发送者.
            if (customer != null && chat.From == customer)
            {
                view.LoadOneChat(chat);
            }
        }



        void IMPresent(string userFrom, int presentType)
        {
            string userName = PHSuit.StringHelper.EnsureNormalUserName(userFrom);
            bool isInList = customerList.Any(x => x.UserName == userName);
            switch (presentType)
            {
                case -1:
                    //登录,判断当前用户是否已经在列表中

                    if (isInList)
                    {
                        //改变对应按钮的样式.
                        view.SetCustomerButtonStyle(userName, em_ButtonStyle.Login);
                    }
                    else
                    {
                        AddCustomer(userName);
                        view.AddCustomerButtonWithStyle(userName, em_ButtonStyle.Login);
                    }

                    break;
                case 4:
                    if (isInList)
                    {
                        view.SetCustomerButtonStyle(userName, em_ButtonStyle.LogOff);
                    }
                    else
                    {
                    }

                    break;
                default: break;
            }
        }

        void view_SendMessageHandler()
        {

            //
            if (customer == null) return;
            

            if (string.IsNullOrEmpty(view.MessageTextBox.Trim())) return;
           
            ReceptionChat chat = new ReceptionChat
            {
                ChatType = Model.Enums.enum_ChatType.Text,
                From = customerService,
                To = customer,
                MessageBody = view.MessageTextBox,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now
            };
             
            SendMessage(chat);
            view.MessageTextBox = string.Empty;
        }

        private void SendMessage(ReceptionChat chat)
        {
            SaveMessage(chat, true);
            instantMessage.SendMessage(chat);
            view.LoadOneChat(chat);
            
        }


        #region Chat

        /*
  public enum PresenceType
     {
         available = -1,
         subscribe = 0,
         subscribed = 1,
         unsubscribe = 2,
         unsubscribed = 3,
         unavailable = 4,
         invisible = 5,
         error = 6,
         probe = 7,
     }
  */
        public void OnPresent(int presentType, string customerName)
        {
            string userName = PHSuit.StringHelper.EnsureNormalUserName(customerName);
            bool isInList = customerList.Any(x => x.UserName == userName);
            switch (presentType)
            {
                case -1:
                    //登录,判断当前用户是否已经在列表中

                    if (isInList)
                    {
                        //改变对应按钮的样式.
                        view.SetCustomerButtonStyle(userName, em_ButtonStyle.Login);
                    }
                    else
                    {
                        AddCustomer(userName);
                        view.AddCustomerButtonWithStyle(userName, em_ButtonStyle.Login);

                    }

                    break;
                case 4:
                    if (isInList)
                    {
                        view.SetCustomerButtonStyle(userName, em_ButtonStyle.LogOff);
                    }
                    else
                    {

                    }

                    break;
                default: break;
            }
        }
        /// <summary>
        /// 当前列表添加客户,如果已经存在 则返回true.
        /// </summary>
        /// <param name="customerName"></param>
        private bool AddCustomer(string customerName)
        {
            string userName = PHSuit.StringHelper.EnsureNormalUserName(customerName);
            bool isInList = customerList.Any(x => x.UserName == userName);
            if (isInList)
            {
                return true;
            }
            DZMembership newCustomer = bllMember.GetUserByName(customerName);
            customerList.Add(newCustomer);
            return false;
        }

        /// <summary>
        /// 激活一个用户,如果当前用户,则不需要加载 或者禁用当前用户的按钮.
        /// </summary>
        /// <param name="buttonText"></param>
        public void ActiveCustomer(string buttonText)
        {
            view.CurrentCustomerName = buttonText;

            LoadChatHistory(buttonText);
            view.SetCustomerButtonStyle(buttonText, em_ButtonStyle.Actived);

            customer = customerList.Single(x => x.UserName == buttonText);
            //设置当前激活的用户
            if (SearchResultForCustomer.ContainsKey(buttonText))
            {
                view.SearchedService = SearchResultForCustomer[buttonText];

            }
        }



        public void SendPayLink(string service_id)
        {
            DZService service = bllService.GetOne(new Guid(service_id));
            //创建订单.

        }
        /// <summary>
        /// 保存消息.
        /// 此方法不知道消息方向.
        /// </summary>
        /// <param name="message"></param>
        /// 
        public void SaveMessage(ReceptionChat chat, bool isSend)
        {
            #region 保存聊天消息

            DZMembership fromCustomer= isSend ? chat.To : chat.From;
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

        private void LoadChatHistory(string customerName)
        {
            bool isContain = ReceptionList.ContainsKey(customerName);

            var chatHistory = bllReception.GetHistoryReceptionChat(
                customerList.Single(x => x.UserName == customerName),
                customerService, DateTime.Now.AddMonths(-1), DateTime.Now.AddDays(1), 10);

            view.ChatLog = chatHistory;
        }
        /// <summary>
        /// 格式化一条聊天信息
        /// </summary>
        /// <param name="chat">聊天类</param>
        /// <param name="isFrom">发送方向</param>
        /// <returns></returns>
        private string BuildFormatedLine(ReceptionChat chat, bool isFrom)
        { /*
           @"{\rtf1\pc \b 02/11/2010 - 05:15 PM 
           * - Adam:\b0 Another test notes added on 2nd November \par
           * \b 02/11/2010 - 05:14 PM - Z_kas:\b0 Test Notes. STAGE CHANGED TO: N Enq - Send Quote\par \b 02/11/2010 - 05:12 PM - user32:\b0 Another test notes added on 2nd November"
           */
            string line = @"\rtf1\pc";
            return "";
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="message"></param>
        public void ReceiveMessage(string customerName, string message, string mediaUrl, string confirm_service_id)
        {
            //保存聊天记录, 改变view的button,聊天窗口增加一条消息



        }

        #endregion

        #region searchservice

        public void SearchService(string keyword)
        {
            
        }
        /// <summary>
        /// 推送一项服务.
        /// </summary>
        /// <param name="serviceId"></param>
        public void PushService(Guid serviceId)
        {
            //接待 加入该服务.
            ReceptionCustomer reception = (ReceptionCustomer)ReceptionList[customer.UserName];
            DZService service = bllService.GetOne(serviceId);
            reception.PushedServices.Add(service);
            //保存到聊天记录并加入聊天记录
            string message = "已推送服务:" + service.Name;
            ReceptionChat chat = new ReceptionChat();
            SaveMessage(chat, true);
            //todo
            instantMessage.SendMessage(chat);


        }
        #endregion
    }
}

