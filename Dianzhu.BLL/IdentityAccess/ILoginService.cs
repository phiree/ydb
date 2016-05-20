using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.IdentityAccess
{
   public  interface ILoginService
    {
        LoginDto  Login(LoginDto loginDto);
    }
}
