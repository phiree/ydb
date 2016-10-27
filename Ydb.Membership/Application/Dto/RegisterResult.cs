using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application.Dto
{
   public class RegisterResult
    {
        public RegisterResult()
        {
            RegisterSuccess = true;
            SendEmailSuccess = true;
        }
        public bool RegisterSuccess { get; internal set; }
        public string RegisterErrMsg { get; internal set; }
        public bool SendEmailSuccess { get; internal set; }

        public string SendEmailErrMsg { get; internal set; }


    }
}
