using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Pay
{
    public class PayService:IPayService
    {
        BLL.IBLLServiceOrder bllOrder;
        BLL.BLLPayment bllPayment;
        public PayService(BLL.BLLPayment bllPayment, BLL.IBLLServiceOrder bllOrder)
        {
            this.bllPayment = bllPayment;
            this.bllOrder = bllOrder;
        }

        /// <summary>
        /// 条件读取支付项
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="payfilter"></param>
        /// <returns></returns>
        public IList<payObj> GetPays(string orderID, common_Trait_Filtering filter, common_Trait_PayFiltering payfilter)
        {
            IList<Model.Payment> payment = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "Payment");
            payment = bllPayment.GetPays(filter1, payfilter.payStatus, payfilter.payType, utils.CheckGuidID(orderID, "orderID"));
            if (payment == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<payObj> payobj = Mapper.Map<IList<Model.Payment>, IList<payObj>>(payment);
            return payobj;
        }

        /// <summary>
        /// 统计支付项的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payfilter"></param>
        /// <returns></returns>
        public countObj GetPaysCount(string orderID, common_Trait_PayFiltering payfilter)
        {
            countObj c = new countObj();
            c.count = bllPayment.GetPaysCount(payfilter.payStatus, payfilter.payType, utils.CheckGuidID(orderID, "orderID")).ToString();
            return c;
        }

        /// <summary>
        /// 读取支付项 根据ID
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <returns></returns>
        public payObj GetPay(string orderID, string payID)
        {
            Model.Payment payment = null;
            payment = bllPayment.GetPay(utils.CheckGuidID(orderID, "orderID"), utils.CheckGuidID(payID, "payID"));
            payObj payobj = Mapper.Map<Model.Payment, payObj>(payment);
            return payobj;
        }

        /// <summary>
        /// 更新支付信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="payobj"></param>
        /// <returns></returns>
        public payObj PatchPay(string orderID, string payID, payObj payobj)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Guid guidPay = utils.CheckGuidID(payID, "payID");
            Model.Payment payment = bllPayment.GetPay(guidOrder, guidPay);
            if (payment == null)
            {
                throw new Exception("该笔支付不存在！");
            }

            Model.Payment payment1 = new Model.Payment();
            payment1.Id = payment1.Id;
            payment1.Order = payment1.Order;
            payment1.PayTarget = payment1.PayTarget;
            payment1.Amount = payment1.Amount;
            payment1.PayApi = payment1.PayApi;
            payment1.CreatedTime = payment1.CreatedTime;
            payment1.PlatformTradeNo = payment1.PlatformTradeNo;
            payment1.LastUpdateTime = payment1.LastUpdateTime;
            payment1.Status = payment1.Status;
            payment1.Memo = payment1.Memo;


            Model.Payment payment2 = Mapper.Map<payObj, Model.Payment>(payobj);
            if (payobj.payStatus != null && payobj.payStatus != "")
            {
                payment1.Status = payment2.Status;
            }
            if (payobj.type != null && payobj.type != "")
            {
                payment1.PayTarget = payment2.PayTarget;
            }
            DateTime dt = DateTime.Now;
            payment1.LastUpdateTime = dt;
            bllPayment.Update(payment1);
            payment2 = bllPayment.GetOne(payment1.Id);


            if (payment2 != null && payment2.LastUpdateTime==dt)
            {
                payobj = Mapper.Map<Model.Payment, payObj>(payment2);
            }
            else
            {
                throw new Exception("更新失败");
            }
            return payobj;
        }
    }
}
