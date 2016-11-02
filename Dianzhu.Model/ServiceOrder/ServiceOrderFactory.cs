using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceOrderFactory
    {
        public static ServiceOrder CreateDraft(DZMembership customerService,DZMembership customer)
        {
            ServiceOrder order = new Model.ServiceOrder { CustomerServiceId=customerService.Id.ToString(),CustomerId= customer.Id.ToString()};
            return order;
            
        }
    }
}
