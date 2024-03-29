﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.ViewModel
{
    public class VMIdentity
    {
        public VMIdentity(string orderId,string customerId, string customerName,string customerAvatarUrl)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.CustomerName = customerName;
            this.CustomerAvatarUrl = customerAvatarUrl;            
        }

        public string OrderId { get; protected internal set; }
        public string CustomerId { get; protected internal set; }
        public string CustomerName { get; protected internal set; }
        public string CustomerAvatarUrl { get; protected internal set; }        
    }
}
