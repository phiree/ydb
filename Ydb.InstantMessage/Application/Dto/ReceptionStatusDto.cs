using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.Application.Dto
{
    public class ReceptionStatusDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 客服
        /// </summary>
        public string CustomerServiceId { get; set; }

        /// <summary>
        /// 最后接待订单
        /// </summary>
        public string OrderId { get; set; }
    }
}
