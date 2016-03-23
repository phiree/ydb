using System;
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

    public class ServiceOrder
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model");
        
       internal ServiceOrder()
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
            }
            else if (existedService.Count() == 1)
            {
                ServiceOrderDetail detail = Details[0];
                detail.UnitAmount+=unitAmount;// new ServiceOrderDetail(service, unitAmount, targetAddress, targetTime);
                detail.TargetAddress = targetAddress;
                detail.TargetTime = targetTime;
                
            }
           
        }
        public virtual void RemoveDetail(DZService service)
        {

        }

        /// <summary>
        /// 将英文状态转换为中文
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public virtual string GetFriendlyStatus(enum_OrderStatus status)
        {
            string str = String.Empty;
            switch (status)
            {
                case enum_OrderStatus.Search: str = "搜索单"; break;
                case enum_OrderStatus.Draft: str = "草稿单"; break;
                case enum_OrderStatus.DraftPushed: str = "推送服务的草稿单"; break;
                case enum_OrderStatus.Created: str = "等待付款"; break;
                case enum_OrderStatus.Payed: str = "已付款"; break;
                case enum_OrderStatus.Canceled: str = "客户申请取消订单"; break;
                case enum_OrderStatus.CanceledDirectly: str = "订单已取消"; break;
                case enum_OrderStatus.isCancel: str = "客户已取消"; break;
                case enum_OrderStatus.Aborded: str = "订单已中止"; break;
                case enum_OrderStatus.Negotiate: str = "商家已确认订单"; break;
                case enum_OrderStatus.Assigned: str = "等待服务开始"; break;
                case enum_OrderStatus.Begin: str = "服务已开始"; break;
                case enum_OrderStatus.IsEnd: str = "商家确定服务完成"; break;
                case enum_OrderStatus.Ended: str = "用户确定服务完成"; break;
                case enum_OrderStatus.Finished: str = "订单完成"; break;
                case enum_OrderStatus.Appraised: str = "用户已评价"; break;
                case enum_OrderStatus.WaitingDepositWithCanceled: str = "等待退还定金"; break;
                case enum_OrderStatus.WaitingCancel: str = "用户申请取消订单"; break;
                default:str = "未知订单类型"; break;
            }
            return str;
        }

        #endregion


        #region properties
        public virtual IList<ServiceOrderDetail> Details
        {
            get; protected set;
        }
        public virtual Guid Id { get;  protected  set; }
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

         
        
        //草稿单转为正式单之后
      

        /// <summary>
        /// 订单自动取消的超时时间.  取订单项中超时时间的最小值.
        /// </summary>
        public virtual int ServiceOvertimeForCancel
        { get {
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

        }
        /// <summary>
        /// 获取支付总额
        /// </summary>
        /// <param name="payTarget">支付类型</param>
        /// <returns></returns>
        public virtual decimal GetPayAmount(enum_PayTarget payTarget)
        {
            if (payTarget == enum_PayTarget.Deposit)
            {
                return this.DepositAmount;
            }
            else if (payTarget == enum_PayTarget.FinalPayment)
            {
                return this.NegotiateAmount - DepositAmount;
            }
            else
            {
                throw new Exception("没有计算公式");
            }
        }



    }



}
