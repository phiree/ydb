using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.ApplicationService.ModelDto
{
    public class FinanceTotalDto
    {

        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户账户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 用户账户余额
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// 用户账户冻结金额
        /// </summary>
        public decimal Frozen { get; set; }


        /// <summary>
        /// 用户绑定的收款账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        ///  联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否是代理的助理
        /// </summary>
        public bool IsAgentCustomerService { get; set; }

    }
}
