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
        
        public ServiceOrder()
        {
            OrderStatus = enum_OrderStatus.Draft;
            OrderCreated = DateTime.Now;
            Staff = new List<Staff>();

        }
        public virtual Guid Id { get; set; }

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
        /// 订单的时间要求
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
        public virtual decimal DepositAmount { get; set; }
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


        #endregion
        #region 客户冗余信息,一定是注册用户
        public virtual string CustomerName { get; set; }
        public virtual string CustomerPhone { get; set; }
        public virtual string CustomerEmail { get; set; }
        #endregion

        public virtual enum_ServiceScopeType ScopeType { get; set; }
        public virtual string BuildPayLink(string payUrl)
        {
            string payLink = payUrl +
                string.Format("?orderid={0}", Id);
            return payLink;
        }
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
        public virtual void CreateFromDraft( )
        {
            string errMsg = string.Empty;

            //判断信息完整性,确定订单的Scope类型.
            bool hasMember = this.Customer != null;
            bool hasService = this.Service != null;
            if (hasMember && hasService)
            {
                this.ScopeType = enum_ServiceScopeType.ISIM;
            }
            else if (!hasMember && hasService)
            {
                this.ScopeType = enum_ServiceScopeType.ISOM;
            }
            else if (hasMember && !hasService)
            {
                this.ScopeType = enum_ServiceScopeType.OSIM;
            }
            else
            {
                this.ScopeType = enum_ServiceScopeType.OSOM;
            }
            bool checkEssentionalInfo = true;
            if (!hasService && string.IsNullOrEmpty(this.ServiceName))
            {
                checkEssentionalInfo = false;
                errMsg = "订单创建失败,缺少服务信息";

                //
            }
            if (!hasMember && string.IsNullOrEmpty(this.CustomerName))
            {
                checkEssentionalInfo = false;
                errMsg = "订单创建失败,缺少用户信息";
                //
            }
            if (!checkEssentionalInfo)
            {
                Debug.Assert(false, errMsg);
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            this.OrderStatus = enum_OrderStatus.Created;

           

        }

        public virtual decimal GetAmount(enum_PayTarget payTarget)
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
