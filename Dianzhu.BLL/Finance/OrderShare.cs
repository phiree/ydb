using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;

namespace Dianzhu.BLL.Finance
{
    /// <summary>
    /// 分账服务.
    /// </summary>
    public class OrderShare : IOrderShare
    {

        IBLLServiceTypePoint bllServiceTypePoint;
        IBalanceFlowService balanceService;
        IBLLSharePoint bllSharePoint;
        
        Agent.IAgentService agentService;
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.Finance.OrderShare");
        public OrderShare(IBLLServiceTypePoint bllServiceTypePoint,
        IBLLSharePoint bllSharePoint,
        Agent.IAgentService agentService,
        IBalanceFlowService balanceService)
        {
            this.bllServiceTypePoint = bllServiceTypePoint;
            this.bllSharePoint = bllSharePoint;
            this.agentService = agentService;
            this.balanceService = balanceService;
        }
        /// <summary>
        /// 订单分成,should be domain service
        /// </summary>
        /// <param name="order"></param>
        /// <returns>分成详情.</returns>
        public IList<Dianzhu.Model.Finance.BalanceFlow> Share(ServiceOrder order)
        {
            IList<Model.Finance.BalanceFlow> balanceFlows = new List<Model.Finance.BalanceFlow>();
            //- 获取该订单总的分账额度
            //-- 循环订单明细,获取每种服务的类型(目前只支持一个订单一个服务)
            log.Debug("开始分账:" + order.Id);
            string errMsg = string.Empty;
            if (order.Details.Count != 1)
            {
                errMsg = "订单内仅且只能包含一个服务.";
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            var serviceType = order.Details[0].OriginalService.ServiceType;

            var typePoint = bllServiceTypePoint.GetPoint(order.Details[0].OriginalService.ServiceType);
            var sharedAmount = order.NegotiateAmount * typePoint;


       
          
            //-  代理商分成
            var area = order.Details[0].OriginalService.Business.AreaBelongTo;
            var agent = agentService.GetAreaAgent(area);
            var agentShare = 0m;
            if (agent != null)
            {
                var agentSharePoint = bllSharePoint.GetSharePoint(agent);
                agentShare = sharedAmount * agentSharePoint;
                Dianzhu.Model.Finance.BalanceFlow flowAgent= new Model.Finance.BalanceFlow
                {
                    Amount = agentShare,
                    Member = agent,
                    RelatedObjectId = order.Id.ToString(),
                    OccurTime = DateTime.Now,
                    FlowType = Model.Finance.enumFlowType.OrderShare

                };
                balanceFlows.Add(flowAgent);
            }

            //- 助理分成
            var customerServiceSharePoint = bllSharePoint.GetSharePoint(order.CustomerService);
            var customerServiceShare = customerServiceSharePoint * sharedAmount;
            Dianzhu.Model.Finance.BalanceFlow flowCustomerService = new Model.Finance.BalanceFlow
            {
                Amount = customerServiceShare,
                Member =order.CustomerService,
                RelatedObjectId= order.Id.ToString(),
                OccurTime = DateTime.Now,
                FlowType = Model.Finance.enumFlowType.OrderShare

            };
            balanceFlows.Add(flowCustomerService);
            //商家
            var businessAmount = order.NegotiateAmount - sharedAmount;

            Dianzhu.Model.Finance.BalanceFlow flowBusiness = new Model.Finance.BalanceFlow {
                Amount = businessAmount,
                Member = order.Business.Owner,
                FlowType = Model.Finance.enumFlowType.OrderShare,
                OccurTime = DateTime.Now,
                RelatedObjectId = order.Id.ToString()
            };
            balanceFlows.Add(flowBusiness);


       
            log.Debug("结束分账:" + order.Id);
            return balanceFlows;
        }

        /// <summary>
        /// ddd: should be application service
        /// </summary>
        public void ShareOrder(ServiceOrder order)
        {
            IList<Model.Finance.BalanceFlow> balanceFlow = Share(order);
            foreach (Model.Finance.BalanceFlow bf in balanceFlow)
            {
                balanceService.Save(bf);
            }
        }
        
    }
}
