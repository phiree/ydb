using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.BLL.IdentityAccess;
using System.Threading;
using Ydb.InstantMessage.Application;

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
        IInstantMessage instantMessage;
        Dianzhu.IDAL.IUnitOfWork iuow;
        BLLAdvertisement bllAdv;
        IBLLServiceOrder bllServiceOrder;
        IEncryptService encryptService;
        IDAL.IDALMembership dalMembership;
        public string[] Args { get; set; }
        public LoginPresenter(IView.ILoginForm loginView, IInstantMessage instantMessage, BLLAdvertisement bllAdv,
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
           
                if (Args.Length == 2)
                {
                    string userName = Args[0];
                    string password = Args[1];
                    Login(userName, password);
                AutoResetEvent waitHandle = new AutoResetEvent(false);
                IMLogined eventHandler = delegate (string userid)
                {
                    waitHandle.Set();  // signal that the finished event was raised
                };
                instantMessage.IMLogined += eventHandler;
                waitHandle.WaitOne();
                return true;
                }
            

            return this.loginView.ShowDialog();
        }
        

        public async void Login(string username, string plainPassword)
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
            //Action ac=()=>
            //{

                string encryptPassword = encryptService.GetMD5Hash(plainPassword);
                var member = dalMembership.ValidateUser(username, encryptPassword);
                //DZMembership member = dalme.GetUserByName(loginView.UserName);
                if (member != null && member.UserType == Model.Enums.enum_UserType.customerservice)
                {
                    instantMessage.OpenConnection(member.Id.ToString()
                         , loginView.Password, Model.Enums.enum_XmppResource.YDBan_CustomerService.ToString());
                }
                else
                {
                    XMPP_IMAuthError();
                }
            //};
            //NHibernateUnitOfWork.With.Transaction(ac);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);

        }
       public  void loginView_ViewLogin()
        {

            loginView.LoginButtonText = "正在登录,请稍后";
            loginView.LoginButtonEnabled = false;
            loginView.LoginMessage = string.Empty;

           

            Login(loginView.UserName, loginView.Password);

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
   
        /// <summary>
        /// 登录成功后触发
        /// </summary>
        /// <param name="jidUser"></param>
        void IMLogined(string jidUser)
        {
            Action ac = () =>
            {

                DZMembership customerService = dalMembership.FindById(new Guid(jidUser));

                GlobalViables.CurrentCustomerService = customerService;

                Guid id = new Guid(Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"));
                DZMembership diandian = dalMembership.FindById(id);
                if (diandian == null)
                {
                    log.Error("点点获取失败");
                }
                GlobalViables.Diandian = diandian;
                if (Args.Length == 0)
                {
                    loginView.IsLoginSuccess = true;
                }
            };

            NHibernateUnitOfWork.With.Transaction(ac);



        }

        void loginView_Logined(object sender, EventArgs e)
        {
            loginView.IsLoginSuccess = true;
        }




    }
}
