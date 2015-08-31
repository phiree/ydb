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
        IMessageAdapter.IAdapter messageAdapter;
        #region state 
        DZMembership customerService=GlobalViables.CurrentCustomerService; //当前k客服
        DZMembership customer=null; //
        List<DZMembership> customerList = new List<DZMembership>(); // 客户列表
        Dictionary<string, ReceptionBase> ReceptionList = new Dictionary<string, ReceptionBase>();//接待列表
        Dictionary<string, IList<DZService>> SearchResultForCustomer = new Dictionary<string, IList<DZService>>();//搜索列表
        
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
            
        }

        //从im服务器接收到的消息. 元消息已经被xmpp转换成receptionChat
        void IMReceivedMessage(ReceptionChat chat)
        {
            
            string customerName = chat.From.UserName;
            if (!customerList.Any(x => x.UserName ==  customerName))
            {
                AddCustomer(customerName);
                view.AddCustomerButtonWithStyle(customerName, em_ButtonStyle.Unread);
            }

            else
            {
                if (customer == null || customer.UserName != customerName)
                {
                    view.SetCustomerButtonStyle(customerName, em_ButtonStyle.Unread);
                }
            }


           // ReceptionChat chat = SaveMessage(message, customerName, false, mediaUrl);
            //chat.ServiceId = confirm_service_id;
            //收到消息之后创建订单, 还是客服点击发送链接确认之后 创建订单
            ReceptionBase reception = null;
            if (ReceptionList.ContainsKey(chat.From.UserName))
            {
                reception = ReceptionList[chat.From.UserName];
            }
            else
            {
                reception = new ReceptionCustomer { Receiver=customerService, Sender=chat.From };
                ReceptionList.Add(chat.From.UserName, reception);
            }
            reception.ChatHistory.Add(chat);
            bllReception.Save(reception);
            //chat.ServiceId = confirm_service_id;
            if (customer != null && customerName == customer.UserName)
            {
                view.LoadOneChat(chat);
            }
        }



        void IMPresent(string userFrom,int presentType)
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

        void view_SendMessageHandler( )
        {
             
            //
            if (customer == null) return;
            if (string.IsNullOrEmpty(view.MessageTextBox.Trim())) return;
            //todo
            ReceptionChat chat = new ReceptionChat { 
             ChatType= Model.Enums.enum_ChatType.Text,
              From=customerService,
              To=customer,
               MessageBody=view.MessageTextBox,
              SendTime=DateTime.Now,
              SavedTime=DateTime.Now
            };
            //需要从当前会话列表中提取对应客户, 所以需要传入客户名称
            SaveMessage(chat);
            view.LoadOneChat(chat);
            instantMessage.SendMessage(chat);
            view.MessageTextBox = string.Empty;
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

        private void AddCustomer(string customerName)
        {
            DZMembership newCustomer = bllMember.GetUserByName(customerName);
            customerList.Add(newCustomer);
        }
        /// <summary>
        /// 激活一个用户,如果当前用户,则不需要加载 或者禁用当前用户的按钮.
        /// </summary>
        /// <param name="buttonText"></param>
        public void ActiveCustomer(string buttonText)
        {
            
            LoadChatHistory(buttonText);
            view.SetCustomerButtonStyle(buttonText, em_ButtonStyle.Actived);
             
            customer = customerList.Single(x => x.UserName == buttonText);
            //设置当前激活的用户
            if (SearchResultForCustomer.ContainsKey(buttonText))
            {
                view.SearchedService= SearchResultForCustomer[buttonText];
                
            }
        }

        
 
        public void SendPayLink(string service_id)
        {
            DZService service = bllService.GetOne(new Guid(service_id));
            //创建订单.

        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// 
        public ReceptionChat SaveMessage(ReceptionChat chat )
        {
#region 保存聊天消息
            string customerName = customer.UserName;
            string message = chat.MessageBody;
            bool isSend = chat.From == customerService;
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
                    Sender = customer,
                    Receiver = customerService,
                    TimeBegin = DateTime.Now,
                   
                };
                ReceptionList.Add(customerName, re);
            }
            DateTime now = DateTime.Now;
                var chat2 = new ReceptionChat
                {
                  
                    MessageBody = message,
                   
                    SavedTime=now,
                    MessageMediaUrl=mediaUrl
                };
                if (isSend)
                {
                    chat.SendTime = now;
                    chat.From = customerService;
                    chat.To = customer;
                }
                else
                {
                    chat.ReceiveTime = now;
                    chat.To = customerService;
                    chat.From = customer;
                }
                re.ChatHistory.Add(chat);
                bllReception.Save(re);
                return chat;
            
#endregion


        }

        private void LoadChatHistory(string customerName)
        {
            bool isContain = ReceptionList.ContainsKey(customerName);

            var chatHistory = bllReception.GetHistoryReceptionChat(
                customerList.Single(x => x.UserName == customerName),
                customerService,DateTime.Now.AddMonths(-1),DateTime.Now.AddDays(1), 10);

            view.ChatLog = chatHistory;
        }
        /// <summary>
        /// 格式化一条聊天信息
        /// </summary>
        /// <param name="chat">聊天类</param>
        /// <param name="isFrom">发送方向</param>
        /// <returns></returns>
        private string BuildFormatedLine(ReceptionChat chat,bool isFrom)
        { /*
           @"{\rtf1\pc \b 02/11/2010 - 05:15 PM 
           * - Adam:\b0 Another test notes added on 2nd November \par
           * \b 02/11/2010 - 05:14 PM - Z_kas:\b0 Test Notes. STAGE CHANGED TO: N Enq - Send Quote\par \b 02/11/2010 - 05:12 PM - user32:\b0 Another test notes added on 2nd November"
           */
            string line=@"\rtf1\pc";
            return "";
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="message"></param>
        public void ReceiveMessage(string customerName, string message,string mediaUrl,string confirm_service_id)
        { 
            //保存聊天记录, 改变view的button,聊天窗口增加一条消息
           
          

        }
        
        #endregion

        #region searchservice

        public void SearchService(string keyword)
        {
            int total;
            var serviceList = bllService.Search(view.SerachKeyword, 0, 10, out total);
            view.SearchedService=serviceList;
           
            string pushServiceKey = customer == null ? "dianzhucs" : customer.UserName;
            if (SearchResultForCustomer.ContainsKey(pushServiceKey))
            {
                SearchResultForCustomer[pushServiceKey]=serviceList;
            }
            else
            {
                SearchResultForCustomer.Add(pushServiceKey, serviceList);
            }
        }
        /// <summary>
        /// 推送一项服务.
        /// </summary>
        /// <param name="serviceId"></param>
        public void  PushService(Guid serviceId)
        { 
            //接待 加入该服务.
            ReceptionCustomer reception = (ReceptionCustomer)ReceptionList[customer.UserName];
            DZService service=bllService.GetOne(serviceId);
            reception.PushedServices.Add(service);
            //保存到聊天记录并加入聊天记录
            string message="已推送服务:" + service.Name;
            ReceptionChat chat = new ReceptionChat();
            SaveMessage(chat);
            //todo
            instantMessage.SendMessage(chat);
             

        }
        #endregion
    }
}

