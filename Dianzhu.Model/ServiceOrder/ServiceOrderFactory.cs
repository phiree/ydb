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
        public static ServiceOrder CreateDraft(string customerServiceId,string customerId)
        {
            ServiceOrder order = new Model.ServiceOrder { CustomerServiceId=customerServiceId,CustomerId= customerId};
            return order;
            
        }
    }
}
