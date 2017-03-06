using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.ApplicationService.ModelDto
{
    public class FinanceWithdrawDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 申请提现用户账户ID
        /// </summary>
        public string ApplyUserId { get; set; }

        /// <summary>
        /// 申请提现金额
        /// </summary>
        public decimal ApplyAmount { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 收款账户
        /// </summary>
        public string ReceiveAccount { get; set; }

        /// <summary>
        /// 实际转账金额
        /// </summary>
        public decimal TransferAmount { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal ServiceFee { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public string ApplyStatus { get; set; }

        /// <summary>
        /// 申请备注
        /// </summary>
        public string ApplyRemark { get; set; }

        /// <summary>
        /// 手续费率
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// 付款人
        /// </summary>
        public string PayUserId { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 付款回调状态
        /// </summary>
        public string PayStatus { get; set; }

        /// <summary>
        /// 付款备注
        /// </summary>
        public string PayRemark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// 申请单流水编号
        /// </summary>
        public string ApplySerialNo { get; set; }


        /// <summary>
        /// 支付批次编号
        /// </summary>
        public string PaySerialNo { get; set; }

        /// <summary>
        /// 第三方账单号
        /// </summary>
        public string D3SerialNo { get; set; }


        /// <summary>
        /// 第三方完成时间
        /// </summary>
        public string D3Time { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickName { get; set; }
    }
}
