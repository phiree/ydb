using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    public class FormController
    {
        IView view;
        DZMembershipProvider bllMember;
        BLLDZService bllService;
        BLLReception bllReception;
        #region local variables
        private static List<DZMembership> CustomerList = new List<DZMembership>();
        //接待列表
        private static Dictionary<string, ReceptionBase> ReceptionList = new Dictionary<string, ReceptionBase>();
        private static DZMembership CurrentCustomer;
        private static Dictionary<string, IList<DZService>> SearchResultForCustomer = new Dictionary<string, IList<DZService>>();
        #endregion
        
        public FormController(IView view, DZMembershipProvider bllMember, BLLReception bllReception,
            BLLDZService bllService)
        {
            this.view = view;
            this.bllMember = bllMember;
            this.bllReception = bllReception;
            this.bllService = bllService;
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
            bool isInList = CustomerList.Any(x => x.UserName == userName);
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
            CustomerList.Add(newCustomer);
        }
        public void ActiveCustomer(string buttonText)
        {
            LoadChatHistory(buttonText);
            view.SetCustomerButtonStyle(buttonText, em_ButtonStyle.Actived);
            view.CurrentCustomerName = buttonText;
            CurrentCustomer = CustomerList.Single(x => x.UserName == buttonText);
            //设置当前激活的用户
            if (SearchResultForCustomer.ContainsKey(buttonText))
            {
                view.LoadSearchHistory(SearchResultForCustomer[buttonText]);
            }
        }

        public void SendMessage(string message, string customerName)
        {
            //保存消息
            if (CurrentCustomer == null) return;
          ReceptionChat chat=  SaveMessage(message, customerName, true,string.Empty);
          view.LoadOneChat(chat);
            //
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
            DZMembership customer = CustomerList.Single(x => x.UserName == customerName);
            ReceptionBase re;
            
            if (isIn)
            {
                re = ReceptionList[customerName];
            }
            else
            {
                re = new ReceptionBase
                {
                    Sender = customer,
                    Receiver = GlobalViables.CurrentCustomerService,
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
                    chat.From = GlobalViables.CurrentCustomerService;
                    chat.To = customer;
                }
                else
                {
                    chat.ReceiveTime = now;
                    chat.To = GlobalViables.CurrentCustomerService;
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
                CustomerList.Single(x => x.UserName == customerName),
                GlobalViables.CurrentCustomerService, 10);

            view.ChatHistory = chatHistory;
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
        public void ReceiveMessage(string customerName, string message,string mediaUrl)
        { 
            //保存聊天记录, 改变view的button,聊天窗口增加一条消息
           
           if (!CustomerList.Any(x => x.UserName == customerName))
           {
               AddCustomer(customerName);
               view.AddCustomerButtonWithStyle(customerName, em_ButtonStyle.Unread);
           }

           else
           {
               if (CurrentCustomer == null || CurrentCustomer.UserName != customerName)
               {
                   view.SetCustomerButtonStyle(customerName, em_ButtonStyle.Unread);
               }
           }

           ReceptionChat chat = SaveMessage(message, customerName, false,mediaUrl);
           if (CurrentCustomer != null && customerName == CurrentCustomer.UserName)
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
            view.LoadSearchHistory(serviceList);
            string pushServiceKey=CurrentCustomer==null?"dianzhucs":CurrentCustomer.UserName;
            if (SearchResultForCustomer.ContainsKey(pushServiceKey))
            {
                SearchResultForCustomer[pushServiceKey]=serviceList;
            }
            else
            {
                SearchResultForCustomer.Add(pushServiceKey, serviceList);
            }
        }
        public void PushService(Guid serviceId)
        { 
            
        }
        #endregion
    }
}

