using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceOrderFactory
    {
        public static ServiceOrder CreateDraft(string customerServiceId,string customerId)
        {
            ServiceOrder order = new  ServiceOrder { CustomerServiceId=customerServiceId,CustomerId= customerId};
            return order;
            
        }
    }
}
