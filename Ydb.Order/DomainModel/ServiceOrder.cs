using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common;
using System.Diagnostics;
using Ydb.Common.Domain;
using Ydb.Order.DomainModel.Repository;

namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 订单
    /// todo:refactor:和服务相关的信息不需要浮现到订单对象中,直接引用 detaiis[0].servicesnapshot.相关属性即可.
    /// </summary>

    public class ServiceOrder : Entity<Guid>
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model");


        public ServiceOrder()
        {
            OrderStatus = enum_OrderStatus.Draft;
            OrderCreated = DateTime.Now;
            LatestOrderUpdated = DateTime.Now;
            IsShared = false;

            Details = new List<ServiceOrderDetail>();

        }
        public virtual string SerialNo { get; set; }
        #region 管理明细
        /// <summary>
        /// 增加一条订单明细
        /// limit: 目前限制 一个订单只有一个服务.
        /// todo: 增加 购物车概念, 通过购物车为不同商家生成不同订单.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="unitAmount"></param>
        /// <param name="targetCustomerName"></param>
        /// <param name="targetCustomerPhone"></param>
        /// <param name="targetAddress"></param>
        /// <param name="targetTime"></param>
        /// <param name="memo"></param>
        public virtual void AddDetailFromIntelService(string serviceId, ServiceSnapShot serviceSnapShot, WorkTimeSnapshot OpenTimeSnapShot,
            // ServiceOpenTimeForDaySnapShotForOrder OpenTimeForDaySnapShot,
            int unitAmount, string targetCustomerName, string targetCustomerPhone, string targetAddress, DateTime targetTime, string memo)
        {
            Details.Clear();
            ServiceOrderDetail detail = new ServiceOrderDetail(serviceId, serviceSnapShot, OpenTimeSnapShot,
                   //    OpenTimeForDaySnapShot,
                   unitAmount, targetCustomerName, targetCustomerPhone, targetAddress, targetTime, memo);
            Details.Add(detail);
            BusinessId = serviceSnapShot.BusinessId;


        }


        /// <summary>
        /// 订单状态的标题
        /// </summary>
        public virtual string GetStatusTitleFriendly(enum_OrderStatus status)
        {
            string str = string.Empty;

            switch (status)
            {
                case enum_OrderStatus.Search: str = "搜索单"; break;

                case enum_OrderStatus.Draft: str = "草稿"; break;
                case enum_OrderStatus.DraftPushed: str = "等待选择服务"; break;
                case enum_OrderStatus.Created: str = "等待支付订金"; break;
                case enum_OrderStatus.checkPayWithDeposit: str = "已支付订金"; break;
                case enum_OrderStatus.Payed: str = "系统已确认定金支付"; break;
                case enum_OrderStatus.Negotiate: str = "服务已确认"; break;
                case enum_OrderStatus.isNegotiate: str = "新价格生成"; break;
                case enum_OrderStatus.Assigned: str = "价格已更新"; break;
                case enum_OrderStatus.Begin: str = "服务中"; break;
                case enum_OrderStatus.isEnd: str = "服务完成"; break;
                case enum_OrderStatus.Ended: str = "服务已确认完成"; break;
                case enum_OrderStatus.checkPayWithNegotiate: str = "已支付尾款"; break;
                case enum_OrderStatus.Finished: str = "订单完成"; break;
                case enum_OrderStatus.Appraised: str = "已评价"; break;
                case enum_OrderStatus.EndWarranty: str = "订单已完结"; break;

                case enum_OrderStatus.Canceled: str = "已提交取消，正在拦截订单"; break;
                case enum_OrderStatus.WaitingDepositWithCanceled: str = "订单正在取消中"; break;
                case enum_OrderStatus.EndCancel: str = "取消成功"; break;

                case enum_OrderStatus.Refund: str = "理赔中"; break;
                case enum_OrderStatus.WaitingRefund: str = "审核中"; break;
                case enum_OrderStatus.isRefund: str = "审核通过"; break;
                case enum_OrderStatus.RejectRefund: str = "审核驳回"; break;
                case enum_OrderStatus.AskPayWithRefund: str = "审核暂停"; break;
                case enum_OrderStatus.WaitingPayWithRefund: str = "等待支付违约金"; break;
                case enum_OrderStatus.checkPayWithRefund: str = "已支付违约金"; break;
                case enum_OrderStatus.EndRefund: str = "理赔完成"; break;

                case enum_OrderStatus.InsertIntervention: str = "一点办介入并提交凭证"; break;
                case enum_OrderStatus.HandleWithIntervention: str = "官方处理中"; break;
                case enum_OrderStatus.NeedRefundWithIntervention: str = "官方处理:理赔通过"; break;
                case enum_OrderStatus.NeedPayWithIntervention: str = "官方处理:理赔暂停"; break;
                case enum_OrderStatus.checkPayWithIntervention: str = "已支付违约金"; break;
                case enum_OrderStatus.EndIntervention: str = "官方介入完成"; break;

                case enum_OrderStatus.ForceStop: str = "已终止"; break;
                case enum_OrderStatus.Complaints: str = "投诉中"; break;
                case enum_OrderStatus.WaitingComplaints: str = "审核投诉中"; break;
                case enum_OrderStatus.EndComplaints: str = "投诉完成"; break;

                default: str = "未知订单类型"; break;
            }
            return str;
        }
        /// <summary>
        /// 订单状态的内容
        /// </summary>
        public virtual string GetStatusContextFriendly(enum_OrderStatus status)
        {
            string str = string.Empty;

            switch (status)
            {
                case enum_OrderStatus.Search: str = "搜索单"; break;

                case enum_OrderStatus.Draft: str = "未生成订单"; break;
                case enum_OrderStatus.DraftPushed: str = "小助理已推荐服务"; break;
                case enum_OrderStatus.Created: str = "新的订单已经生成"; break;
                case enum_OrderStatus.checkPayWithDeposit: str = "请等待系统确认"; break;
                case enum_OrderStatus.Payed: str = "等待店家确认服务"; break;
                case enum_OrderStatus.Negotiate: str = "店家马上会为您服务"; break;
                case enum_OrderStatus.isNegotiate: str = "店家已更新价格，请您确认"; break;
                case enum_OrderStatus.Assigned: str = "等待店家开始服务"; break;
                case enum_OrderStatus.Begin: str = "店家已开始服务，请确认是否开始"; break;
                case enum_OrderStatus.isEnd: str = "服务已完成，请确认是否满足您的需求"; break;
                case enum_OrderStatus.Ended: str = "等待您支付尾款"; break;
                case enum_OrderStatus.checkPayWithNegotiate: str = "等待系统确认"; break;
                case enum_OrderStatus.Finished: str = "订单已完成，请对服务做出评价，订单将在2天后关闭"; break;
                case enum_OrderStatus.Appraised: str = "感谢您对服务做出的评价，这将帮助我们做得更好"; break;
                case enum_OrderStatus.EndWarranty: str = "非常感谢您对我们的支持"; break;

                case enum_OrderStatus.Canceled: str = "已提交取消订单请求"; break;
                case enum_OrderStatus.WaitingDepositWithCanceled: str = "取消请求已被受理，预计48小时内到帐，若未到帐，请咨询客服"; break;
                case enum_OrderStatus.EndCancel: str = "订单已被取消了"; break;

                case enum_OrderStatus.Refund: str = "已提交理赔的请求"; break;
                case enum_OrderStatus.WaitingRefund: str = "等待店家审核理赔请求"; break;
                case enum_OrderStatus.isRefund: str = "理赔请求已通过，等待返还理赔金额"; break;
                case enum_OrderStatus.RejectRefund: str = "理赔请求被驳回，您可申请一点办进行介入或取消理赔"; break;
                case enum_OrderStatus.AskPayWithRefund: str = "理赔请求被暂停，店家要求支付违约金"; break;
                case enum_OrderStatus.WaitingPayWithRefund: str = "您已同意理赔并支付违约金"; break;
                case enum_OrderStatus.checkPayWithRefund: str = "等待系统收账"; break;
                case enum_OrderStatus.EndRefund: str = "非常感谢您对我们的支持"; break;

                case enum_OrderStatus.InsertIntervention: str = "一点办已介入，请提交凭证"; break;
                case enum_OrderStatus.HandleWithIntervention: str = "官方已收集凭证，正在处理"; break;
                case enum_OrderStatus.NeedRefundWithIntervention: str = "官方已处理完成，同意此次理赔请求"; break;
                case enum_OrderStatus.NeedPayWithIntervention: str = "官方已处理完成，认定需要支付违约金"; break;
                case enum_OrderStatus.checkPayWithIntervention: str = "等待系统收账"; break;
                case enum_OrderStatus.EndIntervention: str = "非常感谢您对我们的支持"; break;

                case enum_OrderStatus.ForceStop: str = "很抱歉，因超时未支付赔偿金，此订单已被终止"; break;
                case enum_OrderStatus.Complaints: str = "已提交投诉申请"; break;
                case enum_OrderStatus.WaitingComplaints: str = "等待审核投诉"; break;
                case enum_OrderStatus.EndComplaints: str = "非常感谢您对我们的支持"; break;

                default: str = "未知订单类型"; break;
            }
            return str;
        }

        #endregion


        #region properties
        /// <summary>
        /// 服务详情
        /// </summary>
        public virtual IList<ServiceOrderDetail> Details
        {
            get; protected set;
        }
        private string businessId;
        /// <summary>
        /// 店铺
        /// </summary>
        public virtual string BusinessId
        {
            get
            {
                return businessId;
                //if (Details.Count == 0)
                //{ return null; }
                // ;
                //string errMsg;
                //var businessesInOrder=    Details.Select(x => x.OriginalService.Business).ToList();
                //int count = businessesInOrder.Count;
                //if (count == 1)
                //{
                //    return businessesInOrder[0];
                //}
                //else {
                //    if (count > 1)
                //    {
                //        errMsg = "订单内有多个商家";
                //        log.Error(errMsg);
                //        throw new Exception(errMsg);
                //    }
                //    else {
                //        errMsg = "订单内的服务居然没有";
                //        log.Error(errMsg);
                //        throw new Exception(errMsg);
                //    }
                //}

            }
            protected set { businessId = value; }
        }
        /// <summary>
        /// 订单的标题
        /// </summary>
        public virtual string Title
        {
            get
            {
                string name = string.Empty;
                foreach (ServiceOrderDetail detail in Details)
                {
                    name += detail.ServiceSnapShot.Name + ";";
                }
                return name.TrimEnd(';');
            }
        }


        /// <summary>
        /// 服务店铺名称
        /// </summary>
        public virtual string ServiceBusinessName
        {
            get
            {
                string name = string.Empty;
                foreach (ServiceOrderDetail detail in Details)
                {
                    name += detail.ServiceSnapShot.BusinessName + ";";
                }
                return name.TrimEnd(';');
            }
        }
        /// <summary>
        /// 服务商家电话
        /// </summary>
        public virtual string ServiceBusinessPhone
        {
            get
            {
                string name = string.Empty;
                foreach (ServiceOrderDetail detail in Details)
                {
                    name += detail.ServiceSnapShot.BusinessPhone + ";";
                }
                return name.TrimEnd(';');
            }
        }
        /// <summary>
        /// 服务描述
        /// </summary>
        public virtual string Description
        {
            get
            {
                string description = string.Empty;
                foreach (ServiceOrderDetail detain in Details)
                {
                    description += detain.ServiceSnapShot.Description + ";";
                }
                return description.TrimEnd(';');
            }
        }
        /// <summary>
        /// 订单关联的服务,可以为空.
        /// </summary>
        public virtual string ServiceId
        {
            get
            {
                int i = Details.Count;
                if (i == 1)
                {
                    return Details[0].OriginalServiceId;
                }
                else if (i > 1)
                {
                    log.Warn("该订单包含多个服务.返回第一个");
                    return Details[0].OriginalServiceId;
                }
                else
                {
                    return null;
                    throw new Exception("该订单内没有服务项");
                }
            }
        }

        /// <summary>
        /// 客户 
        /// </summary>
        public virtual string CustomerId { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public virtual DateTime OrderCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime OrderConfirmTime { get; set; }

        /// <summary>
        /// 最近的更新时间
        /// </summary>
        public virtual DateTime LatestOrderUpdated { get; set; }

        /// <summary>
        /// 订单结束的时间.
        /// </summary>
        public virtual DateTime OrderFinished { get; set; }
        /// <summary>
        /// 订单服务开始时间
        /// </summary>
        public virtual DateTime OrderServerStartTime { get; set; }

        /// <summary>
        /// 订单服务结束的时间.
        /// </summary>
        public virtual DateTime OrderServerFinishedTime { get; set; }
        /// <summary>
        /// 订单备注.
        /// </summary>
        public virtual string Memo { get; set; }
        /// <summary>
        /// 订单状态.
        /// </summary>

        public virtual enum_OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 目标用户名称
        /// </summary>
        public virtual string TargetCustomerName
        {
            get
            {
                return string.Join(Environment.NewLine, Details.Select(o => o.TargetCustomerName));
            }
        }
        /// <summary>
        /// 目标用户名称
        /// </summary>
        public virtual string TargetCustomerPhone
        {
            get
            {
                return string.Join(Environment.NewLine, Details.Select(o => o.TargetCustomerPhone));
            }
        }
        /// <summary>
        /// 服务的目标地址
        /// </summary>
        public virtual string TargetAddress
        {
            get
            {
                return string.Join(Environment.NewLine, Details.Select(o => o.TargetAddress));
            }
        }
        /// <summary>
        /// 用户备注
        /// </summary>
        public virtual string TargetMemo
        {
            get
            {
                return string.Join(Environment.NewLine, Details.Select(o => o.Memo));
            }
        }
        /// <summary>
        /// 用户预定的服务时间
        /// </summary>
        public virtual string TargetTime
        {
            get
            {
                return string.Join(Environment.NewLine, Details.Select(o => o.TargetTime));
            }
        }
        /// <summary>
        /// 分配的职员
        /// </summary>
        public virtual string StaffId
        {
            //get {

            //    //var l = from detail in Details
            //    //        select detail into ds
            //    //        from child in ds.Staff
            //    //        select child;
            //    //return l.ToList();
            //}
            get; set;//用于保存指派负责人
        }
        /// <summary>
        /// OpenFire订单联系人
        /// </summary>
        public virtual string OpenFireLinkMan
        {
            get
            {
                ///todo: refactor
                throw new NotImplementedException();
                //   return StaffId==null?businessId.OwnerId.ToString():Staff.UserID;
            }
        }
        /// <summary>
        /// 服务总数, 用来计算总价 service.unitprice*unitamount
        /// </summary>
        public virtual int UnitAmount
        {
            get
            {
                int unitAmount = 0;
                foreach (ServiceOrderDetail detail in Details)
                {
                    unitAmount += detail.UnitAmount;
                }
                return unitAmount;
            }
        }
        /// <summary>
        ///根据订单价格和订购数量计算的预期总价. 可能会被修改
        /// </summary>
        public virtual decimal OrderAmount
        {
            get
            {
                decimal amount = 0;
                foreach (ServiceOrderDetail detail in Details)
                {
                    amount += detail.ServiceAmount;
                }
                return amount;
            }
        }

        /// <summary>
        /// 订金
        /// </summary>
        public virtual decimal DepositAmount
        {
            get;

            set;
        }
        /// <summary>
        /// 协商总价
        /// </summary>
        public virtual decimal NegotiateAmount { get; set; }

        /// <summary>
        /// 商家修改过的协商总价
        /// </summary>
        public virtual decimal NegotiateAmount_Modified { get; set; }

        /// <summary>
        /// 创建此订单的客服
        /// </summary>
        public virtual string CustomerServiceId { get; set; }


        /// <summary>
        /// 订单自动取消的超时时间.  取订单项中超时时间的最小值.
        /// </summary>
        public virtual int ServiceOvertimeForCancel
        {
            get
            {
                if (Details.Count == 0) return 0;
                return Details.Min(x => x.ServiceSnapShot.OverTimeForCancel);
            }
        }

        /// <summary>
        /// 订单是否分账,true表示已分账，false表示未分账
        /// </summary>
        public virtual bool IsShared { get; set; }

        #endregion

        /// <summary>
        /// 订单转为正式.
        /// </summary>
        public virtual void CreatedFromDraft()
        {

            //判断信息完整性,确定订单的Scope类型.
            //没有任何服务项.

            Debug.Assert(Details.Count > 0, "错误:订单明细为空");
            if (Details.Count == 0)
            {
                string errMsg = "订单内没有服务项";
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            //订单金额默认为 订单明细总额之和
            foreach (ServiceOrderDetail detail in Details)
            {
                this.NegotiateAmount += detail.ServiceAmount;
                this.DepositAmount += detail.ServiceSnapShot.DepositAmount;
            }
            //this.OrderStatus = enum_OrderStatus.Created;
            this.OrderConfirmTime = DateTime.Now;
        }

        #region 临时数据
        /// <summary>
        /// 订单状态中文名
        /// </summary>
        public virtual string OrderStatusStr { get; set; }
        #endregion

        public virtual string ServiceTypeName
        {

            get
            {
                string name = string.Empty;
                foreach (ServiceOrderDetail detail in Details)
                {
                    name += detail.ServiceSnapShot.ServiceTypeName + ";";
                }
                return name.TrimEnd(';');
            }
        }
        public virtual string ServiceBusinessOwnerId
        {
            get
            {
                string name = string.Empty;
                //todo:refactor 需要进一步处理
                if (Details.Count > 1) { log.Error("订单服务数量大于1"); throw new Exception("订单服务数量大于1"); }
                foreach (ServiceOrderDetail detail in Details)
                {
                    name += detail.ServiceSnapShot.BusinessOwnerId + ";";
                }
                return name.TrimEnd(';');
            }
        }

        /// <summary>
        /// 申请一个支付项
        /// </summary>

        /// <param name="payTarget">支付类型</param>
        /// <returns></returns>
        public Payment ApplyPay(enum_PayTarget payTarget, IRepositoryPayment repoPayment, IRepositoryClaims repoClaims)
        {
            string errMsg = string.Empty;
            //验证请求类型是否有效
            bool applyIsValid = false;

            switch (payTarget)
            {

                case enum_PayTarget.Deposit:
                    errMsg = "只有 刚创建的订单 才能申请 订金支付";
                    applyIsValid = this.OrderStatus == enum_OrderStatus.Created;
                    break;
                case enum_PayTarget.FinalPayment:
                    //只有 已经服务完成的 订单 才能申请 支付尾款 
                    errMsg = "只有 已经服务完成的 订单 才能申请 支付尾款 ";
                    applyIsValid = this.OrderStatus == enum_OrderStatus.Ended || this.OrderStatus == enum_OrderStatus.Finished;
                    break;
                case enum_PayTarget.Compensation:
                    errMsg = "只有已经完成的订单 才能申请赔偿.";
                    applyIsValid = this.OrderStatus == enum_OrderStatus.Finished ||
                           this.OrderStatus == enum_OrderStatus.Appraised;
                    break;
            }
            if (!applyIsValid)
            {
                throw new Exception(errMsg);
            }
            //获取该订单已经申请过的项目.
            IList<Payment> payments = repoPayment.GetPaymentsForOrder(this);
            var paymentList = payments.Where(x => x.PayTarget == payTarget).ToList();
            //验证该支付申请是否有效. 
            //无效: 同类型的支付申请已经创建, 直接返回该支付链接. 当前支付金额
            var paymentCount = paymentList.Count();
            Payment payment = null;
            if (paymentCount == 1)
            {
                payment = paymentList[0];

                //验证该支付项的状态
                //todo:如果 支付成功 
                if (payment.Status == enum_PaymentStatus.Trade_Success)
                {
                    errMsg = "该项已经支付完成";
                    log.Error(errMsg);
                    throw new Exception(errMsg);
                }

                //该支付项已经创建,验证其金额是否有变化
                var payAmount = GetPayAmount(payTarget, repoClaims);
                if (payAmount != payment.Amount)
                {
                    errMsg = string.Format("本次申请金额和上次不一样. 本次:{0},上次:{1}", payment.Amount, payAmount);
                    log.Warn(errMsg);
                    //申请金额和之前的不一致, 需要警告
                }
                payment.Amount = payAmount;



            }
            else if (paymentCount == 0)
            {
                payment = new Payment(GetPayAmount(payTarget, repoClaims), this, payTarget);

                repoPayment.Add(payment);
            }
            else //已经存在多项
            {
                errMsg = string.Format("该订单已经存在多项同类型的支付项", this.Id);
                log.Fatal(errMsg);
                throw new Exception(errMsg);
            }
            return payment;
        }
        public void SaveOrderHistory(enum_OrderStatus oldStatus, IRepositoryServiceOrderStateChangeHis repoStateChante)
        {
            int num = 1;
            ServiceOrderStateChangeHis oldOrderHis = repoStateChante.GetMaxNumberOrderHis(this);
            if (oldOrderHis != null)
            {
                num = oldOrderHis.Number + 1;
            }
            ServiceOrderStateChangeHis orderHis = new ServiceOrderStateChangeHis(this, oldStatus, num);
            repoStateChante.Add(orderHis);
        }
        /// <summary>
        /// 获取支付总额
        /// </summary>
        /// <param name="payTarget">支付类型</param>
        /// <returns></returns>
        public decimal GetPayAmount(enum_PayTarget payTarget, IRepositoryClaims repoClaims)
        {
            if (payTarget == enum_PayTarget.Deposit)
            {
                return this.DepositAmount;
            }
            else if (payTarget == enum_PayTarget.FinalPayment)
            {
                return this.NegotiateAmount - this.DepositAmount;
            }
            else if (payTarget == enum_PayTarget.Compensation)
            {
                log.Debug("查询订单的理赔");
                Claims claims = repoClaims.GetOneByOrder(this);
                if (claims == null)
                {
                    log.Error("订单没有对应的理赔");
                    throw new Exception("订单没有对应的理赔");
                }

                log.Debug("查询理赔详情");
                IList<ClaimsDetails> cdList = claims.ClaimsDatailsList.OrderByDescending(x => x.LastUpdateTime).Where(x => x.Target == enum_ChatTarget.store).ToList();
                ClaimsDetails claimsDetails;
                if (cdList.Count > 0)
                {
                    claimsDetails = cdList[0];
                }
                else
                {
                    log.Error("该订单没有理赔");
                    throw new Exception("该订单没有理赔");
                }

                return claimsDetails.Amount;
            }
            else
            {
                throw new Exception("没有计算公式");
            }
        }


        public void Confirm_Order(IRepositoryServiceOrderPushedService repoPushedService, string serviceId,
             
          IRepositoryPayment repoPayment, IRepositoryClaims repoClaims)
        {
            IList<ServiceOrderPushedService> l = repoPushedService.FindByOrder(this);
            if (l.Count > 1)
            {
                throw new Exception("包含多个推送服务");
            }
            else if (l.Count == 1)
            {
                ServiceOrderPushedService s = l.Single(x => x.OriginalServiceId == serviceId);
                if (s == null)
                {
                    throw new Exception("该服务不是该订单的推送服务！");
                }
                

                //todo:  需要用Automapper

                this.AddDetailFromIntelService(s.OriginalServiceId, s.ServiceSnapShot,
                    s.ServiceOpenTimeSnapShot,
                    s.UnitAmount, s.TargetCustomerName, s.TargetCustomerPhone, s.TargetAddress, s.TargetTime, s.Memo);

                CreatedFromDraft();
                if (DepositAmount > 0)
                {
                    OrderServiceFlow.ChangeStatus(this, enum_OrderStatus.Created);

                    Payment payment = ApplyPay(enum_PayTarget.Deposit, repoPayment, repoClaims);
                }
                else
                {
                    OrderServiceFlow.ChangeStatus(this, enum_OrderStatus.Payed);
                }

            }
        }
    }
}
