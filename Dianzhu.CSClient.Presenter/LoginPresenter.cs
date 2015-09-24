using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.CSClient.IInstantMessage;
 
using Dianzhu.Model;
using Dianzhu.BLL;
namespace Dianzhu.CSClient.Presenter
{
   public class LoginPresenter
    {
     
       IVew.ILoginForm loginView;
       InstantMessage instantMessage;
       DZMembershipProvider bllMembership;
       public LoginPresenter(IVew.ILoginForm loginView, InstantMessage instantMessage,

            BLL.DZMembershipProvider bllMembership)
       {
           this.loginView = loginView;
           this.instantMessage = instantMessage;
           loginView.ViewLogin +=new IVew.ViewLogin(loginView_ViewLogin);
           this.bllMembership = bllMembership;

            instantMessage.IMError += new IMError(XMPP_IMError);
            instantMessage.IMConnectionError += new IMConnectionError(instantMessage_IMConenctionError);
            instantMessage.IMLogined += new IMLogined(IMLogined);
            instantMessage.IMAuthError += new IMAuthError(XMPP_IMAuthError);

        }

        BLLReceptionStatus BLLReceptionStatus = new BLLReceptionStatus();

       void loginView_ViewLogin()
       {
           loginView.LoginButtonText = "正在登录,请稍后";
           loginView.LoginButtonEnabled = false;
           loginView.LoginMessage = string.Empty;
             DZMembership member = bllMembership.GetUserByName(loginView.UserName);
           if (member == null)
           {
               XMPP_IMAuthError();
           }
           else
           {
               instantMessage.OpenConnection(member.Id.ToString()
                   , loginView.Password);
           }
           
       }

       void instantMessage_IMConenctionError(string error)
       {
           loginView.LoginButtonEnabled = true;
           loginView.LoginButtonText = "重新登录";
           loginView.ErrorMessage = "服务器错误, 请确保通讯服务器已开启." + error;
           loginView.ShowError();
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

       void IMLogined(string jidUser)
       {
          
           DZMembership customerService = BLLFactory.BLLMembership.GetUserById(new Guid( jidUser));
           GlobalViables.CurrentCustomerService = customerService;
           loginView.IsLoginSuccess = true;
           //
           BLLReceptionStatus.CustomerServiceLogin(customerService);
       }

       void loginView_Logined(object sender, EventArgs e)
       {
           loginView.IsLoginSuccess = true;
        }

       
 

    }
}
