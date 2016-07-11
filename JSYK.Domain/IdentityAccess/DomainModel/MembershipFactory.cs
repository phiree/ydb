using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model.IdentityAccess
{
   public  abstract class MembershipFactory
    {
        public void CreateAuthInfo()
        {

        }
        public abstract void Create(DZMembership membership);
    }
   
     
}
