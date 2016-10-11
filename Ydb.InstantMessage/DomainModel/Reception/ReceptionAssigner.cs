using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
using Ydb.InstantMessage.DomainModel.Enums;
namespace Ydb.InstantMessage.DomainModel.Reception
{

    public class ReceptionAssigner:IReceptionAssigner
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.InstantMessage.DomainModel.Reception");
        IReceptionSession receptionSession;
        AssignStratage assignStratage;


        string DianDianId {
            get {
                return Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            }
        }
        public ReceptionAssigner(IReceptionSession receptionSession, AssignStratage assignStratage)
        {
            this.receptionSession = receptionSession;
            this.assignStratage = assignStratage;
        }
        public string AssignCustomerServiceToCustomer(IList<ReceptionStatus> existedReceptionForCustomer,string customerId)
        {
            //获取对应的的分配列表
            //  existedReception = receptionRepository.FindByCustomerId(customerId);

            //如果是客户, 那么最多只有一条分配
            if (existedReceptionForCustomer.Count > 1)
            {
                log.Warn("同一个客户匹配了多个客服");
                //返回不是diandian的客服
                return existedReceptionForCustomer.First(x => x.CustomerServiceId != DianDianId).CustomerServiceId;

            }
            else if (existedReceptionForCustomer.Count == 1)
            {

                if (!receptionSession.IsUserOnline(existedReceptionForCustomer[0].CustomerServiceId))
                {
                    log.Warn("客服应该是在线状态,但是现在不是");
                }
                return existedReceptionForCustomer[0].CustomerServiceId;
            }
            else
            {
                var onlineList = receptionSession.GetOnlineSessionUser( XmppResource.YDBan_CustomerService);
                Dictionary<string, string> assignResult = assignStratage.Assign(new List<string> { customerId }, onlineList.Select(x => x.username).ToList(),
                                    DianDianId);
                return assignResult[customerId];

            }


        }
        public Dictionary<string, string> ReAssignToOther(IList<ReceptionStatus> existedReceptionForCustomerService)
        {
           
            //获取当前客户接待的客户
              var onlineList = receptionSession.GetOnlineSessionUser( XmppResource.YDBan_CustomerService);
            //re assign
            Dictionary<string, string> newAssign
                = assignStratage.Assign(existedReceptionForCustomerService.Select(x => x.CustomerId).ToList(), onlineList.Select(x => x.username).ToList(),
                DianDianId);

            foreach (ReceptionStatus status in existedReceptionForCustomerService)
            {
                status.CustomerServiceId = newAssign[status.CustomerId];
                 
            }
            return newAssign;
        }
    }
}
