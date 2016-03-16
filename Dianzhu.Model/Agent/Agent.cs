using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 代理商
    /// 代理商发展当地线下店铺
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// 代理商代理的区域
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// 发展的商家
        /// </summary>
        public IList<Business_Abs> Businesses { get; set; }
        /// <summary>
        /// 分润
        /// </summary>
        public decimal ProfitPercent { get; set; }
        /// <summary>
        /// 个人信息
        /// </summary>
        public AgentMemberInfo MemberInfo { get; set; }


    }
    /// <summary>
    /// 代理商个人信息
    /// </summary>
    public class AgentMemberInfo
    {

    }
}
