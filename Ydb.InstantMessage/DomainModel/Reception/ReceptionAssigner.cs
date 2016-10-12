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

        /// <summary>
        /// 用户上线分配，如果返回为空，则分配失败
        /// </summary>
        /// <param name="existedReceptionForCustomer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string AssignCustomerLogin(IList<ReceptionStatus> existedReceptionForCustomer,string customerId)
        {
            string csId = string.Empty;
            
            if (existedReceptionForCustomer.Count >= 1)
            {
                if (existedReceptionForCustomer.Count > 1)
                {
                    log.Warn("用户有多个分配");
                }

                foreach(var item in existedReceptionForCustomer)
                {
                    if (receptionSession.IsUserOnline(item.CustomerServiceId) && item.CustomerServiceId!= DianDianId)
                    {
                        csId = item.CustomerServiceId;
                    }
                }
            }

            if(string.IsNullOrEmpty(csId))
            {
                try
                {
                    var onlineList = receptionSession.GetOnlineSessionUser(XmppResource.YDBan_CustomerService);
                    Dictionary<string, string> assignResult = assignStratage.Assign(new List<string> { customerId }, onlineList, DianDianId);
                    csId = assignResult[customerId];
                }
                catch (Exception ee)
                {
                    PHSuit.ExceptionLoger.ExceptionLog(log, ee);
                }
            }

            return csId;
        }

        public Dictionary<string, string> AssignCSLogin(IList<ReceptionStatus> existReceptionDD, string csId)
        {
            Dictionary<string, string> assignList = new Dictionary<string, string>();

            foreach (var item in existReceptionDD)
            {
                assignList[item.CustomerId] = csId;
            }

            return assignList;
        }

        public Dictionary<string, string> AssignCSLogoff(IList<ReceptionStatus> existedReceptionForCustomerService)
        {
            Dictionary<string, string> assignList = new Dictionary<string, string>();

            try
            {
                var onlineList = receptionSession.GetOnlineSessionUser(XmppResource.YDBan_CustomerService);
                assignList = assignStratage.Assign(existedReceptionForCustomerService.Select(x => x.CustomerId).ToList(), onlineList, DianDianId);
            }
            catch (Exception ee)
            {
                PHSuit.ExceptionLoger.ExceptionLog(log, ee);
                throw ee;
            }

            return assignList;
        }

        
    }
}
