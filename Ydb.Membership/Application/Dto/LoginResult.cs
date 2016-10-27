using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application.Dto
{
   public class LoginResult
    {

        public LoginResult()
        {
            LoginSuccess = true;
        }
        public bool LoginSuccess { get; internal set; }
        public string LoginErrMsg { get; internal set; }
       


    }
}
