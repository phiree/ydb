using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.ApplicationService.ModelDto
{
    public class FinanceFlowDto
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
        /// 用户昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 本次发生金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccurTime { get; set; }

        /// <summary>
        /// 关联对象的Id, 比如 订单, 提现单,充值单..等等.
        /// </summary>
        public string RelatedObjectId { get; set; }

        /// <summary>
        /// 关联对象的流水编号
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 发生类型
        /// </summary>
        public string FlowType { get; set; }

        /// <summary>
        /// true为收入，false为支出
        /// </summary>
        public bool Income { get; set; }

        /// <summary>
        /// true为收入，false为支出
        /// </summary>
        public string AmountTotal { get; set; }

        /// <summary>
        /// true为收入，false为支出
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// 本次发生金额显示字符串
        /// </summary>
        public virtual string AmountView { get; set; }
    }
}
