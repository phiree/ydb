using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.DomainModel
{
    /// <summary>
    /// 各用户类型的分成比例
    /// </summary>
    public class SharePoint :Entity<Guid>
    {

        public static SharePoint None
        {
            get { return new SharePoint(); }
        }

        public virtual string MembershipId { get; set; }
        public virtual decimal Point { get; set; }
 
    }
}
