using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.CSClient.IInstantMessage;
using agsXMPP;
using Dianzhu.Model;
using Dianzhu.BLL;
namespace Dianzhu.CSClient.Presenter
{
   public class LoginPresenter
    {
       IVew.ILoginForm loginView;
       
       public LoginPresenter(IVew.ILoginForm loginView )
       {
           this.loginView = loginView;
           loginView.ViewLogin +=new IVew.ViewLogin(loginView_ViewLogin);
  
       }


       void loginView_ViewLogin()
       {
           loginView.LoginButtonText = "正在登录,请稍后";
           loginView.LoginButtonEnabled = false;
           loginView.LoginMessage = string.Empty;
           GlobalViables.XMPP.OpenConnection(loginView.UserName               
               , loginView.Password);
           GlobalViables.XMPP.IMLogined += new IMLogined(IMLogined);
           GlobalViables.XMPP.IMAuthError += new IMAuthError(XMPP_IMAuthError);
           GlobalViables.XMPP.IMError += new IMError(XMPP_IMError);
          
           
       }

       

       void XMPP_IMError(string error)
       {
           loginView.LoginButtonEnabled = true;
           loginView.LoginButtonText = "重新登录";
           loginView.ErrorMessage = "服务器错误:"+error;
           loginView.ShowError();
           
       }

       void XMPP_IMAuthError()
       {
           loginView.LoginButtonEnabled = true;
           loginView.LoginButtonText = "重新登录";
           loginView.LoginMessage = "用户名/密码错误,请重试.";
       }

       void IMLogined()
       {
           BLL.DZMembershipProvider bllMembership = new BLL.DZMembershipProvider();
           DZMembership customerService = BLLFactory.BLLMembership.GetUserByName(loginView.UserName);
           GlobalViables.CurrentCustomerService = customerService;
           loginView.IsLoginSuccess = true;
       }

       void loginView_Logined(object sender, EventArgs e)
       {
           loginView.IsLoginSuccess = true;
        }

       
 

    }
}
