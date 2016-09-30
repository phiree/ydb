using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.ViewModel
{
    public class VMOrderHistory
    {
        public VMOrderHistory(Guid orderId,decimal orderAmount,string businessName,string serviceName,string targetAddress,string orderStatusStr,DateTime startTime,DateTime endTime)
        {
            this.OrderId = orderId;
            this.OrderAmount = orderAmount;
            this.BusinessName = businessName;
            this.ServiceName = serviceName;
            this.TargetAddress = targetAddress;
            this.OrderStatusStr = orderStatusStr;
            this.StartTime = startTime;
            this.EntTime = endTime;
        }

        public Guid OrderId { get; protected internal set; }
        public decimal OrderAmount { get; protected internal set; }
        public string BusinessName { get; protected internal set; }
        public string ServiceName { get; protected internal set; }
        public string TargetAddress { get; protected internal set; }
        public string OrderStatusStr { get; protected internal set; }
        public DateTime StartTime { get; protected internal set; }
        public DateTime EntTime { get; protected internal set; }
    }
}
