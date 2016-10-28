using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application.Dto
{
   public class ValidateResult
    {

        public ValidateResult()
        {
            IsValidated = true;
        }
        public bool IsValidated { get; internal set; }
        public string ValidateErrMsg { get; internal set; }
        public MemberDto ValidatedMember { get; internal set; }
       


    }
}
