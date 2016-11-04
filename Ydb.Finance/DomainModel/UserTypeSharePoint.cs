using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Finance.DomainModel.Enums;
namespace Ydb.Finance.DomainModel
{
    /// <summary>
    /// 用户类型的分成比
    /// </summary>
    public class UserTypeSharePoint :Entity<Guid>
    {
        protected UserTypeSharePoint() { }
        public UserTypeSharePoint(decimal point, UserType userType)
        {
            this.Point = point;
            this.UserType = userType;
        }
        public static UserTypeSharePoint None
        {
            get {
                return new UserTypeSharePoint(0.0m, Enums.UserType.None);
                
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual UserType UserType { get; set; }

        /// <summary>
        /// 分账比例
        /// </summary>
        public virtual decimal Point { get; set; }

    }
}
