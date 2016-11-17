using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.ViewModel;
using Dianzhu.Model;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public class VMOrderHistoryAdapter : IVMOrderHistoryAdapter
    {
        public VMOrderHistory OrderToVMOrderHistory(ServiceOrder order)
        {
            string status = order.GetStatusTitleFriendly(order.OrderStatus);
            string serviceName = order.Details.Count > 0 ? order.Details[0].ServiceSnapShot.ServiceName : string.Empty;

            return new VMOrderHistory(order.Id, order.OrderAmount, order.Business.Name, serviceName, order.TargetAddress, status, order.OrderServerStartTime, order.OrderServerFinishedTime);
        }
    }
}
