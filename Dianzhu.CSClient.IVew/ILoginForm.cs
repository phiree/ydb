using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.IVew
{
   public interface ILoginForm
    {
       string UserName { get;   }
       string Password { get;   }
        // when send login (click login button)
       event EventHandler LoginHandler;
       //when xmpp authfailed
        
       bool IsLoginSuccess { set; }
    }
}
