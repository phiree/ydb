using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Diagnostics;
using Ydb.Common;

namespace Dianzhu.Model
{
    /// <summary>
    /// 退款项. 每个订单可能有多次退款项
    /// </summary>
    public class Refund:DDDCommon.Domain.Entity<Guid>
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model");

        protected Refund()
        {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="payment">支付项</param>
        /// <param name="totalAmount">支付总额</param>
        /// <param name="refundAmount">退款总额</param>
        /// <param name="refundReason">退款原因</param>
        /// <param name="platformTradeNo">平台交易号</param>
        /// <param name="refundStatus">支付状态</param>
        /// <param name="memo">备注</param>
        public Refund(ServiceOrder order,Payment payment,decimal totalAmount,decimal refundAmount,string refundReason,string platformTradeNo, enum_RefundStatus refundStatus,string memo)
        {
            this.CreatedTime =this.LastUpdateTime = DateTime.Now;
            this.Order = order;
            this.Payment = payment;
            this.TotalAmount = totalAmount;
            this.RefundAmount = refundAmount;
            this.RefundReason = refundReason;
            this.PlatformTradeNo = platformTradeNo;
            this.RefundStatus = refundStatus;
            this.Memo = memo;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        public virtual ServiceOrder Order { get; set; }

        /// <summary>
        /// 支付项
        /// </summary>
        public virtual Payment Payment { get; set; }

        /// <summary>
        /// 支付总额
        /// </summary>
        public virtual decimal TotalAmount { get; set; }

        /// <summary>
        /// 本次退款总额
        /// </summary>
        public virtual decimal RefundAmount { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public virtual string RefundReason { get; set; }

        /// <summary>
        /// 平台交易号,如果是在线支付，则由支付平台返回
        /// </summary>
        public virtual string PlatformTradeNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; }

        /// <summary>
        ///  最后操作时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public virtual enum_RefundStatus RefundStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }


        /// <summary>
        /// 請求的
        /// </summary>
        /// <returns></returns>
        public bool RequestRefund()
        {
            try
            {
                log.Debug("支付宝退款开始");
               //    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

                string refund_no = DateTime.Now.ToString("yyyyMMdd") + refundAliApp.Id.ToString().Substring(0, 10);

                IRefund iRefundAliApp = new RefundAliApp(Dianzhu.Config.Config.GetAppSetting("PaySite") + "RefundCallBack/Alipay/notify_url.aspx", refund_no, refundAliApp.RefundAmount, refundAliApp.PlatformTradeNo, refundAliApp.Payment.Id.ToString(), string.Empty);
                var respDataAliApp = iRefundAliApp.CreateRefundRequest();

                string respDataStrAliApp = string.Empty;
                foreach (string key in respDataAliApp)
                {
                    respDataStrAliApp += key + "=" + respDataAliApp[key] + "&";
                }
                respDataStrAliApp = respDataStrAliApp.TrimEnd('&');
                log.Debug("支付宝退款请求参数:" + respDataStrAliApp);

                #region 保存退款请求数据
                BLLRefundLog bllRefundLogAliApp = new BLLRefundLog();
                RefundLog refundLogAliApp = new RefundLog(respDataStrAliApp, refundAliApp.Id, refundAliApp.RefundAmount, enum_PaylogType.ResultNotifyFromAli, enum_PayType.Online);
                bllRefundLogAliApp.Save(refundLogAliApp);
                #endregion

                string url_AliApp = "https://mapi.alipay.com/gateway.do";
                string returnstrAliApp = HttpHelper.CreateHttpRequest(url_AliApp, "post", respDataAliApp, Encoding.Default);
                log.Debug("支付宝返回数据:" + returnstrAliApp);

                #region 保存退款返回数据，这里是同步数据，异步数据的在notify中处理
                refundLogAliApp = new RefundLog(returnstrAliApp, refundAliApp.Id, refundAliApp.RefundAmount, enum_PaylogType.ResultReturnFromAli, enum_PayType.Online);
                bllRefundLogAliApp.Save(refundLogAliApp);
                #endregion

                string jsonAliApp = JsonHelper.Xml2Json(returnstrAliApp, true);
                RefundReturnAliApp refundReturnAliApp = JsonConvert.DeserializeObject<RefundReturnAliApp>(jsonAliApp);

                if (refundReturnAliApp.is_success.ToUpper() == "T")
                {
                    log.Debug("支付宝返回成功");
                    isRefund = true;


                    log.Debug("更新支付宝退款记录");
                    refundAliApp.RefundStatus = enum_RefundStatus.Success;
                    bllRefund.Update(refundAliApp);
                }
                else
                {
                    log.Error("错误提示:" + refundReturnAliApp.error);
                    isRefund = false;
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }


    }



}
