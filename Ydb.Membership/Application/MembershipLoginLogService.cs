using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Repository;

namespace Ydb.Membership.Application
{
    public class MembershipLoginLogService:IMembershipLoginLogService
    {
        IRepositoryMembershipLoginLog repositoryMembershipLoginLog;

        public MembershipLoginLogService(IRepositoryMembershipLoginLog repositoryMembershipLoginLog)
        {
            this.repositoryMembershipLoginLog = repositoryMembershipLoginLog;
        }
        /// <summary>
        /// 用户登录记录
        /// </summary>
        /// <param name="memebr"></param>
        /// <param name="memo"></param>
        public void MemberLogin(string memeberId, string memo)
        {
            MembershipLoginLog log = new MembershipLoginLog(memeberId, enumLoginLogType.Login, memo);
            repositoryMembershipLoginLog.Add(log);
        }

        public void MemberLogoff(string memberId, string memo)
        {
            MembershipLoginLog log = new MembershipLoginLog(memberId, enumLoginLogType.Logoff, memo);
            repositoryMembershipLoginLog.Add(log);
        }
    }
}
