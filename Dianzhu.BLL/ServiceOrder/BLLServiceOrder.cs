﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dianzhu.Model;

using Dianzhu.DAL;
using Dianzhu.Model.Enums;
using Dianzhu.Pay;
using Dianzhu.Pay.RefundRequest;
using PHSuit;
using Newtonsoft.Json;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;

namespace Dianzhu.BLL
{

    /// <summary>
    /// 订单业务逻辑
    /// </summary>
    public class BLLServiceOrder:IBLLServiceOrder
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");

        IDALServiceOrder repoServiceOrder;
       
        DZMembershipProvider membershipProvider = null;
        BLLPayment bllPayment = null;
        BLLRefund bllRefund = null;
        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis = null;

        public BLLServiceOrder(  BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis, DZMembershipProvider membershipProvider,BLLPayment bllPayment,BLLRefund bllRefund,IDALServiceOrder repoServiceOrder)
        {
            this.repoServiceOrder = repoServiceOrder;
             
            this.bllServiceOrderStateChangeHis = bllServiceOrderStateChangeHis;
            this.membershipProvider = membershipProvider;
            this.bllPayment = bllPayment;
            this.bllRefund = bllRefund;
           
        }

        
      
 
        #region 基本操作
 

        public int GetServiceOrderCount(Guid userId, Dianzhu.Model.Enums.enum_OrderSearchType searchType)
        {

            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.Customer.Id == userId);
            
            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    where = where.And( x => x.OrderStatus == enum_OrderStatus.Finished
                          && x.OrderStatus == enum_OrderStatus.Aborded
                          && x.OrderStatus == enum_OrderStatus.Appraised)
                        ;
                    break;
                case enum_OrderSearchType.Nt:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Aborded
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.Search);
                     
                    break;
                default:
                case enum_OrderSearchType.ALL:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Search)
                      ;
                    break;
            }
            return (int)repoServiceOrder.GetRowCount(where);

           // return DALServiceOrder.GetServiceOrderCount(userId, searchType);
        }
        public IList<ServiceOrder> GetServiceOrderList(Guid userId, Dianzhu.Model.Enums.enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.Customer.Id == userId);

            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    where = where.And(x => x.OrderStatus == enum_OrderStatus.Finished
                         && x.OrderStatus == enum_OrderStatus.Aborded
                         && x.OrderStatus == enum_OrderStatus.Appraised)
                        ;
                    break;
                case enum_OrderSearchType.Nt:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Aborded
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.Search);

                    break;
                default:
                case enum_OrderSearchType.ALL:
                    where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Search)
                      ;
                    break;
            }
            long totalRecord;
            return repoServiceOrder.Find(where, pageNum, pageSize,out totalRecord).ToList();

           // return DALServiceOrder.GetServiceOrderList(userId, searchType, pageNum, pageSize);
        }

        public virtual ServiceOrder GetOne(Guid guid)
        {
            return repoServiceOrder.FindById(guid);
        
            // return DALServiceOrder.GetOne(guid);
        }
        public void SaveOrUpdate(ServiceOrder order)
        {
            repoServiceOrder.Update(order);
         
           // order.LatestOrderUpdated = DateTime.Now;
           // DALServiceOrder.SaveOrUpdate(order);
        }
        public IList<ServiceOrder> GetAll() //获取全部订单
        {
            var where = PredicateBuilder.True<ServiceOrder>();
           return repoServiceOrder.Find(where).ToList();
            
            ///return DALServiceOrder.GetAll<ServiceOrder>();
        }

        public IList<ServiceOrder> GetAllByOrderStatus(Dianzhu.Model.Enums.enum_OrderStatus status)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.OrderStatus == status);
            return repoServiceOrder.Find(where).ToList();
          
            //return DALServiceOrder
            //   .GetAll<ServiceOrder>()
            //   .Where(x => x.OrderStatus == status)
            //   .ToList();
        }

        



        public IList<ServiceOrder> GetListForBusiness(Business business, int pageNum, int pageSize, out int totalAmount)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x =>x.Business==business);
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed);

            long long_totalAmount;
            var result= repoServiceOrder.Find(where,pageNum,pageSize,out long_totalAmount).ToList();
            totalAmount = (int)long_totalAmount;
            return result;
           // return DALServiceOrder.GetAllOrdersForBusiness(business.Id, pageNum, pageSize, out totalAmount);
        }

        public IList<ServiceOrder> GetListForCustomer(DZMembership customer, int pageNum, int pageSize, out int totalAmount)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.Customer == customer);
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed);

            long long_totalAmount;
            var result = repoServiceOrder.Find(where, pageNum, pageSize, out long_totalAmount).ToList();
            totalAmount = (int)long_totalAmount;
            return result;
            //return DALServiceOrder.GetListForCustomer(customer, pageNum, pageSize, out totalAmount);
        }

        public void Delete(ServiceOrder order)
        {
            repoServiceOrder.Delete(order);
           // DALServiceOrder.Delete(order);
        }

        public virtual ServiceOrder GetDraftOrder(DZMembership c, DZMembership cs)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.Customer == c&&x.CustomerService==cs&&x.OrderStatus== enum_OrderStatus.Draft);
            ServiceOrder order = null;
            try
            {
                order = repoServiceOrder.FindOne(where);
               
            }
            catch (Exception ex)
            {
                string errMsg = "错误:用户和客服有多张草稿单!";
                log.Error(errMsg);
                PHSuit.ExceptionLoger.ExceptionLog(log, ex);

            }
            return order;
            
            
           // return DALServiceOrder.GetDraftOrder(c, cs);
        }
        public IList<ServiceOrder> GetOrderListByDate(DZService service, DateTime date)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.Service == service && x.OrderCreated.Date == date.Date);

            return repoServiceOrder.Find(where).ToList();

            //return DALServiceOrder.GetOrderListByDate(service, date);
        }
        public ServiceOrder GetOrderByIdAndCustomer(Guid Id, DZMembership customer)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            where = where.And(x => x.Id==Id && x.Customer ==customer);

            return repoServiceOrder.FindOne(where);

          //  return DALServiceOrder.GetOrderByIdAndCustomer(Id, customer);
        }
        #endregion

        #region 订单流程变化

        /// <summary>
        /// 用户定金支付完成,等待后台确认订单是否到帐
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_PayDepositAndWaiting(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.checkPayWithDeposit);
        }

        /// <summary>
        /// 后台确认订单到帐
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_ConfirmDeposit(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Payed);
        }
        /// <summary>
        /// 商家确认订单,准备执行.
        /// </summary>
        public void OrderFlow_BusinessConfirm(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Negotiate);
        }
        /// <summary>
        /// 商家输入协议
        /// </summary>
        /// <param name="order"></param>
        /// <param name="negotiateAmount"></param>
        public void OrderFlow_BusinessNegotiate(ServiceOrder order, decimal negotiateAmount)
        {
            order.NegotiateAmount = negotiateAmount;
            if (negotiateAmount < order.DepositAmount)
            {
                log.Warn("协商价格小于订金");
            }

            ChangeStatus(order, enum_OrderStatus.isNegotiate);
        }
        /// <summary>
        /// 商户已经提交新价格，等待用户确认
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomConfirmNegotiate(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Assigned);
        }
        /// <summary>
        /// 用户确认协商价格,并确定开始服务
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessStartService(ServiceOrder order)
        {
            order.OrderServerStartTime = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Begin);
        }
        /// <summary>
        /// 商家确定服务完成
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessFinish(ServiceOrder order)
        {
            order.OrderServerFinishedTime = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.isEnd);
        }
        /// <summary>
        /// 用户确认服务完成。
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerFinish(ServiceOrder order)
        {
            order.OrderServerFinishedTime = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Ended);
        }
        /// <summary>
        /// 用户支付尾款
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayFinalPayment(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.checkPayWithNegotiate);
        }

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_OrderFinished(ServiceOrder order)
        {
            order.OrderFinished = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Finished);
        }

        /// <summary>
        /// 订单评价
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerAppraise(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Appraised);
        }

        /// <summary>
        /// 用户申请理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Refund);
            if (true)//todo：是否在质保期，质保期？？
            {
                ChangeStatus(order, enum_OrderStatus.WaitingRefund);
            }
        }

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessIsRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.isRefund);
        }

        /// <summary>
        /// 理赔成功
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_RefundSuccess(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.EndRefund);
        }

        /// <summary>
        /// 商户要求支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessAskPayWithRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.AskPayWithRefund);
        }

        /// <summary>
        /// 商户驳回理赔请求
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessRejectRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.RejectRefund);
        }

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_WaitingPayWithRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.WaitingPayWithRefund);
        }

        /// <summary>
        /// 用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.checkPayWithRefund);
        }

        /// <summary>
        /// 一点办官方介入
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBInsertIntervention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.InsertIntervention);
        }

        /// <summary>
        /// 等待一点办官方处理
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBHandleWithIntervention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.HandleWithIntervention);
        }

        /// <summary>
        /// 一点办已确认理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBConfirmNeedRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.NeedRefundWithIntervention);
            //todo:等待退还金额
        }

        /// <summary>
        /// 一点办要求用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_NeedPayInternention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.NeedPayWithIntervention);
        }

        /// <summary>
        /// 用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayInternention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.checkPayWithIntervention);
        }

        /// <summary>
        /// 理赔成功
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_ConfirmInternention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.EndIntervention);
        }

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_ForceStop(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.ForceStop);
        }

        //订单状态改变通用方法
        private void ChangeStatus(ServiceOrder order, enum_OrderStatus targetStatus)
        {
            enum_OrderStatus oldStatus = order.OrderStatus;

            OrderServiceFlow flow = new OrderServiceFlow();
            flow.ChangeStatus(order, targetStatus);

            //保存订单历史记录
            order.OrderStatus = oldStatus;
            bllServiceOrderStateChangeHis.SaveOrUpdate(order, targetStatus);

            //更新订单状态
            order.OrderStatus = targetStatus;
            SaveOrUpdate(order);

            log.Debug("调用IMServer,发送订单状态变更通知");
            System.Net.WebClient wc = new System.Net.WebClient();
            string notifyServer = Dianzhu.Config.Config.GetAppSetting("NotifyServer");
            Uri uri = new Uri(notifyServer + "type=ordernotice&orderId=" + order.Id);
            System.IO.Stream returnData = wc.OpenRead(uri);
            System.IO.StreamReader reader = new System.IO.StreamReader(returnData);
            string result = reader.ReadToEnd();
            log.Debug("发送结果:" + result);
        }
        #endregion

        #region 订单取消
        /// <summary>
        /// 申请取消
        /// </summary>
        /// <param name="order"></param>
        public bool OrderFlow_Canceled(ServiceOrder order)
        {
            log.Debug("---------开始取消订单---------");
            bool isCanceled = false;
            enum_OrderStatus oldStatus = order.OrderStatus;
            log.Debug("当前订单状态:" + oldStatus.ToString());

            //ChangeStatus(order, enum_OrderStatus.Canceled);.
            OrderServiceFlow flow = new OrderServiceFlow();
            flow.ChangeStatus(order, enum_OrderStatus.Canceled);
            log.Debug("订单状态可以改为Cancled");

            switch (oldStatus)
            {
                case enum_OrderStatus.Created:
                    log.Debug("订单为Created，取消成功");
                    ChangeStatus(order, enum_OrderStatus.Canceled);
                    ChangeStatus(order, enum_OrderStatus.EndCancel);
                    isCanceled = true;
                    break;
                case enum_OrderStatus.Payed:
                case enum_OrderStatus.Negotiate:
                case enum_OrderStatus.isNegotiate:
                case enum_OrderStatus.Assigned:
                    log.Debug("订单已支付订金，系统判断是否退还");
                    ////获取确认时间
                    //var negotiateTime = bllServiceOrderStateChangeHis.GetChangeTime(order, enum_OrderStatus.Negotiate);
                    var targetTime = order.Details[0].TargetTime;
                    if (DateTime.Now <= targetTime)
                    {
                        double timeSpan = (targetTime - DateTime.Now).TotalMinutes;
                        //整个取消
                        if (order.ServiceOvertimeForCancel <= timeSpan)
                        {
                            log.Debug("开始退还订金");
                            //todo:退还定金
                            Payment payment = bllPayment.GetPayedForDeposit(order);
                            if (payment == null)
                            {
                                log.Debug("订单" + order.Id + "没有订金支付项!");
                                throw new Exception("订单" + order.Id + "没有订金支付项!");
                            }

                            switch (payment.PayApi)
                            {
                                case enum_PayAPI.Alipay:
                                    log.Debug("支付宝退款开始");
                                    IRefund iRefundAliApp = new RefundAliApp(Dianzhu.Config.Config.GetAppSetting("PaySite") + "RefundCallBack/Alipay/notify_url.aspx", payment.Amount, payment.PlatformTradeNo, payment.Id.ToString(), string.Empty);
                                    var respDataAliApp = iRefundAliApp.CreateRefundRequest();

                                    string url_AliApp = "https://openapi.alipay.com/gateway.do";
                                    string returnstrAliApp = HttpHelper.CreateHttpRequest(url_AliApp, "post", respDataAliApp, Encoding.Default);

                                    RefundReturnAliApp refundReturnAliApp = JsonConvert.DeserializeObject<RefundReturnAliApp>(HttpUtility.UrlDecode(returnstrAliApp, Encoding.UTF8));
                                    string a = Regex.Unescape(returnstrAliApp);

                                    isCanceled = false;

                                    break;
                                case enum_PayAPI.Wechat:
                                    log.Debug("微信退款开始");
                                    Refund refund = new Refund(payment.Order, payment, payment.Amount, payment.Amount, "取消订单退还订金", payment.PlatformTradeNo, enum_RefundStatus.Fail, string.Empty);
                                    bllRefund.Save(refund);

                                    string refundNo = refund.Id.ToString().Replace("-", "");

                                    IRefund iRefundWeChat = new RefundWePay(Dianzhu.Config.Config.GetAppSetting("PaySite") + "RefundCallBack/Wepay/notify_url.aspx", refund.RefundAmount, refundNo, refund.PlatformTradeNo, refund.Payment.Id.ToString(), string.Empty, refund.TotalAmount);
                                    var respDataWeChat = iRefundWeChat.CreateRefundRequest();

                                    string respDataXmlWechat = "<xml>";

                                    string sign = string.Empty;
                                    foreach (string key in respDataWeChat)
                                    {
                                        if (key != "sign")
                                        {
                                            respDataXmlWechat += "<" + key + ">" + respDataWeChat[key] + "</" + key + ">";
                                        }
                                        else
                                        {
                                            sign = respDataWeChat[key];
                                        }
                                    }
                                    respDataXmlWechat += "<sign>" + sign + "</sign>";
                                    respDataXmlWechat = respDataXmlWechat + "</xml>";
                                    log.Debug("请求微信api数据:" + respDataXmlWechat);

                                    #region 保存退款请求数据
                                    BLLRefundLog bllRefundLog = new BLLRefundLog();
                                    RefundLog refundLog = new RefundLog(respDataXmlWechat, refund.Id, refund.RefundAmount, enum_PaylogType.ReturnNotifyFromWePay, enum_PayType.Online);
                                    bllRefundLog.Save(refundLog);
                                    #endregion

                                    string url_WeChat = "https://api.mch.weixin.qq.com/secapi/pay/refund";
                                    string returnstrWeChat = HttpHelper.CreateHttpRequestPostXml(url_WeChat, respDataXmlWechat, "北京集思优科网络科技有限公司");
                                    log.Debug("微信返回数据:" + returnstrWeChat);

                                    string jsonWeChat = JsonHelper.Xml2Json(returnstrWeChat, true);
                                    RefundReturnWeChat refundReturnWeChat = JsonConvert.DeserializeObject<RefundReturnWeChat>(jsonWeChat);

                                    #region 保存退款返回数据
                                    refundLog = new RefundLog(jsonWeChat, refund.Id, refund.RefundAmount, enum_PaylogType.ReturnNotifyFromWePay, enum_PayType.Online);
                                    bllRefundLog.Save(refundLog);
                                    #endregion

                                    if (refundReturnWeChat.return_code.ToUpper() == "SUCCESS")
                                    {
                                        if (refundReturnWeChat.result_code.ToUpper() == "SUCCESS")
                                        {
                                            log.Debug("微信返回退款成功");
                                            log.Debug("更新订单状态");
                                            order.OrderStatus = oldStatus;
                                            ChangeStatus(order, enum_OrderStatus.Canceled);
                                            ChangeStatus(order, enum_OrderStatus.WaitingDepositWithCanceled);
                                            ChangeStatus(order, enum_OrderStatus.EndCancel);

                                            log.Debug("更新退款记录");
                                            refund.RefundStatus = enum_RefundStatus.Success;
                                            bllRefund.Update(refund);

                                            isCanceled = true;
                                        }
                                        else
                                        {
                                            log.Error("err_code:" + refundReturnWeChat.err_code + "err_code_des:" + refundReturnWeChat.err_code_des);
                                            isCanceled = false;
                                        }
                                    }
                                    else
                                    {
                                        log.Error(refundReturnWeChat.return_msg);
                                        isCanceled = false;
                                    }

                                    break;

                                default: break;
                            }
                        }
                        else
                        {
                            log.Debug("取消订单时间不在订单保险时间内，取消成功");
                            //扣除定金，取消成功
                            ChangeStatus(order, enum_OrderStatus.Canceled);
                            ChangeStatus(order, enum_OrderStatus.EndCancel);
                            isCanceled = true;
                        }
                    }
                    else
                    {
                        log.Debug("取消订单时间大于预约时间，取消成功");
                        //扣除定金，取消成功
                        ChangeStatus(order, enum_OrderStatus.Canceled);
                        ChangeStatus(order, enum_OrderStatus.EndCancel);
                        isCanceled = true;
                    }

                    break;


                default: break;
            }
            log.Debug("----------取消订单完成----------");
            return isCanceled;
        }
        #endregion

        #region 分配工作人员
        public void AssignStaff(ServiceOrder order, Staff staff)
        {
            BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();
            OrderAssignment oa = new OrderAssignment();
            oa.Order = order;
            oa.AssignedStaff = staff;

            bllOrderAssignment.SaveOrUpdate(oa);
        }
        public void DeassignStaff(ServiceOrder order, Staff staff)
        {
            BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();
            OrderAssignment oa = bllOrderAssignment.FindByOrderAndStaff(order, staff);
            oa.DeAssignedTime = DateTime.Now;
            oa.Enabled = false;

            bllOrderAssignment.SaveOrUpdate(oa);
        }
        #endregion

        #region http接口方法



        #endregion


        public int GetServiceOrderCountWithoutDraft(Guid userid, bool isCustomerService)
        {
            var where = PredicateBuilder.True<ServiceOrder>();
            if (isCustomerService)
            {
                where = where.And(x => x.CustomerService.Id == userid);
            }
            else
            {
                where = where.And(x => x.Customer.Id == userid);
            }
            where = where.And(x =>x.OrderStatus!= enum_OrderStatus.Draft&& x.OrderStatus!= enum_OrderStatus.DraftPushed);

            return (int) repoServiceOrder.GetRowCount(where);

           // return DALServiceOrder.GetServiceOrderCountWithoutDraft(userid, isCustomerService);
        }
        public decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService)
        {

            var where = PredicateBuilder.True<ServiceOrder>();
            if (isCustomerService)
            {
                where = where.And(x => x.CustomerService.Id == userid);
            }
            else
            {
                where = where.And(x => x.Customer.Id == userid);
            }
            where = where.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed);

            var list = repoServiceOrder.Find(where).ToList();
            
            return list.Sum(x => x.DepositAmount);
         //   return DALServiceOrder.GetServiceOrderAmountWithoutDraft(userid, isCustomerService);
        }
      
        //查询店铺的所有订单
        public IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId)
        {
            var where = PredicateBuilder.True<ServiceOrder>()
                .And(x=>x.Business.Id==businessId);

            return repoServiceOrder.Find(where).ToList();
         //   return DALServiceOrder.GetAllOrdersForBusiness(businessId);
        }
        //查询全部已经完成的订单
        public IList<ServiceOrder> GetAllCompleteOrdersForBusiness(Guid businessId)
        {
            var where = PredicateBuilder.True<ServiceOrder>()
                .And(x => x.Business.Id == businessId)
                .And(x=>x.OrderStatus== enum_OrderStatus.Finished|| x.OrderStatus== enum_OrderStatus.Appraised)
                ;
            

            return repoServiceOrder.Find(where).ToList();

           // return DALServiceOrder.GetAllCompleteOrdersForBusiness(businessId);
        }
        //查询订单的总金额
        //查询订单的曝光率.
    }
    /// <summary>
    /// 支付宝退款返回数据
    /// </summary>
    public class RefundReturnAliApp
    {
        public RefundReturnResposeAliApp alipay_trade_refund_response { get; set; }
        public string sign { get; set; }
    }
    /// <summary>
    /// 支付宝退款返回数据中的对象
    /// </summary>
    public class RefundReturnResposeAliApp
    {
        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 买家支付宝用户号，该参数已废弃，请不要使用
        /// </summary>
        public string open_id { get; set; }
        /// <summary>
        /// 用户的登录id
        /// </summary>
        public string buyer_logon_id { get; set; }
        /// <summary>
        /// 本次退款是否发生了资金变化
        /// </summary>
        public string fund_change { get; set; }
        /// <summary>
        /// 本次发生的退款金额
        /// </summary>
        public string refund_fee { get; set; }
        /// <summary>
        /// 退款支付时间
        /// </summary>
        public string gmt_refund_pay { get; set; }
        /// <summary>
        /// 用户的登录id
        /// </summary>
        public RefundDetailItemListAliApp refund_detail_item_list { get; set; }
        /// <summary>
        /// 交易在支付时候的门店名称
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 买家在支付宝的用户id
        /// </summary>
        public string buyer_user_id { get; set; }
        /// <summary>
        /// 实际退回给用户的金额
        /// </summary>
        public string send_back_fee { get; set; }
        /// <summary>
        /// 返回码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string sub_code { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string sub_msg { get; set; }
    }
    /// <summary>
    /// 微信退款放回数据
    /// </summary>
    public class RefundReturnWeChat
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string device_info { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }
        public string transaction_id { get; set; }
        public string out_trade_no { get; set; }
        public string out_refund_no { get; set; }
        public string refund_id { get; set; }
        public string refund_channel { get; set; }
        public string refund_fee { get; set; }
        public string total_fee { get; set; }
        public string fee_type { get; set; }
        public string cash_fee { get; set; }
        public string cash_refund_fee { get; set; }
        public string coupon_refund_fee { get; set; }
        public string coupon_refund_count { get; set; }
        public string coupon_refund_id { get; set; }
    }

    /// <summary>
    /// 退款返回的资金明细类型
    /// </summary>
    public class RefundDetailItemListAliApp
    {
        /// <summary>
        /// 支付所使用的渠道
        /// </summary>
        public string fund_channel { get; set; }
        /// <summary>
        /// 该支付工具类型所使用的金额
        /// </summary>
        public string amount { get; set; }
    }


}
