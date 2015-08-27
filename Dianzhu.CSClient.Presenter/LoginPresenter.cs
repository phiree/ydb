using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.CSClient.IInstantMessage;
using agsXMPP;
namespace Dianzhu.CSClient.Presenter
{
   public class LoginPresenter
    {
       IVew.ILoginForm loginView;
       IXMPP xmpp;
       public LoginPresenter(IVew.ILoginForm loginView,IXMPP xmpp)
       {
           this.loginView = loginView;
           this.xmpp = xmpp;
           loginView.LoginHandler += new EventHandler(loginView_LoginHandler);
           
       }

       void xmpp_OnLogin(object sender)
       {
           loginView.IsLoginSuccess = true;
       }

       void loginView_LoginHandler(object sender, EventArgs e)
       {
           xmpp.OpenConnection(loginView.UserName               
               , loginView.Password);
           xmpp.OnLogin+=new ObjectHandler(xmpp_OnLogin);
       }

       void loginView_Logined(object sender, EventArgs e)
       {
           loginView.IsLoginSuccess = true;
       }

       void loginView_ViewLogin(object sender, EventArgs e)
       {
           
       }

       public void Login()
       {
           
       }
       //$xmpp

    }
}
