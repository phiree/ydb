using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 支付项模块 业务业务层
    /// </summary>
    public class BLLPayment
    {
        DAL.DALPayment dal = new DAL.DALPayment();
         
        /// <summary>
        /// 订单申请支付,
        ///<returns>支付链接</returns>
        /// <param name="order">申请支付的订单</param>
        /// <param name="payTarget">支付目标</param>
        public string ApplyPay(ServiceOrder order, enum_PayTarget payTarget)
        {
            //获取该订单已经申请过的项目.

            //验证该支付申请是否有效. 
            //无效: 同类型的支付申请已经创建, 直接返回该支付链接. 当前支付金额

            IList<Payment> payments = dal.GetPaymentsForOrder(order);
            return string.Empty;
        }
    }
}
