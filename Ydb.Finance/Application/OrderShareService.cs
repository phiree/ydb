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
        ISession session;
        IRepositoryBalanceFlow repositoryBalanceFlow;
        internal OrderShareService(IServiceTypePointService serviceTypePointService, IUserTypeSharePointService userTypeSharePointService,
        ISession session,IRepositoryBalanceFlow repositoryBalanceFlow)
        {
            this.serviceTypePointService = serviceTypePointService;
            this.userTypeSharePointService = userTypeSharePointService;
            this.session = session;
            this.repositoryBalanceFlow = repositoryBalanceFlow;
        }

        /// <summary>
        /// 订单分成,should be domain service
        /// </summary>
        /// <param name="order"></param>
        /// <returns>分成详情.</returns>
        internal IList<BalanceFlow> Share(BalanceParam order)
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
            for (int i = 0; i < order.BalanceUser.Count; i++)
            {
                sharePoint = userTypeSharePointService.GetSharePoint(order.BalanceUser[i].UserType, out errMsg);
                switch (order.BalanceUser[i].UserType)
                {
                    case "agent":
                        share = Math.Truncate(sharedAmount * sharePoint * 100) / 100m;
                        break;
                    case "customerservice":
                        share = Math.Truncate(sharedAmount * sharePoint * 100) / 100m;
                        break;
                    case "business":
                        share = Math.Truncate(order.Amount * (1 - typePoint) * 100) / 100m;
                        break;
                    case "diandian":
                        share = order.Amount - Math.Truncate(order.Amount * (1 - typePoint) * 100) / 100m;
                        sharePoint = userTypeSharePointService.GetSharePoint("agent", out errMsg);
                        share = share - Math.Truncate(sharedAmount * sharePoint * 100) / 100m;
                        sharePoint = userTypeSharePointService.GetSharePoint("customerservice", out errMsg);
                        share = share - Math.Truncate(sharedAmount * sharePoint * 100) / 100m;
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
        /// ddd: should be application service
        /// </summary>
        public void ShareOrder(BalanceParam order)
        {
            IList<BalanceFlow> balanceFlow = Share(order);
            foreach (BalanceFlow bf in balanceFlow)
            {
                repositoryBalanceFlow.Add(bf);
            }
        }
    }
}
