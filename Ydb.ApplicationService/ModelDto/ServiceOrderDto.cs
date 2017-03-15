using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.ApplicationService.ModelDto
{
    public class ServiceOrderDto
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceBusinessName { get; set; }
        public string TargetAddress { get; set; }
        public string TargetCustomerName { get; set; }
        public DateTime OrderConfirmTime { get; set; }
        public string OrderStatus { get; set; }
        public string StaffName { get; set; }
    }
}
