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
            Staff = new List<Staff>();
            
            Details = new List<ServiceOrderDetail>();

        }
        #region 管理明细
        /// <summary>
        /// 增加一条订单明细
        /// </summary>
        /// <param name="detail"></param>
        public void AddDetailFromIntelService(DZService service)
        {
            ServiceOrderDetail detail = new ServiceOrderDetail(service);
            Details.Add(detail);
        }
        public void AddDetailFromExternalService(
       string ServiceName,
         string Description,
          bool IsCompensationAdvance,

          decimal MinPrice,
          decimal UnitPrice,
          enum_ChargeUnit ChargeUnit,

          decimal DepositAmount,
          decimal CancelCompensation,
          int OverTimeForCancel,
          enum_ServiceMode ServiceMode)
        {
            ServiceOrderDetail detail = new ServiceOrderDetail(ServiceName,
           Description,
            IsCompensationAdvance,

            MinPrice,
            UnitPrice,
            ChargeUnit,

            DepositAmount,
            CancelCompensation,
            OverTimeForCancel,
            ServiceMode);
            Details.Add(detail);
        }
        #endregion

        #region properties
        public IList<ServiceOrderDetail> Details
        {
            get; protected set;
        }
        public virtual Guid Id { get;  protected  set; }

        /// <summary>
        /// 订单关联的服务,可以为空.
        /// </summary>
        public virtual DZService Service { get; set; }

        /// <summary>
        /// 客户,可以为空
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
        public virtual string TargetAddress { get; set; }
        /// <summary>
        /// 用户预定的服务时间
        /// </summary>
        public virtual string TargetTime { get; set; }
        /// <summary>
        /// 分配的职员
        /// </summary>
        public virtual IList<Staff> Staff { get; set; }
        /// <summary>
        /// 服务总数, 用来计算总价 service.unitprice*unitamount
        /// </summary>
        public virtual int UnitAmount { get; set; }
        /// <summary>
        ///订单预期
        /// </summary>
        public virtual decimal OrderAmount { get; set; }

        /// <summary>
        /// 订金
        /// </summary>
        public virtual decimal DepositAmount {get; set; }
        /// <summary>
        /// 协商总价
        /// </summary>
        public virtual decimal NegotiateAmount { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public virtual string RefundMemo { get; set; }
        /// <summary>
        /// 外部服务的链接
        /// </summary>
        public virtual string ServiceURL { get; set; }
        #region 服务冗余信息,
        public virtual string ServiceName { get; set; }
        public virtual string ServiceDescription { get; set; }
        public virtual string ServiceBusinessName { get; set; }
        public virtual decimal ServiceUnitPrice { get; set; }

        /// <summary>
        /// 取消超时时间:分钟
        /// </summary>
        public virtual int ServiceOvertimeForCancel { get; set; }
        /// <summary>
        /// 超时取消需要支付的赔偿金
        /// </summary>
        public virtual decimal ServiceCancelCompensation
        { get; set; }


        #endregion
        #region 客户冗余信息,一定是注册用户
        public virtual string CustomerName { get; set; }
        public virtual string CustomerPhone { get; set; }
        public virtual string CustomerEmail { get; set; }
        #endregion
  
        //创建此订单的客服.
        public virtual DZMembership CustomerService { get; set; }

        /// <summary>
        /// 支付交易号
        /// </summary>
        public virtual string TradeNo { get; set; }
        /// <summary>
        /// 从草稿单创建正式订单.
        /// </summary>
        /// <param name="serviceOrder"></param>
        #endregion

        #region 流程及操作
        
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
        /// 用户支付定金之后
        /// </summary>
       

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
