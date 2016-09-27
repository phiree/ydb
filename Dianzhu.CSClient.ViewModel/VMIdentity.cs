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

        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAvatarUrl { get; set; }        
    }
}
