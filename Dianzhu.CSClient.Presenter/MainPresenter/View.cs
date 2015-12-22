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
    /// <summary>
    /// Presenter for MainForm
    /// </summary>
    public partial class MainPresenter
    {
        /// <summary>
        /// 切换订单1:改变当前订单, 改变按钮样式
        /// </summary>
        /// <param name="order"></param>
        private void ActiveCustomer(ServiceOrder order)
        {
            CurrentServiceOrder = order;
            view.SetCustomerButtonStyle(order, em_ButtonStyle.Actived);
        }

        /// <summary>
        /// ToDO: 在当前订单内,创建一个新订单,涉及到组合订单的业务逻辑.未实现.
        /// </summary>
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
                MessageBody = "创建新订单，订单Id："+newOrder.Id,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                ServiceOrder = newOrder
            };
            SendMessage(chat);

            view.OrderNumber = newOrder.Id.ToString();
            view.OrderStatus = Model.Enums.enum_OrderStatus.Draft.ToString();
            CleanOrderData();
        }

        /// <summary>
        /// 初始化值
        /// </summary>
        private void CleanOrderData()
        {
            view.CurrentService = null;
            view.ServiceName = string.Empty;
            view.ServiceBusinessName = string.Empty;
            view.ServiceDescription = string.Empty;
            view.ServiceUnitPrice = "0.0000";
            view.ServiceUrl = string.Empty;
            view.OrderAmount = "0.0000";
            view.TargetAddress = string.Empty;
            view.Memo = string.Empty;
            view.ServiceTime = string.Empty;            
        }

        /// <summary>
        /// 从草稿订单创建正式订单
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

                MessageBody = "支付链接" + payLink,
                SendTime = DateTime.Now
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
        /// 当前订单发生改变(点击了另外的订单按钮)
        /// </summary>
        void view_BeforeCustomerChanged()
        {
            // view.SetCustomerButtonStyle(CurrentServiceOrder, em_ButtonStyle.Readed);
            //保存当前界面的草稿订单先~
            SaveCurrentOrder();
        }
        //加载订单
        void LoadCurrentOrder(ServiceOrder order)
        {
            if (order == null)
            {
                return;
            }

            if (!OrderList.Contains(order))
            {
                OrderList.Add(order);
            }
            view.CurrentService = order.Service;
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
            : order.OrderStatus.ToString();
            if (order.OrderStatus == Model.Enums.enum_OrderStatus.Draft)
            {
                view.CanEditOrder = true;
            }
            else
            {
                view.CanEditOrder = false;
            }

        }
        /// <summary>
        /// 保存当前界面的订单数据
        /// </summary>
        void SaveCurrentOrder()
        {

            if (CurrentServiceOrder == null)
            {
                return;
            }

            CurrentServiceOrder.Service = view.CurrentService;
            CurrentServiceOrder.ServiceName = view.ServiceName;
            CurrentServiceOrder.ServiceBusinessName = view.ServiceBusinessName;
            CurrentServiceOrder.ServiceDescription = view.ServiceDescription;
            CurrentServiceOrder.ServiceUnitPrice = Convert.ToDecimal(view.ServiceUnitPrice);
            CurrentServiceOrder.ServiceURL = view.ServiceUrl;
            CurrentServiceOrder.OrderAmount = Convert.ToDecimal(view.OrderAmount);
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
        /// 发送消息~
        /// </summary>
        void view_SendMessageHandler()
        {
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
        /// <summary>
        /// 发送多媒体消息(截图,本地图片,音频,视频)
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="domainType"></param>
        /// <param name="mediaType"></param>
        private void view_SendMediaHandler(byte[] fileData, string domainType, string mediaType)
        {
            if (CurrentServiceOrder == null) return;
 
            string s = Convert.ToBase64String(fileData);
            string fileName = MediaServer.HttpUploader.Upload(GlobalViables.MediaUploadUrl, s, domainType, mediaType);

            ReceptionChatMedia chat = new ReceptionChatMedia
            {
                ServiceOrder = CurrentServiceOrder,
                ChatType = Model.Enums.enum_ChatType.Media,
                From = customerService,
                To = CurrentServiceOrder.Customer,
                MessageBody = view.MessageTextBox,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                MedialUrl = GlobalViables.MediaGetUrl + fileName,
                MediaType = mediaType
            };

            SendMessage(chat);
            view.MessageTextBox = string.Empty;

        }




    }



}

