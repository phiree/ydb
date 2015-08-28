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
           
           loginView.LoginHandler += new EventHandler(loginView_LoginHandler);
           
       }

       void xmpp_OnLogin(object sender)
       {
           BLL.DZMembershipProvider bllMembership = new BLL.DZMembershipProvider();
           DZMembership customerService = BLLFactory.BLLMembership.GetUserByName(loginView.UserName);
           GlobalViables.CurrentCustomerService = customerService;
           loginView.IsLoginSuccess = true;
       }

       void loginView_LoginHandler(object sender, EventArgs e)
       {
           GlobalViables.XMPP.OpenConnection(loginView.UserName               
               , loginView.Password);
           GlobalViables.XMPP.OnLogin += new ObjectHandler(xmpp_OnLogin);
       }

       void loginView_Logined(object sender, EventArgs e)
       {
           loginView.IsLoginSuccess = true;
        }

       
 

    }
}
