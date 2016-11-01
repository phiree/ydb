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
        void MemberLogin(string memebrId,string memo);
        void MemberLogoff(string memberId, string memo);
    }
    public class BLLMembershipLoginLog : IBLLMembershipLoginLog
    {
        IDAL.IDALMembershipLoginLog dalLoginLog;

        public BLLMembershipLoginLog(IDAL.IDALMembershipLoginLog dal)
        {
            dalLoginLog = dal;
        }

        /// <summary>
        /// 用户登录记录
        /// </summary>
        /// <param name="memebr"></param>
        /// <param name="memo"></param>
        public void MemberLogin(string memeberId, string memo)
        {
            MembershipLoginLog log = new MembershipLoginLog(memeberId, enumLoginLogType.Login, memo);
            dalLoginLog.Add(log);
        }

        public void MemberLogoff(string memberId, string memo)
        {
            MembershipLoginLog log = new MembershipLoginLog(memberId, enumLoginLogType.Logoff, memo);
            dalLoginLog.Add(log);
        }
    }
}
