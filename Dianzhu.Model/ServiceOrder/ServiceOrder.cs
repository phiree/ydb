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
            
            Details = new List<ServiceOrderDetail>();

        }
        #region 管理明细
        /// <summary>
        /// 增加一条订单明细
        /// </summary>
        /// <param name="detail"></param>
        public virtual void AddDetailFromIntelService(DZService service,int unitAmount,string targetAddress,string targetTime)
        {
            ServiceOrderDetail detail = new ServiceOrderDetail(service,unitAmount,targetAddress,targetTime);
            
            Details.Add(detail);
        }
        public virtual void RemoveDetail(DZService service)
        {

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
                    name += detail.ServiceName + ";";
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
                    description += detain.Description + ";";
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
        public virtual void CreatedFromDraft()
        {

            //判断信息完整性,确定订单的Scope类型.
             //没有任何服务项.
            if (Details.Count==0)
            {
                string errMsg = "订单内没有服务项";
                Debug.Assert(false, errMsg);
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            //订单金额默认为 订单明细总额之和
            foreach (ServiceOrderDetail detail in Details)
            {
                this.NegotiateAmount += detail.ServiceAmount;
                this.DepositAmount += detail.DepositAmount;
            }
           
        }

        /// <summary>
        /// 订单自动取消的超时时间.  取订单项中超时时间的最小值.
        /// </summary>
        public virtual int ServiceOvertimeForCancel
        { get {
                return Details.Min(x => x.OverTimeForCancel);
            } }

        /// <summary>
        /// 获取需要支付的金额
        /// </summary>
        /// <param name="payTarget">支付目标: 定金 或者 尾款 或者../.</param>
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

        #endregion  
 




    }



}
