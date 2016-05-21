using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    //用户登录,退出/注销记录
    public interface IBLLMembershipLoginLog
    {
        void MemberLogin(DZMembership memebr,string memo);
        void MemberLogoff(DZMembership member, string memo);
    }
    public class BLLMembershipLoginLog : IBLLMembershipLoginLog
    {
        DALMembershipLoginLog dalLoginLog = new DALMembershipLoginLog();
        /// <summary>
        /// 用户登录记录
        /// </summary>
        /// <param name="memebr"></param>
        /// <param name="memo"></param>
        public void MemberLogin(DZMembership memeber, string memo)
        {
            MembershipLoginLog log = new MembershipLoginLog(memeber, enumLoginLogType.Login, memo);
            dalLoginLog.Save(log);
        }

        public void MemberLogoff(DZMembership member, string memo)
        {
            MembershipLoginLog log = new MembershipLoginLog(member, enumLoginLogType.Logoff, memo);
            dalLoginLog.Save(log);
        }
    }
}
