using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel.Enums;
using Ydb.Finance.DomainModel;
using NHibernate;

namespace Ydb.Finance.Application
{
    public class OrderShareService: IOrderShareService
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Finance.OrderShareService");
        IServiceTypePointService serviceTypePointService;
        IUserTypeSharePointService userTypeSharePointService;
        IRepositoryBalanceFlow repositoryBalanceFlow;
        IRepositoryBalanceTotal repositoryBalanceTotal;
        public OrderShareService(IServiceTypePointService serviceTypePointService, IUserTypeSharePointService userTypeSharePointService)
        {
            this.serviceTypePointService = serviceTypePointService;
            this.userTypeSharePointService = userTypeSharePointService;
            repositoryBalanceFlow = Ydb.Finance.Infrastructure.Bootstrap.Container.Resolve<IRepositoryBalanceFlow>();
            repositoryBalanceTotal = Ydb.Finance.Infrastructure.Bootstrap.Container.Resolve<IRepositoryBalanceTotal>();
        }

        /// <summary>
        /// 订单分成,should be domain service
        /// </summary>
        /// <param name="order" type="OrderShareParam">分账的订单及用户信息</param>
        /// <returns type="IList<BalanceFlow>">分成详情列表.</returns>
        internal IList<BalanceFlow> Share(OrderShareParam order)
        {
            IList<BalanceFlow> balanceFlows = new List<BalanceFlow>();
            //- 获取该订单总的分账额度
            //-- 循环订单明细,获取每种服务的类型(目前只支持一个订单一个服务)
            log.Debug("开始分账:" + order.RelatedObjectId);
            
            var typePoint = serviceTypePointService.GetPoint(order.ServiceTypeID);
            var sharedAmount = order.Amount * typePoint;

            var share= 0m;
            decimal sharePoint = 0;
            string errMsg = string.Empty;
            IList<string> strUserTypes = new List<string>();
            strUserTypes.Add("business");
            for (int i = 0; i < order.BalanceUser.Count; i++)
            {
                switch (order.BalanceUser[i].UserType)
                {
                    case "agent":
                        strUserTypes.Add("agent");
                        break;
                    case "customerservice":
                        strUserTypes.Add("customerservice");
                        break;
                    case "business":
                        break;
                    case "diandian":
                        break;
                    default:
                        throw new Exception("分账用户类型不正确!");
                }
            }
            strUserTypes.Add("diandian");
            decimal shareDiandian = order.Amount;
            for (int i = 0; i < strUserTypes.Count; i++)
            {
                switch (strUserTypes[i])
                {
                    case "agent":
                        sharePoint = userTypeSharePointService.GetSharePoint(strUserTypes[i], out errMsg);
                        share = Math.Truncate(sharedAmount * sharePoint * 100) / 100m;
                        shareDiandian = shareDiandian - share;
                        break;
                    case "customerservice":
                        sharePoint = userTypeSharePointService.GetSharePoint(strUserTypes[i], out errMsg);
                        share = Math.Truncate(sharedAmount * sharePoint * 100) / 100m;
                        shareDiandian = shareDiandian - share;
                        break;
                    case "business":
                        share = Math.Truncate(order.Amount * (1 - typePoint) * 100) / 100m;
                        shareDiandian = shareDiandian - share;
                        break;
                    case "diandian":
                        share = shareDiandian;
                        break;
                    default:
                        throw new Exception("分账用户类型不正确!");
                }
                BalanceFlow flow = new BalanceFlow
                {
                    AccountId = order.BalanceUser[i].AccountId,
                    Amount = share,
                    RelatedObjectId = order.RelatedObjectId,
                    OccurTime = DateTime.Now,
                    FlowType = FlowType.OrderShare,
                    Income = true
                };
                balanceFlows.Add(flow);
            }

            log.Debug("结束分账:" + order.RelatedObjectId);
            return balanceFlows;
        }

        /// <summary>
        /// 订单分成操作
        /// </summary>
        /// <param name="order" type="OrderShareParam">分账的订单及用户信息</param>
        [Ydb.Common.Repository.UnitOfWork]
        public void ShareOrder(OrderShareParam order)
        {
            IList<BalanceFlow> balanceFlow = Share(order);
            foreach (BalanceFlow bf in balanceFlow)
            {
                repositoryBalanceFlow.Add(bf);
                repositoryBalanceTotal.InBalance(bf.AccountId, bf.Amount);
            }
        }
    }
}
