using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Reception
{
    /// <summary>
    /// 客服分配策略.
    /// </summary>
    public abstract class AssignStratage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerList"></param>
        /// <param name="csList">当前在线的非点点客服</param>
        /// <param name="diandian"></param>
        /// <returns></returns>
   //     public abstract Dictionary<string,string> Assign(IList<string> customerList, IList<OnlineUserSession> csList, string diandian);
        public abstract Dictionary<string, string> Assign(IList<MemberArea> customerList, IList<MemberArea> csList, string diandian);


    }
}
