﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
using System.Diagnostics;
 
namespace Dianzhu.Model
{
    /// <summary>
    /// 订单
    /// </summary>

    public class ServiceOrder:DDDCommon.Domain.Entity<Guid>
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model");

        
       public ServiceOrder()
        {
            OrderStatus = enum_OrderStatus.Draft;
            OrderCreated = DateTime.Now;
            LatestOrderUpdated = DateTime.Now;
            
            Details = new List<ServiceOrderDetail>();

        }
        #region 管理明细
        /// <summary>
        /// 增加一条订单明细
        /// limit: 目前限制 一个订单只有一个服务.
        /// todo: 增加 购物车概念, 通过购物车为不同商家生成不同订单.
        /// </summary>
        /// <param name="detail"></param>
        public virtual void AddDetailFromIntelService(DZService service,int unitAmount,string targetAddress,DateTime targetTime)
        {
            
            var existedService = Details.Where(x => x.OriginalService == service);
            if (existedService.Count() == 0)
            {
                ServiceOrderDetail detail = new ServiceOrderDetail(service, unitAmount, targetAddress, targetTime);
                Details.Add(detail);
                Business = service.Business;
            }
            else if (existedService.Count() == 1)
            {
                ServiceOrderDetail detail = Details[0];
                detail.UnitAmount += unitAmount;// new ServiceOrderDetail(service, unitAmount, targetAddress, targetTime);
                detail.TargetAddress = targetAddress;
                detail.TargetTime = targetTime;
                Business = service.Business;

            }
            else if (existedService.Count() > 1)
            {

            }

           
        }
        public virtual void RemoveDetail(DZService service)
        {

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

                case enum_OrderStatus.Canceled: str = "取消中"; break;
                case enum_OrderStatus.WaitingDepositWithCanceled: str = "取消即将成功"; break;
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
                case enum_OrderStatus.WaitingDepositWithCanceled: str = "取消请求已被受理，请系统等待返还订金"; break;
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

        /// <summary>
        /// 订单概要文本
        /// </summary>
        public virtual string GetSummaryString()
        {
            return string.Format("订单状态:{5} 用户:{0},定金金额:{1},服务项:{2},商家:{3}, 服务时间:{4}",
                Customer.DisplayName,DepositAmount, Title,ServiceBusinessName,TargetTime,GetStatusTitleFriendly(this.OrderStatus));
        }
        #endregion


        #region properties
        public virtual IList<ServiceOrderDetail> Details
        {
            get; protected set;
        }
        private Business business;
        public virtual Business Business {
            get {
                return business;
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
           protected set { business = value; }
        }
        /// <summary>
        /// 订单的标题
        /// </summary>
        public virtual string Title { get {
                string name = string.Empty;
                foreach (ServiceOrderDetail detail in Details)
                {
                    name += detail.ServieSnapShot. ServiceName + ";";
                }
                return name;
            } }

        public virtual string ServiceBusinessName {
            get {
                string name = string.Empty;
                foreach (ServiceOrderDetail detail in Details)
                {
                    name += detail.OriginalService.Business.Name + ";";
                }
                return name;
            }
        }
        public virtual string Description {
            get {
                string description = string.Empty;
                foreach (ServiceOrderDetail detain in Details)
                {
                    description += detain.ServieSnapShot. Description + ";";
                }
                return description;
            }
        }
        /// <summary>
        /// 订单关联的服务,可以为空.
        /// </summary>
        public virtual DZService Service { get {
                int i = Details.Count;
                if (i == 1)
                {
                    return Details[0].OriginalService;
                }
                else if (i > 1)
                {
                    log.Warn("该订单包含多个服务.返回第一个");
                    return Details[0].OriginalService;
                }
                else {
                    return null;
                    throw new Exception("该订单内没有服务项");
                }
            } }

        /// <summary>
        /// 客户 
        /// </summary>
        public virtual DZMembership Customer { get; set; }

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
        /// 服务的目标地址
        /// </summary>
        public virtual string TargetAddress { get {
                return string.Join(Environment.NewLine, Details.Select(o => o.TargetAddress));
            } }
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
        public virtual IList<Staff> Staff { get{

                var l = from detail in Details
                        select detail into ds
                        from child in ds.Staff
                        select child;
                return l.ToList();
            } }
        /// <summary>
        /// 服务总数, 用来计算总价 service.unitprice*unitamount
        /// </summary>
        public virtual int UnitAmount { get {
                int unitAmount = 0;
                foreach (ServiceOrderDetail detail in Details)
                {
                    unitAmount += detail.UnitAmount;
                }
                return unitAmount;
            } }
        /// <summary>
        ///根据订单价格和订购数量计算的预期总价. 可能会被修改
        /// </summary>
        public virtual decimal OrderAmount { get {
                decimal amount = 0;
                foreach (ServiceOrderDetail detail in Details)
                {
                    amount += detail.ServiceAmount;
                }
                return amount;
            } }

        /// <summary>
        /// 订金
        /// </summary>
        public virtual decimal DepositAmount {
            get;
             
            set;
        }
        /// <summary>
        /// 协商总价
        /// </summary>
        public virtual decimal NegotiateAmount { get; set; }

        
        //创建此订单的客服.
        public virtual DZMembership CustomerService { get; set; }
      

        /// <summary>
        /// 订单自动取消的超时时间.  取订单项中超时时间的最小值.
        /// </summary>
        public virtual int ServiceOvertimeForCancel
        { get {
                if (Details.Count == 0) return 0;
                return Details.Min(x => x.ServieSnapShot.OverTimeForCancel);
            } }

        /// <summary>
        /// 获取需要支付的金额
        /// </summary>
        /// <param name="payTarget">支付目标: 定金 或者 尾款 或者../.</param>
        /// <returns></returns>
        #endregion

        //订单转为正式.
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
                this.DepositAmount += detail.ServieSnapShot.DepositAmount;
            }
            this.OrderStatus = enum_OrderStatus.Created;
            this.OrderConfirmTime = DateTime.Now;
        }

        #region 临时数据
        /// <summary>
        /// 订单状态中文名
        /// </summary>
        public virtual string OrderStatusStr{ get; set; }
        #endregion

    }



}
