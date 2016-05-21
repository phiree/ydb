using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model.Finance
{
    /// <summary>
    /// 分成比例
    /// </summary>
    public class SharePoint
    {
        public static SharePoint None {
            get { return new SharePoint(); }
        }
        public Guid Id { get; set; }
        public DZMembership Membership { get; set; }
        public decimal Point { get; set; }

        /// <summary>
        /// 该用户的分成比例
        /// </summary>
        /// <param name="defaultSharePoint"></param>
        /// <returns></returns>
         
    }
    public class DefaultSharePoint
    {
        public static DefaultSharePoint None {
            get { return new DefaultSharePoint(); }
        }
        public Guid Id { get; set; }
        public Dianzhu.Model.Enums.enum_UserType UserType;
        public decimal Point { get; set; }
    }
}
