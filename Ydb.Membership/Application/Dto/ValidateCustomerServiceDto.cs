using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application.Dto
{
    public class ValidateCustomerServiceDto
    {
        //public IList<DZMembershipCustomerServiceDto> NotVerifiedCustomerService { get; set; }

        //public IList<DZMembershipCustomerServiceDto> AgreeVerifiedCustomerService { get; set; }

        //public IList<DZMembershipCustomerServiceDto> RefuseVerifiedCustomerService { get; set; }

        //public IList<DZMembershipCustomerServiceDto> MyCustomerService { get; set; }

        Dictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> VerifiedCustomerService { get; set; }
    }
}
