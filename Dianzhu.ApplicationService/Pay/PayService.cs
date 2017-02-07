using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using PHSuit;
using Newtonsoft.Json;
using Ydb.Common.Specification;
using Ydb.Common;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.PayGateway;
using Ydb.PayGateway.Application;
using Ydb.PayGateway.DomainModel;
using Ydb.PayGateway.DomainModel.Pay;

namespace Dianzhu.ApplicationService.Pay
{
    public class PayService:IPayService
    {
       
        IServiceOrderService orderService;
        IPaymentService paymentService;

        IPaymentLogService paymentLogService;
       
        public PayService(IServiceOrderService orderService,
        IPaymentService paymentService
            , IPaymentLogService paymentLogService)
        {
            this. orderService=orderService;
            this.paymentService=paymentService;
            this.paymentLogService = paymentLogService;
        }

        /// <summary>
        /// 条件读取支付项
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="payfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<payObj> GetPays(string orderID, common_Trait_Filtering filter, common_Trait_PayFiltering payfilter,Customer customer)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("orderID不能为空！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
             ServiceOrder order = orderService.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            IList< Payment> payment = null;
            TraitFilter filter1 = utils.CheckFilter(filter, "Payment");
            payment = paymentService.GetPays(filter1, payfilter.payStatus, payfilter.payType, guidOrder, utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (payment == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<payObj>();
            }
            IList<payObj> payobj = Mapper.Map<IList<Payment>, IList<payObj>>(payment);
            return payobj;
        }

        /// <summary>
        /// 统计支付项的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public countObj GetPaysCount(string orderID, common_Trait_PayFiltering payfilter, Customer customer)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("orderID不能为空！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
         ServiceOrder order = orderService.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            countObj c = new countObj();
            c.count = paymentService.GetPaysCount(payfilter.payStatus, payfilter.payType, guidOrder, utils.CheckGuidID(customer.UserID, "customer.UserID")).ToString();
            return c;
        }

