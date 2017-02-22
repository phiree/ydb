using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application.Dto
{
    public class DZMembershipCustomerServiceDto:MemberDto
    {
        public  IList<DZMembershipImageDto> DZMembershipImages { get; set; }
        public  string ApplyMemo { get; set; }
        public  DateTime ApplyTime { get; set; }
        public  DateTime VerifyTime { get; set; }
        public  string RefuseReason { get; set; }
        public  bool IsAgentCustomerService { get; set; }
        public  bool IsVerified { get; set; }
        public  DateTime LockTime { get; set; }
        public  bool IsLocked { get; set; }
        public  string LockReason { get; set; }
        public DZMembershipImageDto DZMembershipDiploma { get; set; }
        public  IList<DZMembershipImageDto> DZMembershipCertificates { get; set; }
        public  IList<DZMembershipImageDto> DZMembershipOthers { get; set; }
    }
}
