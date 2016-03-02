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
     
       IView.ILoginForm loginView;
       InstantMessage instantMessage;
     
       public LoginPresenter(IView.ILoginForm loginView, InstantMessage instantMessage
)
       {

           this.loginView = loginView;
           this.instantMessage = instantMessage;
           loginView.ViewLogin +=new IView.ViewLogin(loginView_ViewLogin);
           
            instantMessage.IMError += new IMError(XMPP_IMError);
            instantMessage.IMConnectionError += new IMConnectionError(instantMessage_IMConenctionError);
            instantMessage.IMLogined += new IMLogined(IMLogined);
            instantMessage.IMAuthError += new IMAuthError(XMPP_IMAuthError);

        }

        

       void loginView_ViewLogin()
       {
           
           loginView.LoginButtonText = "正在登录,请稍后";
           loginView.LoginButtonEnabled = false;
           loginView.LoginMessage = string.Empty;
            BLLFactory bllFactory = new BLLFactory();
            DZMembershipProvider bllmember = bllFactory.BLLMember;

           DZMembership member = bllmember.GetUserByName(loginView.UserName);
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
         
       }

       

       void XMPP_IMError(string error)
       {
           loginView.LoginButtonEnabled = true;
           loginView.LoginButtonText = "重新登录";
           loginView.ErrorMessage = "服务器错误:"+error;
           
           
       }

       void XMPP_IMAuthError()
       {
           loginView.LoginButtonEnabled = true;
           loginView.LoginButtonText = "重新登录";
           loginView.LoginMessage = "用户名/密码错误,请重试.";
       }
        BLLReceptionStatus bllReceptionStatus;
        BLLReceptionStatus BLLReceptionStatus { get
            {
                if(bllReceptionStatus==null)
                    bllReceptionStatus = new BLLReceptionStatus();
                return bllReceptionStatus;
            } } 
        /// <summary>
        /// 登录成功后触发
        /// </summary>
        /// <param name="jidUser"></param>
        void IMLogined(string jidUser)
       {
          
            DZMembership customerService = new BLLFactory().BLLMember.GetUserById(new Guid( jidUser));
            GlobalViables.CurrentCustomerService = customerService;

            Guid id =new Guid(Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"));
            DZMembership diandian = new BLLFactory().BLLMember.GetUserById(id);
            GlobalViables.Diandian = diandian;

            loginView.IsLoginSuccess = true;
            
            //
            

        }

       void loginView_Logined(object sender, EventArgs e)
       {
           loginView.IsLoginSuccess = true;
        }

       
 

    }
}
