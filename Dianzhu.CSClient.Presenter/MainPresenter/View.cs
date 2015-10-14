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
using FluentValidation.Results;
namespace Dianzhu.CSClient.Presenter
{
    public partial class MainPresenter
    {
        /// <summary>
        /// 激活一个用户,如果当前用户,则不需要加载 或者禁用当前用户的按钮.
        /// 这个动作应该被订阅.
        /// </summary>
        /// <param name="buttonText"></param>
        private void ActiveCustomer(ServiceOrder order)
        {

            //view.CurrentCustomerName = order.Customer.DisplayName;
            CurrentServiceOrder = order;
            
            //LoadChatHistory(buttonText);
            view.SetCustomerButtonStyle(order, em_ButtonStyle.Readed);
             
            
            
        }


        private void View_CreateNewOrder()
        {
            if (CurrentServiceOrder == null)
            { return; }
            ServiceOrder newOrder = ServiceOrder.Create(Model.Enums.enum_ServiceScopeType.OSIM,
                string.Empty, string.Empty, string.Empty, 0, string.Empty, CurrentServiceOrder.Customer,
                string.Empty, 0, 0);


            
                bllOrder.SaveOrUpdate(newOrder);
            
            ReceptionChat chat = new ReceptionChat
            {
                ChatType = Model.Enums.enum_ChatType.Text,
                From = customerService,
                To = CurrentServiceOrder.Customer,
                MessageBody = "创建新订单",
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                ServiceOrder = newOrder
            };
            SendMessage(chat);
        }

        /// <summary>
        /// 生成订单的支付链接
        /// </summary>
        void view_CreateOrder()
        {
            decimal unitPrice = Convert.ToDecimal(view.ServiceUnitPrice);
            decimal orderAmount = Convert.ToDecimal(view.OrderAmount);
            
            Debug.Assert(CurrentServiceOrder.OrderStatus == Model.Enums.enum_OrderStatus.Draft, "orderStatus is not valid");
            SaveCurrentOrder();
            CurrentServiceOrder.OrderStatus = Model.Enums.enum_OrderStatus.Created;
            string payLink = CurrentServiceOrder.BuildPayLink(System.Configuration.ConfigurationManager.AppSettings["PayUrl"]);

            ReceptionChatNotice chatNotice = new ReceptionChatNotice
            {
                ChatType = Model.Enums.enum_ChatType.Notice,
                From = customerService,
                To = CurrentServiceOrder.Customer,
                SavedTime = DateTime.Now,
                ServiceOrder = CurrentServiceOrder,
                UserObj = customerService,
  
                  MessageBody="支付链接",   SendTime=DateTime.Now
            };
            
            SendMessage(chatNotice);
            LoadCurrentOrder(CurrentServiceOrder);
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
        void LoadCurrentOrder(ServiceOrder order)
        {
            if (order == null)
            {
                return;
            }
             
            if (!OrderList.Contains (order))
            {
                OrderList.Add(order);
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
            view.OrderNumber = order.Id.ToString();
            view.OrderStatus = order.OrderStatus == Model.Enums.enum_OrderStatus.Draft ? "草稿"
            : order.OrderStatus == Model.Enums.enum_OrderStatus.Created ? "已创建,等待支付"
            : order.OrderStatus == Model.Enums.enum_OrderStatus.Created ? "已创建,等待支付"
            : order.OrderStatus == Model.Enums.enum_OrderStatus.Created ? "已创建,等待支付"
            : order.OrderStatus == Model.Enums.enum_OrderStatus.Created ? "已创建,等待支付"
            : order.OrderStatus == Model.Enums.enum_OrderStatus.Created ? "已创建,等待支付"
            :order.OrderStatus.ToString();
            if (order.OrderStatus == Model.Enums.enum_OrderStatus.Draft)
            {
                view.CanEditOrder = true;
            }
            else {
                view.CanEditOrder = false;
            }




        }
        void SaveCurrentOrder()
        {
            
            if (CurrentServiceOrder==null)
            {
                return;
            }
             
            CurrentServiceOrder.ServiceName = view.ServiceName;
            CurrentServiceOrder.ServiceBusinessName = view.ServiceBusinessName;
            CurrentServiceOrder.ServiceDescription = view.ServiceDescription;
            CurrentServiceOrder.ServiceUnitPrice =Convert.ToDecimal( view.ServiceUnitPrice);
            CurrentServiceOrder.ServiceURL = view.ServiceUrl;
            CurrentServiceOrder.OrderAmount =Convert.ToDecimal( view.OrderAmount);
            CurrentServiceOrder.TargetAddress = view.TargetAddress;
            CurrentServiceOrder.Memo = view.Memo;
            CurrentServiceOrder.TargetTime = view.ServiceTime;
            bllOrder.SaveOrUpdate(CurrentServiceOrder);
        }

        
        /// <summary>
        /// 界面的搜索事件
        /// </summary>
        void view_SearchService()
        {
            int total;
            var serviceList = bllService.Search(view.SerachKeyword, 0, 10, out total);
            view.SearchedService = serviceList;

            string pushServiceKey = CurrentServiceOrder == null ? "dianzhucs" : CurrentServiceOrder.Id.ToString();
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
        

        /// <summary>
        /// 1)判断该用户是否已在聊天会话中
        /// 2)如果在 则取出该会话, 没有 则创建会话
        /// </summary>
        /// <param name="chat"></param>


        void view_SendMessageHandler()
        {

            //

            if (CurrentServiceOrder == null)
            { return; }

            if (string.IsNullOrEmpty(view.MessageTextBox.Trim())) return;

            ReceptionChat chat = new ReceptionChat
            {
                ChatType = Model.Enums.enum_ChatType.Text,
                From = customerService,
                To = CurrentServiceOrder.Customer,
                MessageBody = view.MessageTextBox,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                ServiceOrder = CurrentServiceOrder
            };

            SendMessage(chat);
            view.MessageTextBox = string.Empty;
        }
        private void view_SendMediaHandler(string domainType, string mediaType)
        {
            if (CurrentServiceOrder == null) return;
            
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
                domainType, mediaType);
            //string result = PHSuit.IOHelper.UploadFileHttp(
            //    GlobalViables.MediaUploadUrl,
            //     string.Empty, bytes, fileExtension);

            ReceptionChatMedia chat = new ReceptionChatMedia
            {
                ServiceOrder = CurrentServiceOrder,
                ChatType = Model.Enums.enum_ChatType.Media,
                From = customerService,
                To = CurrentServiceOrder.Customer,
                MessageBody = view.MessageTextBox,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                MedialUrl =GlobalViables.MediaGetUrl+ fileName,
                MediaType = mediaType
            };

            SendMessage(chat);
            view.MessageTextBox = string.Empty;

        }




    }



}

