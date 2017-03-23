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
        /// <param name="existedReceptionForCustomer">已经存在的分配关系. 用户修改城市之后需要移除该关系.</param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string AssignCustomerLogin(IList<ReceptionStatus> existedReceptionForCustomer,MemberArea customerToAssign,IList<MemberArea> csList)
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

            if (string.IsNullOrEmpty(csId))
            {
              //  var onlineList = receptionSession.GetOnlineSessionUser(XmppResource.YDBan_CustomerService);
                Dictionary<string, string> assignResult = assignStratage.Assign(new List<MemberArea> { customerToAssign }, csList, DianDianId);
                csId = assignResult[customerToAssign.MemberId];
            }

            return csId;
        }

        public Dictionary<string, string> AssignCSLogin(IList<ReceptionStatus> existReceptionDD, MemberArea cs)
        {
            Dictionary<string, string> assignList = new Dictionary<string, string>();

            foreach ( var item in existReceptionDD.Where(x=>x.AreaId==cs.AreaId))
            {
                assignList[item.CustomerId] = cs.MemberId;
            }

            return assignList;
        }

        public Dictionary<string, string> AssignCSLogoff(IList<ReceptionStatus> existedReceptionForCustomerService,IList<MemberArea> csList)
        {
            Dictionary<string, string> assignList = new Dictionary<string, string>();

          //  var onlineList = receptionSession.GetOnlineSessionUser(XmppResource.YDBan_CustomerService);
            var beleftCustomer = existedReceptionForCustomerService.Select(x => new MemberArea(x.CustomerId,x.AreaId)).ToList();

            assignList = assignStratage.Assign(beleftCustomer, csList, DianDianId);

            return assignList;
        }

        
    }
}
