using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.ViewModel
{
    public class VMIdentity
    {
        public VMIdentity(Guid orderId,Guid customerId, string customerName,string customerAvatarUrl)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.CustomerName = customerName;
            this.CustomerAvatarUrl = customerAvatarUrl;            
        }

        public Guid OrderId { get; protected internal set; }
        public Guid CustomerId { get; protected internal set; }
        public string CustomerName { get; protected internal set; }
        public string CustomerAvatarUrl { get; protected internal set; }        
    }
}
