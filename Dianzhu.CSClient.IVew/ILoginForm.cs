using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.IVew
{
    public  delegate void ViewLogin();
   public interface ILoginForm
    {
       string UserName { get;   }
       string Password { get;   }
       string LoginButtonText { set; }
       bool LoginButtonEnabled { set; }
        // when send login (click login button)
       event ViewLogin ViewLogin;
       //when xmpp authfailed
        
       bool IsLoginSuccess { set; }
       string LoginMessage { set; }
       string ErrorMessage { get; set; }
       void ShowError();
    }
}