        /// <summary>
        /// 读取支付项 根据ID
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public payObj GetPay(string orderID, string payID, Customer customer)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("orderID不能为空！");
            }
            if (string.IsNullOrEmpty(payID))
            {
                throw new FormatException("payID不能为空！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
         ServiceOrder order = orderService.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
         Payment payment =  paymentService.GetPay(guidOrder, utils.CheckGuidID(payID, "payID"), utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (payment == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                //return null;
                throw new Exception("没有找到资源！");
            }
            payObj payobj = Mapper.Map<Payment, payObj>(payment);
            return payobj;
        }

        /// <summary>
        /// 更新支付信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="payobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public payObj PatchPay(string orderID, string payID, payObj payobj,Customer customer)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("orderID不能为空！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
         ServiceOrder order = orderService.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (order.OrderStatus !=  enum_OrderStatus.Ended)
            {
                throw new Exception("该订单不是支付尾款的状态！");
            }
            Guid guidPay = utils.CheckGuidID(payID, "payID");
         Payment payment = paymentService.GetPay(guidOrder, guidPay, utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (payment == null)
            {
                throw new Exception("该笔支付不存在！");
            }
            if (payment.PayTarget != enum_PayTarget.FinalPayment)
            {
                throw new Exception("该笔支付类型不是尾款！");
            }
            if (payment.Status != enum_PaymentStatus.Wait_Buyer_Pay)
            {
                throw new Exception("该笔支付不是待支付状态！");
            }
            //中能从线上改为到线下支付，无法改回
            //if (!string.IsNullOrEmpty(payobj.payStatus))
            //{
            //    payment.Status = (enum_PaymentStatus)Enum.Parse(typeof(enum_PaymentStatus), payobj.payStatus);
            //}
            //if (!string.IsNullOrEmpty(payobj.type))
            //{
            //    payment.PayTarget = (enum_PayTarget)Enum.Parse(typeof(enum_PayTarget), payobj.type);
            //}
            //payment.PayType = payobj.bOnline ?enum_PayType.Online :enum_PayType.Offline;
            //保存记录

            orderService.OrderFlow_OrderFinished(order);

            //Payment payment = bllPayment.ApplyPay(order, Enums.enum_PayTarget.FinalPayment);

            //if (payment == null)
            //{
            //    throw new Exception("该笔支付不存在！");
            //}


            PaymentLog paymentLog = new PaymentLog();
            paymentLog.PaylogType =  enum_PaylogType.None;
            paymentLog.PayType =  enum_PayType.Offline;
            paymentLog.PayAmount = payment.Amount;
            paymentLog.PaymentId = payment.Id;
            paymentLogService.Save(paymentLog);

            //更新支付项              
            payment.Status = enum_PaymentStatus.Trade_Success;
            payment.PayType = enum_PayType.Offline;
            payment.PayApi =  enum_PayAPI.None;
            payment.Memo = "线下支付";

            DateTime dt = DateTime.Now;
            payment.LastUpdateTime = dt;
            paymentService.Update(payment);
            //bllPayment.Update(payment1);
            //payment2 = bllPayment.GetOne(payment1.Id);


            //if (payment2 != null && payment2.LastUpdateTime==dt)
            //{
            payobj = Mapper.Map<Payment, payObj>(payment);
            //}
            //else
            //{
            //    throw new Exception("更新失败");
            //}
            return payobj;
        }

        /// <summary>
        /// 获得第三方支付字符串
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="payID"></param>
        /// <param name="payTarget"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public pay3rdStringObj GetPay3rdString(string orderID, string payID, string payTarget,Customer customer)
        {

            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
         ServiceOrder order = orderService.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            Guid guidPay = utils.CheckGuidID(payID, "payID");
         Payment payment = paymentService.GetPay(guidOrder, guidPay, utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (payment == null)
            {
                throw new Exception("该笔支付不存在！");
            }
            if (payment.Status != enum_PaymentStatus.Wait_Buyer_Pay)
            {
                throw new Exception("该笔支付不是待支付状态！");
            }
            pay3rdStringObj c = new pay3rdStringObj();
            switch (payTarget.ToLower())
            {
                case "alipay":
                    // http://119.29.39.211:8168
                    IPayRequest ipayAli = new PayAlipayApp(payment.Amount, payment.Id.ToString(), payment.Order.Title, Dianzhu.Config.Config.GetAppSetting("PaySite") + "PayCallBack/Alipay/notify_url.aspx", payment.Order.Description);
                    c.pay3rdString = ipayAli.CreatePayRequest();
                    return c;
                case "wepay":
                    for (int i = 0; i < 10; i++)
                    {
                        IPayRequest ipayWe = new PayWeChat(payment.Amount, payment.Id.ToString(), payment.Order.Title
                            , Dianzhu.Config.Config.GetAppSetting("PaySite") + "PayCallBack/Wepay/notify_url.aspx", payment.Order.Description);
                        //var respDataWeibo = new NameValueCollection();
                        string respDataWechat = "<xml>";

                        string[] arrPropertyValues = ipayWe.CreatePayRequest().Split('&');
                        foreach (string value in arrPropertyValues)
                        {
                            string[] arrValue = value.Split('=');
                            //respDataWeibo.Add(arrValue[0], arrValue[1]);
                            if (arrValue[0] != "key")
                            {
                                respDataWechat += "<" + arrValue[0] + ">" + arrValue[1] + "</" + arrValue[0] + ">";
                            }
                        }
                        respDataWechat = respDataWechat + "</xml>";
                        //ilog.Debug(respDataWechat);
                        //XmlDocument respDataWechatXml = JsonConvert.DeserializeXmlNode(respDataWechat);

                        string requestUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder?";
                        string resultTokenWechat = HttpHelper.CreateHttpRequestPostXml(requestUrl, respDataWechat);
                        //ilog.Debug(resultTokenWechat);
                        resultTokenWechat = resultTokenWechat.Replace("\\n", string.Empty);
                        //ilog.Debug(resultTokenWechat);
                        string json = JsonHelper.Xml2Json(resultTokenWechat, true);
                        //ilog.Debug(json);

                        //todo:下面这段数据就是返回的字符串，不知道怎么解析
                        //resultTokenWechat="<xml>< return_code >< ![CDATA[SUCCESS]] ></ return_code >\n      < return_msg >< ![CDATA[OK]] ></ return_msg >\n            < appid >< ![CDATA[wxd928d1f351b77449]] ></ appid >\n                  < mch_id >< ![CDATA[1304996701]] ></ mch_id >\n                        < nonce_str >< ![CDATA[RnTyNTtoDpMC335q]] ></ nonce_str >\n                              < sign >< ![CDATA[8440115DC99103B7B242042239395967]] ></ sign >\n                                    < result_code >< ![CDATA[SUCCESS]] ></ result_code >\n                                          < prepay_id >< ![CDATA[wx201602031137215b5350a5300317902456]] ></ prepay_id >\n                                                < trade_type >< ![CDATA[APP]] ></ trade_type >\n                                                      </ xml > ";

                        WePayChatUserObj respData = JsonConvert.DeserializeObject<WePayChatUserObj>(json);
                        //ilog.Debug(respData.return_code);
                        if (respData.return_code != "SUCCESS")
                        {
                            continue;
                        }

                        string orderString = ipayWe.CreatePayStr(respData.prepay_id);
                        c.pay3rdString = orderString;
                        return c;
                    }
                    throw new Exception("生成失败！");
                default:
                    throw new Exception("不支持传入的支付类型！"); ;
            }
        }
    }
}
