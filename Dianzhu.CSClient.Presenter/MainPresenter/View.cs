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
        /// <summary>
        /// 激活一个用户,如果当前用户,则不需要加载 或者禁用当前用户的按钮.
        /// 这个动作应该被订阅.
        /// </summary>
        /// <param name="buttonText"></param>
        private void ActiveCustomer(string buttonText)
        {
            view.CurrentCustomerName = buttonText;

            //LoadChatHistory(buttonText);
            view.SetCustomerButtonStyle(buttonText, em_ButtonStyle.Actived);

            customer = customerList.Single(x => x.UserName == buttonText);
            //设置当前激活的用户
            if (SearchResultForCustomer.ContainsKey(buttonText))
            {
                view.SearchedService = SearchResultForCustomer[buttonText];

            }
        }


        /// <summary>
        /// 创建订单 
        /// </summary>
        void view_CreateOrder()
        {
            decimal unitPrice = Convert.ToDecimal(view.ServiceUnitPrice);
            decimal orderAmount = Convert.ToDecimal(view.OrderAmount);
            
            ServiceOrder order = bllOrder.CreateOrder(customer, view.ServiceName, view.ServiceBusinessName, view.ServiceDescription,
            unitPrice, view.ServiceUrl, orderAmount, view.TargetAddress);

        }
        /// <summary>
        /// 加载该客户的订单列表 和 当前正在处理的订单
        /// </summary>
        void view_LoadOrder()
        {

        }

        void view_SendPayLink(ReceptionChat chat)
        {
            //根据接收到的服务确认消息, 创建订单. 
        }
        /// <summary>
        /// 订单数据变化
        /// </summary>
        void view_OrderChanged()
        {
            
        }
        void view_SaveOrderBeforeCustomerChange()
        {
            SaveCurrentOrder();
        }
        //加载当前用户的数据
        void LoadCurrentOrder(string customerName)
        {
           // SaveCurrentOrder();
            //先保存当前
            if (string.IsNullOrEmpty(customerName))
            {
                return;
            }
            ViewModel.ViewOrder viewOrder;
            if (CustomerCurrentOrder.ContainsKey(customer.UserName))
            {
                viewOrder = CustomerCurrentOrder[customer.UserName];
            }
            else
            {
                viewOrder = new ViewModel.ViewOrder();
                CustomerCurrentOrder.Add(customer.UserName, viewOrder);
            }
            view.ServiceName = viewOrder.ServiceName;
            view.ServiceBusinessName = viewOrder.ServiceBusinessName;
            view.ServiceDescription = viewOrder.ServiceDescription;
            view.ServiceUnitPrice = viewOrder.ServiceUnitPrice;
            view.ServiceUrl = viewOrder.ServiceUrl;
            view.OrderAmount = viewOrder.OrderAmount;
            view.TargetAddress = viewOrder.ServiceTargetAddress;
            view.Memo = viewOrder.Memo;
            view.ServiceTime = viewOrder.ServiceTime;

        }
        void SaveCurrentOrder()
        {
            if (customer==null)
            {
                return;
            }
            ViewModel.ViewOrder viewOrder;
            if (CustomerCurrentOrder.ContainsKey(customer.UserName))
            {
                viewOrder = CustomerCurrentOrder[customer.UserName];
            }
            else
            {
                viewOrder = new ViewModel.ViewOrder();
                CustomerCurrentOrder.Add(customer.UserName, viewOrder);
            }
           viewOrder.ServiceName = view.ServiceName;
            viewOrder.ServiceBusinessName = view.ServiceBusinessName;
            viewOrder.ServiceDescription = view.ServiceDescription;
            viewOrder.ServiceUnitPrice = view.ServiceUnitPrice;
            viewOrder.ServiceUrl = view.ServiceUrl;
            viewOrder.OrderAmount = view.OrderAmount;
            viewOrder.ServiceTargetAddress = view.TargetAddress;
            viewOrder.Memo = view.Memo;
            viewOrder.ServiceTime = view.ServiceTime;
        }

        void view_PushInternalService(DZService service)
        {
            ReceptionChatServicePushed chat = new ReceptionChatServicePushed
            {
                ChatType = Model.Enums.enum_ChatType.PushedService,
                Service = service,
                From = customerService,
                To = customer,
                MessageBody = "已推送服务"
            };
            SendMessage(chat);
        }
        /// <summary>
        /// 界面的搜索事件
        /// </summary>
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
            ReceptionChatServicePushed chatService = new ReceptionChatServicePushed();
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




    }



}

