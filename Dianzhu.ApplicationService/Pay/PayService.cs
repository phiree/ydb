﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dianzhu.Pay;
using PHSuit;
using Newtonsoft.Json;

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
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<payObj> GetPays(string orderID, common_Trait_Filtering filter, common_Trait_PayFiltering payfilter,Customer customer)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("orderID不能为空！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order = bllOrder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            IList<Model.Payment> payment = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "Payment");
            payment = bllPayment.GetPays(filter1, payfilter.payStatus, payfilter.payType, guidOrder, utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (payment == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<payObj>();
            }
            IList<payObj> payobj = Mapper.Map<IList<Model.Payment>, IList<payObj>>(payment);
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
            Model.ServiceOrder order = bllOrder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            countObj c = new countObj();
            c.count = bllPayment.GetPaysCount(payfilter.payStatus, payfilter.payType, guidOrder, utils.CheckGuidID(customer.UserID, "customer.UserID")).ToString();
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
            Model.ServiceOrder order = bllOrder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            Model.Payment payment =  bllPayment.GetPay(guidOrder, utils.CheckGuidID(payID, "payID"), utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (payment == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                //return null;
                throw new Exception("没有找到资源！");
            }
            payObj payobj = Mapper.Map<Model.Payment, payObj>(payment);
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
            Model.ServiceOrder order = bllOrder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            Guid guidPay = utils.CheckGuidID(payID, "payID");
            Model.Payment payment = bllPayment.GetPay(guidOrder, guidPay, utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (payment == null)
            {
                throw new Exception("该笔支付不存在！");
            }

            //Model.Payment payment1 = new Model.Payment();
            //payment1.Id = payment1.Id;
            //payment1.Order = payment1.Order;
            //payment1.PayTarget = payment1.PayTarget;
            //payment1.Amount = payment1.Amount;
            //payment1.PayApi = payment1.PayApi;
            //payment1.CreatedTime = payment1.CreatedTime;
            //payment1.PlatformTradeNo = payment1.PlatformTradeNo;
            //payment1.LastUpdateTime = payment1.LastUpdateTime;
            //payment1.Status = payment1.Status;
            //payment1.Memo = payment1.Memo;


            //Model.Payment payment2 = Mapper.Map<payObj, Model.Payment>(payobj);
            if (!string.IsNullOrEmpty(payobj.payStatus))
            {
                payment.Status = (Model.Enums.enum_PaymentStatus)Enum.Parse(typeof(Model.Enums.enum_PaymentStatus), payobj.payStatus);
            }
            if (!string.IsNullOrEmpty(payobj.type))
            {
                payment.PayTarget = (Model.Enums.enum_PayTarget)Enum.Parse(typeof(Model.Enums.enum_PayTarget), payobj.type);
            }
            payment.PayType = payobj.bOnline ? Model.Enums.enum_PayType.Online : Model.Enums.enum_PayType.Offline;
            DateTime dt = DateTime.Now;
            payment.LastUpdateTime = dt;
            //bllPayment.Update(payment1);
            //payment2 = bllPayment.GetOne(payment1.Id);


            //if (payment2 != null && payment2.LastUpdateTime==dt)
            //{
            payobj = Mapper.Map<Model.Payment, payObj>(payment);
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
        public countObj GetPay3rdString(string orderID, string payID, string payTarget,Customer customer)
        {

            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order = bllOrder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            Guid guidPay = utils.CheckGuidID(payID, "payID");
            Model.Payment payment = bllPayment.GetPay(guidOrder, guidPay, utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (payment == null)
            {
                throw new Exception("该笔支付不存在！");
            }
            countObj c = new countObj();
            switch (payTarget.ToLower())
            {
                case "alipay":
                    // http://119.29.39.211:8168
                    IPayRequest ipayAli = new PayAlipayApp(payment.Amount, payment.Id.ToString(), payment.Order.Title, Dianzhu.Config.Config.GetAppSetting("PaySite") + "PayCallBack/Alipay/notify_url.aspx", payment.Order.Description);
                    c.count = ipayAli.CreatePayRequest();
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
                        c.count = orderString;
                        return c;
                    }
                    throw new Exception("生成失败！");
                default:
                    throw new Exception("不支持传入的支付类型！"); ;
            }
        }
    }
}
