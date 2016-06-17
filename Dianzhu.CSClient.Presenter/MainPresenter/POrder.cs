using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IView;
using Dianzhu.Model;
using System.Diagnostics;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// order的控制类
    /// </summary>
  public  class POrder
    {
          
        IViewOrder iViewOrder;
        DAL.DALServiceOrderStateChangeHis dalHistory;
        DAL.DALServiceOrder dalOrder;
        BLL.BLLPayment bllPayment;
        IInstantMessage.InstantMessage iIM;
        public POrder(IInstantMessage.InstantMessage iIM, IViewOrder iViewOrder, DAL.DALServiceOrderStateChangeHis dalHistory, DAL.DALServiceOrder dalOrder, BLL.BLLPayment bllPayment)
        {
            this.iViewOrder = iViewOrder;
            iViewOrder.CreateOrderClick += IViewOrder_CreateOrderClick;
            this.dalHistory = dalHistory;
            this.bllPayment = bllPayment;
            this.dalOrder = dalOrder;
            this.iIM = iIM;
        }

        /// <summary>
        /// 创建订单.
        /// </summary>
        private void IViewOrder_CreateOrderClick()
        {
            ServiceOrder currentOrder = IdentityManager.CurrentIdentity;
          

            Debug.Assert(currentOrder.OrderStatus == Model.Enums.enum_OrderStatus.Draft, "orderStatus is not valid,orderStatus=" + currentOrder.OrderStatus);
            currentOrder.CreatedFromDraft();
            currentOrder.DepositAmount = iViewOrder.DepositAmount;
            currentOrder.Memo = iViewOrder.Memo;
            currentOrder.LatestOrderUpdated = DateTime.Now;
            dalOrder.Update(currentOrder);
            
           // View_NoticeOrder();

            Payment payment = bllPayment.ApplyPay(currentOrder, Model.Enums.enum_PayTarget.Deposit);

            //  ClientState.CurrentServiceOrder.BuildPayLink(Dianzhu.Config.Config.GetAppSetting("PayUrl"),Model.Enums.enum_PayTarget.Deposit);

            ReceptionChatNotice chatNotice = new ReceptionChatNotice
            {
                ChatType = Model.Enums.enum_ChatType.Notice,
                From =GlobalViables.CurrentCustomerService,
                To = currentOrder.Customer,
                SavedTime = DateTime.Now,
                ServiceOrder = currentOrder,
                UserObj = GlobalViables.CurrentCustomerService,
                MessageBody = "支付链接" + bllPayment.BuildPayLink(payment.Id),
                SendTime = DateTime.Now
            };
            iIM.SendMessage(chatNotice);
           // SendMessage(chatNotice);
            //LoadCurrentOrder(ClientState.CurrentServiceOrder);
            iViewOrder.Order = IdentityManager.CurrentIdentity;
        }

        public void LoadOrder(ServiceOrder order)
        {
            iViewOrder.Order = order;
        }

    }
}
