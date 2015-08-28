using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.CSClient.IVew;
 
namespace Dianzhu.CSClient.Presenter
{
    public class FormController
    {
        IVew.MainFormView view;
        DZMembershipProvider bllMember;
        BLLDZService bllService;
        BLLReception bllReception;
        BLLServiceOrder bllOrder;
        #region state 
        DZMembership customerService=GlobalViables.CurrentCustomerService; //当前客户
        DZMembership customer; //客服
        List<DZMembership> customerList = new List<DZMembership>(); // 客户列表
        Dictionary<string, ReceptionBase> ReceptionList = new Dictionary<string, ReceptionBase>();//接待列表
        Dictionary<string, IList<DZService>> SearchResultForCustomer = new Dictionary<string, IList<DZService>>();//搜索列表
        
        #endregion
        public FormController(IVew.MainFormView view, DZMembershipProvider bllMember, BLLReception bllReception,
            BLLDZService bllService, BLLServiceOrder bllOrder )
        {
            this.view = view;
            this.bllMember = bllMember;
            this.bllReception = bllReception;
            this.bllService = bllService;
          
            this.bllOrder = bllOrder;
           //
            //present 无法测试了...需要把handler 和 event都隔离开去?
            GlobalViables.XMPP.OnPresent += new agsXMPP.protocol.client.PresenceHandler(xmpp_OnPresent);
            GlobalViables.XMPP.ReceiveMessageHandler += new IInstantMessage.ReceiveMessageHandler(XMPP_ReceiveMessageHandler);
            this.view.SendMessageHandler += new SendMessageHandler(view_SendMessageHandler);
            this.view.ActiveCustomerHandler += new IVew.ActiveCustomerHandler(ActiveCustomer);
            
        }

        void XMPP_ReceiveMessageHandler(string userFrom, string message)
        {
            ReceiveMessage(StringHelper.EnsureNormalUserName( userFrom), message, string.Empty, string.Empty);
        }

        

        void xmpp_OnPresent(object sender, agsXMPP.protocol.client.Presence pres)
        {
            string userName = StringHelper.EnsureNormalUserName(pres.From.User);
            bool isInList = customerList.Any(x => x.UserName == userName);
            switch (pres.Type)
            {
                case agsXMPP.protocol.client.PresenceType.available:
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
                case agsXMPP.protocol.client.PresenceType.unavailable:
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
            
            GlobalViables.XMPP.SendMessage(view.MessageTextBox, customer.UserName);
            SendMessage(view.MessageTextBox, customer.UserName);
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
            string userName = StringHelper.EnsureNormalUserName(customerName);
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
        /// 激活一个用户
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

        

        public void SendMessage(string message, string customerName)
        {
            //保存消息
            if (customer == null) return;
          ReceptionChat chat=  SaveMessage(message, customerName, true,string.Empty);
          view.LoadOneChat(chat);
          GlobalViables.XMPP.SendMessage(message, customer.UserName);
          view.MessageTextBox = string.Empty;
           
            //
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
        public ReceptionChat SaveMessage(string message,string customerName,bool isSend,string mediaUrl)
        {
#region 保存聊天消息
            
            bool isIn = ReceptionList.ContainsKey(customerName);
            DZMembership customer = customerList.Single(x => x.UserName == customerName);
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
                var chat = new ReceptionChat
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
                customerService, 10);

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
           
           if (!customerList.Any(x => x.UserName == customerName))
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
            

           ReceptionChat chat = SaveMessage(message, customerName, false,mediaUrl);
           chat.ServiceId = confirm_service_id;
           //收到消息之后创建订单, 还是客服点击发送链接确认之后 创建订单
           if (!string.IsNullOrEmpty(confirm_service_id))
           {
               
              ServiceOrder order= bllOrder.CreateOrder(customer.Id,new Guid(confirm_service_id));
           }
           chat.ServiceId = confirm_service_id;
           if (customer != null && customerName == customer.UserName)
           {
               view.LoadOneChat(chat);
           }

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
            SaveMessage(message, customer.UserName, true, string.Empty);
            SendMessage(message, customer.UserName);

        }
        #endregion
    }
}

