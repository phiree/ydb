using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.IView
{
    /// <summary>
    /// 登录界面接口定义
    /// </summary>
    public delegate void ViewLogin();
   
    public interface ILoginForm
    {
        string Version {  set; }
        string UserName { get; }
        string Password { get; }
        string LoginButtonText { set; }
        bool LoginButtonEnabled { set; }
        // when send login (click login button)
        event ViewLogin ViewLogin;
        

        bool IsLoginSuccess { set; }
        string LoginMessage { set; }
        string ErrorMessage { set; }

        bool ShowDialog();

    }
}
