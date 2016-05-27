using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.CSClient.IInstantMessage;

using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.BLL.IdentityAccess;
namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// ddd:  在应用程序中暴露领域是个严重错误..应该通过 ApplicationService接口和 Dto与之沟通.
    /// 
    /// </summary>
    public class LoginPresenter
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.LoginPresenter");
        IView.ILoginForm loginView;
        InstantMessage instantMessage;
        Dianzhu.IDAL.IUnitOfWork iuow;
        BLLAdvertisement bllAdv;
        IBLLServiceOrder bllServiceOrder;
        IEncryptService encryptService;
        IDAL.IDALMembership dalMembership;
        public LoginPresenter(IView.ILoginForm loginView, InstantMessage instantMessage, BLLAdvertisement bllAdv,
            IBLLServiceOrder bllServiceOrder,IDAL.IDALMembership dalMembership 
            ,IEncryptService encryptService
            //,Dianzhu.IDAL.IUnitOfWork iuow
 )
        {
            this.encryptService = encryptService;
            this.dalMembership = dalMembership;
           // this.loginService = loginService;
            this.bllServiceOrder = bllServiceOrder;
            this.loginView = loginView;
            this.instantMessage = instantMessage;
            this.bllAdv = bllAdv;
            loginView.ViewLogin += new IView.ViewLogin(loginView_ViewLogin);
            loginView.TestClick += LoginView_TestClick;
            instantMessage.IMError += new IMError(XMPP_IMError);
            instantMessage.IMConnectionError += new IMConnectionError(instantMessage_IMConenctionError);
            instantMessage.IMLogined += new IMLogined(IMLogined);
            instantMessage.IMAuthError += new IMAuthError(XMPP_IMAuthError);
           // this.iuow= iuow;
        }

        private void LoginView_TestClick(object sender, EventArgs e)
        {

            long totalRecord;
            var advList = bllAdv.GetADList(1, 10, out totalRecord);
            var orderList = bllServiceOrder.GetAll(1,10,out totalRecord);
            foreach (ServiceOrder order in orderList)
            {
                var amount = order.OrderAmount;
                foreach (ServiceOrderDetail d in order.Details)
                {
                    var damount = d.OriginalService.DepositAmount;
                }
            }

        }

        public bool ShowDialog()
        {
            return this.loginView.ShowDialog();
        }


       public  void loginView_ViewLogin()
        {

            loginView.LoginButtonText = "正在登录,请稍后";
            loginView.LoginButtonEnabled = false;
            loginView.LoginMessage = string.Empty;
            string encryptPassword = encryptService.GetMD5Hash(loginView.Password);
              var member=dalMembership.ValidateUser(loginView.UserName, encryptPassword);
            //DZMembership member = dalme.GetUserByName(loginView.UserName);
            if (member != null)
            {
                instantMessage.OpenConnection(member.Id.ToString()
                      , loginView.Password);
            }
            else
            {
                XMPP_IMAuthError();
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
            loginView.ErrorMessage = "服务器错误:" + error;


        }

        void XMPP_IMAuthError()
        {
            loginView.LoginButtonEnabled = true;
            loginView.LoginButtonText = "重新登录";
            loginView.LoginMessage = "用户名/密码错误,请重试.";
        }
        BLLReceptionStatus bllReceptionStatus;
        BLLReceptionStatus BLLReceptionStatus
        {
            get
            {
                if (bllReceptionStatus == null)
                    bllReceptionStatus = new BLLReceptionStatus();
                return bllReceptionStatus;
            }
        }
        /// <summary>
        /// 登录成功后触发
        /// </summary>
        /// <param name="jidUser"></param>
        void IMLogined(string jidUser)
        {

            DZMembership customerService = dalMembership.FindById(new Guid(jidUser));
            //GlobalViables.CurrentCustomerService = customerService;
            GlobalViables.CurrentCustomerService = customerService;

            Guid id = new Guid(Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"));
            DZMembership diandian = dalMembership.FindById(id);
            if (diandian == null)
            {
                log.Error("点点获取失败");
            }
            GlobalViables.Diandian = diandian;
            loginView.IsLoginSuccess = true;
        }

        void loginView_Logined(object sender, EventArgs e)
        {
            loginView.IsLoginSuccess = true;
        }




    }
}
