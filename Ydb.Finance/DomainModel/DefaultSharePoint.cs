using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;
using Ydb.Finance.DomainModel.Enums;
namespace Ydb.Finance.DomainModel
{
    public class DefaultSharePoint :Entity<Guid>
    {
        protected DefaultSharePoint() { }
        public DefaultSharePoint(decimal point, UserType userType)
        {
            this.Point = point;
            this.UserType = UserType;
        }
        public static DefaultSharePoint None
        {
            get {//  return new DefaultSharePoint(0.0m, Enums.enum_UserType.admin);
                throw new NotImplementedException();
            }
        }

        public virtual UserType UserType { get; set; }
        public virtual decimal Point { get; set; }

    }
}
