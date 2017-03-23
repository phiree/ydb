using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InstantMessage.DomainModel.Reception
{
    /// <summary>
    /// 待分配的用户
    /// </summary>
    public class MemberArea
    {
        public MemberArea(string memberId, string areaId)
        {
            this.MemberId = memberId;
            this.AreaId = areaId;
        }
        public string MemberId { get; internal set; }
        /// <summary>
        /// 用户所属区域.
        /// </summary>
        public string AreaId { get; internal set; }
    }
  
}
