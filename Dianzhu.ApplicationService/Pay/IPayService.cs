using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Pay
{
    public interface IPayService
    {
        /// <summary>
        /// 条件读取支付项
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="payfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        IList<payObj> GetPays(string orderID, common_Trait_Filtering filter, common_Trait_PayFiltering payfilter, Customer customer);

        /// <summary>
        /// 统计支付项的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        countObj GetPaysCount(string orderID, common_Trait_PayFiltering payfilter, Customer customer);

        /// <summary>
        /// 读取支付项 根据ID
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        payObj GetPay(string orderID, string payID, Customer customer);

        /// <summary>
        /// 更新支付信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="payobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        payObj PatchPay(string orderID, string payID, payObj payobj, Customer customer);

        /// <summary>
        /// 获得第三方支付字符串
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="payTarget"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        countObj GetPay3rdString(string orderID, string payID, string payTarget, Customer customer);
    }
}
