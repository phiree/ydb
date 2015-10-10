using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.CSClient.IVew;
using Dianzhu.CSClient.IInstantMessage;
using System.IO;
using System.Diagnostics;

namespace Dianzhu.CSClient.Presenter
{
    public partial class MainPresenter
    {
        /// <summary>
        /// 激活一个用户,如果当前用户,则不需要加载 或者禁用当前用户的按钮.
        /// 这个动作应该被订阅.
        /// </summary>
        /// <param name="buttonText"></param>
        private void ActiveCustomer(DZMembership clickedCustomer )
        {
            view.CurrentCustomerName = clickedCustomer.DisplayName;

            //LoadChatHistory(buttonText);
            view.SetCustomerButtonStyle(clickedCustomer, em_ButtonStyle.Actived);

            customer = customerList.Single(x => x.UserName == clickedCustomer.UserName);
            //设置当前激活的用户
            if (SearchResultForCustomer.ContainsKey(clickedCustomer.DisplayName))
            {
                view.SearchedService = SearchResultForCustomer[clickedCustomer.DisplayName];
            }
        }


        /// <summary>
        /// 生成订单的支付链接
        /// </summary>
        void view_CreateOrder()
        {
            decimal unitPrice = Convert.ToDecimal(view.ServiceUnitPrice);
            decimal orderAmount = Convert.ToDecimal(view.OrderAmount);
            ServiceOrder order = OrderList[customer];
            Debug.Assert(order.OrderStatus == Model.Enums.enum_OrderStatus.Draft, "orderStatus is not valid");

            
            

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
        void view_BeforeCustomerChanged()
        {
            SaveCurrentOrder();
        }
        //加载当前用户的订单
        void LoadCurrentOrder(DZMembership customer)
        {
            if (customer==null)
            {
                return;
            }
            ServiceOrder order;
            if (CustomerCurrentOrder.ContainsKey(customer.UserName))
            {
                order = CustomerCurrentOrder[customer.UserName];
            }
            else
            {
               
                  order = ServiceOrder.Create(Model.Enums.enum_ServiceScopeType.OSIM,
                    string.Empty, string.Empty, string.Empty, 0, string.Empty, customer, string.Empty, 0, 0);
                CustomerCurrentOrder.Add(customer.UserName, order);
            }
            view.ServiceName = order.ServiceName;
            view.ServiceBusinessName = order.ServiceBusinessName;
            view.ServiceDescription = order.ServiceDescription;
            view.ServiceUnitPrice = order.ServiceUnitPrice.ToString();
            view.ServiceUrl = order.ServiceURL;
            view.OrderAmount = order.OrderAmount.ToString();
            view.TargetAddress = order.TargetAddress;
            view.Memo = order.Memo;
            view.ServiceTime = order.TargetTime;

        }
        void SaveCurrentOrder()
        {
            if (customer==null)
            {
                return;
            }
            ServiceOrder viewOrder;
            if (CustomerCurrentOrder.ContainsKey(customer.UserName))
            {
                viewOrder = CustomerCurrentOrder[customer.UserName];
            }
            else
            {
                viewOrder = ServiceOrder.Create(Model.Enums.enum_ServiceScopeType.OSIM,
                    string.Empty, string.Empty, string.Empty, 0, string.Empty, customer, string.Empty, 0, 0);
                CustomerCurrentOrder.Add(customer.UserName, viewOrder);
            }
            viewOrder.ServiceName = view.ServiceName;
            viewOrder.ServiceBusinessName = view.ServiceBusinessName;
            viewOrder.ServiceDescription = view.ServiceDescription;
            viewOrder.ServiceUnitPrice =Convert.ToDecimal( view.ServiceUnitPrice);
            viewOrder.ServiceURL = view.ServiceUrl;
            viewOrder.OrderAmount =Convert.ToDecimal( view.OrderAmount);
            viewOrder.TargetAddress = view.TargetAddress;
            viewOrder.Memo = view.Memo;
            viewOrder.TargetTime = view.ServiceTime;
            bllOrder.SaveOrUpdate(viewOrder);
        }

        void view_PushInternalService(DZService service)
        {
            //ReceptionChatServicePushed chat = new ReceptionChatServicePushed
            //{
            //    ChatType = Model.Enums.enum_ChatType.PushedService,
            //    Service = service,
            //    From = customerService,
            //    To = customer,
            //    MessageBody = "已推送服务"
            //};
            //SendMessage(chat);
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
            //ReceptionChatServicePushed chatService = new ReceptionChatServicePushed();
            //chatService.ChatType = Model.Enums.enum_ChatType.PushedService;

            //chatService.MessageBody = "已推送服务";

            //chatService.ServiceBusinessName = view.ServiceBusinessName;
            //chatService.ServiceDescription = view.ServiceDescription;
            //chatService.ServiceName = view.ServiceName;
            //chatService.UnitPrice = Convert.ToDecimal(view.ServiceUnitPrice);
            //chatService.ServiceUrl = view.ServiceUrl;

            //SaveMessage(chatService, true);
            //instantMessage.SendMessage(chatService);
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
            ServiceOrder order = OrderList[customer];

            if (string.IsNullOrEmpty(view.MessageTextBox.Trim())) return;

            ReceptionChat chat = new ReceptionChat
            {
                ChatType = Model.Enums.enum_ChatType.Text,
                From = customerService,
                To = customer,
                MessageBody = view.MessageTextBox,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                ServiceOrder = order
            };

            SendMessage(chat);
            view.MessageTextBox = string.Empty;
        }
        private void view_SendImageHandler()
        {
            if (customer == null) return;
            
            System.IO.FileStream fs = view.SelectedImageStream as System.IO.FileStream;
                string fileExtension = Path.GetExtension(view.SelectedImageName);
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                string s = Convert.ToBase64String(bytes);
         string fileName=   MediaServer.HttpUploader.Upload(
             GlobalViables.MediaUploadUrl, s, view.SelectedImageName,
                "ChatImage", "image");
                //string result = PHSuit.IOHelper.UploadFileHttp(
                //    GlobalViables.MediaUploadUrl,
                //     string.Empty, bytes, fileExtension);

            ReceptionChatMedia chat = new ReceptionChatMedia
            {
                ChatType = Model.Enums.enum_ChatType.Media,
                From = customerService,
                To = customer,
                MessageBody = view.MessageTextBox,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                MedialUrl =GlobalViables.MediaGetUrl+ fileName,
                MediaType = "image"
            };

            SendMessage(chat);
            view.MessageTextBox = string.Empty;

        }




    }



}

