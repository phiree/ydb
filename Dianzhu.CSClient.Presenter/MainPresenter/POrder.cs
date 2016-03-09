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
        BLL.BLLPayment bllPayment;
        public POrder(IViewOrder iViewOrder, DAL.DALServiceOrderStateChangeHis dalHistory,BLL.BLLPayment bllPayment)
        {
            this.iViewOrder = iViewOrder;
            iViewOrder.CreateOrderClick += IViewOrder_CreateOrderClick;
            this.dalHistory = dalHistory;
            this.bllPayment = bllPayment;
        }
        public POrder(IViewOrder iViewOrder):this(iViewOrder,new DAL.DALServiceOrderStateChangeHis(),new BLL.BLLPayment()){}

        private void IViewOrder_CreateOrderClick()
        {
            ServiceOrder currentOrder = IdentityManager.CurrentIdentity;
            Debug.Assert(currentOrder.OrderStatus == Model.Enums.enum_OrderStatus.Draft, "orderStatus is not valid,orderStatus=" + currentOrder.OrderStatus);
            currentOrder.CreatedFromDraft();

            ServiceOrderStateChangeHis orderHis = new ServiceOrderStateChangeHis
            {
                OrderAmount = currentOrder.OrderAmount,
                DepositAmount = currentOrder.DepositAmount,
                NegotiateAmount = currentOrder.NegotiateAmount,
                Order = currentOrder,
                Remark = currentOrder.Memo,
                OldStatus = Model.Enums.enum_OrderStatus.Draft,
                NewStatus = currentOrder.OrderStatus,
                Number = 1,
            };
            dalHistory.SaveOrUpdate(orderHis);

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
