using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.CSClient.IView;
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
        /// 切换订单1:改变当前用户, 改变按钮样式
        /// 加载该用户的订单列表
        /// </summary>
        /// <param name="dm"></param>
        //private void ActiveCustomer(ServiceOrder order)
        private void ActiveCustomer(DZMembership dm)
        {
            //ClientState.CurrentServiceOrder = order;
            ClientState.CurrentCustomer = dm;
            //view.SetCustomerButtonStyle(order, em_ButtonStyle.Actived);
            view.SetCustomerButtonStyle(dm, em_ButtonStyle.Actived);
            int currentOrderAmount;
            ClientState.OrderList = bllOrder.GetListForCustomer(dm,1,10,out currentOrderAmount);
            this.view.OrdersList = ClientState.OrderList;
        }

        /// <summary>
        /// ToDO: 在当前订单内,创建一个新订单,涉及到组合订单的业务逻辑.未实现.
        /// </summary>
        private void View_CreateNewOrder()
        {
            if (ClientState.CurrentServiceOrder == null)
            { return; }
            ServiceOrder newOrder = ServiceOrderFactory.CreateDraft(ClientState.CurrentServiceOrder.Customer
                , ClientState.customerService);
               
                 
            bllOrder.SaveOrUpdate(newOrder);
            ClientState.CurrentServiceOrder = newOrder;
            //ReceptionChat chat = new ReceptionChat
            //{
            //    ChatType = Model.Enums.enum_ChatType.Text,
            //    From = ClientState.customerService,
            //    To = ClientState.CurrentServiceOrder.Customer,
            //    MessageBody = "创建新订单，订单Id："+newOrder.Id,
            //    SendTime = DateTime.Now,
            //    SavedTime = DateTime.Now,
            //    ServiceOrder = newOrder
            //};
            //SendMessage(chat);
            NoticeDraftNew();

            view.OrderNumber = newOrder.Id.ToString();
            view.OrderStatus = Model.Enums.enum_OrderStatus.Draft.ToString();
            CleanOrderData();
        }

        /// <summary>
        /// 初始化值订单界面的值
        /// </summary>
        private void CleanOrderData()
        {
             

            view.CanEditOrder = true;
        }

        /// <summary>
        /// 从草稿订单创建正式订单
        /// </summary>
        void view_CreateOrder()
        {
            
            Debug.Assert(ClientState.CurrentServiceOrder.OrderStatus == Model.Enums.enum_OrderStatus.Draft, "orderStatus is not valid,orderStatus="+ ClientState.CurrentServiceOrder.OrderStatus);
            SaveCurrentOrder(); //从草稿单创建正式订单
           
            ClientState.CurrentServiceOrder.CreatedFromDraft();

            View_NoticeOrder();
            BLLPayment bllPayment = new BLLPayment();

            Payment payment = bllPayment.ApplyPay(ClientState.CurrentServiceOrder, Model.Enums.enum_PayTarget.Deposit);
                
              //  ClientState.CurrentServiceOrder.BuildPayLink(Dianzhu.Config.Config.GetAppSetting("PayUrl"),Model.Enums.enum_PayTarget.Deposit);

            ReceptionChatNotice chatNotice = new ReceptionChatNotice
            {
                ChatType = Model.Enums.enum_ChatType.Notice,
                From = ClientState.customerService,
                To = ClientState.CurrentServiceOrder.Customer,
                SavedTime = DateTime.Now,
                ServiceOrder = ClientState.CurrentServiceOrder,
                UserObj = ClientState.customerService,
                MessageBody = "paymentId=" + payment.Id,
                SendTime = DateTime.Now
            };

            SendMessage(chatNotice);
            LoadCurrentOrder(ClientState.CurrentServiceOrder);
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
            //view.SetCustomerButtonStyle(CurrentServiceOrder, em_ButtonStyle.Readed);
            //view.SetCustomerButtonStyle(ClientState.CurrentCustomer, em_ButtonStyle.Readed);
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

            if (!ClientState.OrderList.Contains(order))
            {
                ClientState.OrderList.Add(order);
            }
            
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

            if (ClientState.CurrentServiceOrder == null)
            {
                return;
            }

             
            ClientState.CurrentServiceOrder.DepositAmount = view.OrderDepositAmount;// == "" ? 0 : Convert.ToDecimal(view.ServiceDepositAmount);

            ClientState.CurrentServiceOrder.Memo = view.Memo;
            bllOrder.SaveOrUpdate(ClientState.CurrentServiceOrder);
        }


        /// <summary>
        /// 界面的搜索事件
        /// </summary>
        void view_SearchService()
        {
            //int total;
            //var serviceList = bllService.Search(view.SerachKeyword, 0, 10, out total);
            //view.SearchedService = serviceList;

            //string pushServiceKey = ClientState.CurrentServiceOrder == null ? "dianzhucs" :
            //    ClientState.CurrentServiceOrder.Id.ToString();
            //if (ClientState.SearchResultForCustomer.ContainsKey(pushServiceKey))
            //{
            //    ClientState.SearchResultForCustomer[pushServiceKey] = serviceList;
            //}
            //else
            //{
            //    ClientState.SearchResultForCustomer.Add(pushServiceKey, serviceList);
            //}
        }


        /// <summary>
        /// 发送消息~
        /// </summary>
        void view_SendMessageHandler()
        {
            if (ClientState.CurrentServiceOrder == null)
            { return; }

            if (string.IsNullOrEmpty(view.MessageTextBox.Trim())) return;

            ReceptionChat chat = new ReceptionChat
            {
                ChatType = Model.Enums.enum_ChatType.Text,
                From = ClientState.customerService,
                To = ClientState.CurrentServiceOrder.Customer,
                MessageBody = view.MessageTextBox,
                SendTime = DateTime.Now,
                SavedTime = DateTime.Now,
                ServiceOrder = ClientState.CurrentServiceOrder
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
            if (ClientState.CurrentServiceOrder == null) return;
 
            string s = Convert.ToBase64String(fileData);
            string fileName = MediaServer.HttpUploader.Upload(GlobalViables.MediaUploadUrl, s, domainType, mediaType);

            ReceptionChatMedia chat = new ReceptionChatMedia
            {
                ServiceOrder = ClientState.CurrentServiceOrder,
                ChatType = Model.Enums.enum_ChatType.Media,
                From = ClientState.customerService,
                To = ClientState.CurrentServiceOrder.Customer,
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

