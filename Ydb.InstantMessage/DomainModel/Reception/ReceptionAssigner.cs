using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;
namespace Ydb.InstantMessage.DomainModel.Reception
{

    public class ReceptionAssigner
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.InstantMessage.DomainModel.Reception");
        IReceptionSession receptionSession;
        IRepositoryReception receptionRepository;
        AssignStratage assignStratage;
        public ReceptionAssigner(IReceptionSession receptionSession,
        IRepositoryReception receptionRepository, AssignStratage assignStratage)
        {
            this.receptionRepository = receptionRepository;
            this.receptionSession = receptionSession;
            this.assignStratage = assignStratage;
        }
        public string Assign(string customerId)
        {
            //获取对应的的分配列表
            var receptionList = receptionRepository.Find(x => x.CustomerId == customerId);

            //如果是客户, 那么最多只有一条分配
            if (receptionList.Count > 1)
            {
                log.Warn("同一个客户匹配了多个客服");
                //返回不是diandian的客服
                return receptionList.First(x => x.CustomerServiceId != Dianzhu.Config.Config.GetAppSetting("DiandianLoginId")).CustomerServiceId;

            }
            else if (receptionList.Count == 1)
            {

                if (!receptionSession.IsUserOnline(receptionList[0].CustomerServiceId))
                {
                    log.Warn("客服应该是在线状态,但是现在不是");
                }
                return receptionList[0].CustomerServiceId;
            }
            else
            {
                var onlineList = receptionSession.GetOnlineSessionUser(Enums.XmppResource.YDBan_CustomerService);
                Dictionary<string, string> assignResult = assignStratage.Assign(new List<string> { customerId }, onlineList.Select(x => x.username).ToList(),
                                    Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"));
                return assignResult[customerId];

            }


        }
        public void ReAssignToOther(string customerServiceId)
        {
            var receptionList = receptionRepository.Find(x => x.CustomerServiceId == customerServiceId);
            //获取当前客户接待的客户
            IList<ReceptionStatus> customerWithCS = receptionRepository.Find(x=>x.CustomerServiceId==customerServiceId);
            var onlineList = receptionSession.GetOnlineSessionUser(Enums.XmppResource.YDBan_CustomerService);
            //re assign
            Dictionary<string, string> newAssign
                = assignStratage.Assign(customerWithCS.Select(x => x.CustomerId).ToList(), onlineList.Select(x => x.username).ToList(),
                Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"));

            foreach (ReceptionStatus status in customerWithCS)
            {
                status.CustomerServiceId = newAssign[status.CustomerId];
                receptionRepository.Update(status);
            }
        }
    }
}
