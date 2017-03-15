
using Dianzhu.CSClient.IView;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Membership.Application;

namespace Dianzhu.CSClient.Presenter
{
    public class PMain
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PMain");
        
        IViewMainForm viewMainForm;
        IViewFormShowMessage viewFormShowMessage;

        IInstantMessage iIM;
        IMembershipLoginLogService bllLoginLog;

        public PMain(
            IViewMainForm viewMainForm,
            IInstantMessage iIM,
            IMembershipLoginLogService bllLoginLog,
            IViewFormShowMessage viewFormShowMessage)
        {
            this.viewMainForm = viewMainForm;
            this.viewMainForm.CSName = GlobalViables.CurrentCustomerService.NickName;
            this.iIM = iIM;
            this.bllLoginLog = bllLoginLog;
            this.viewFormShowMessage = viewFormShowMessage;

            iIM.IMStreamError += IIM_IMStreamError;
            iIM.IMClosed += IIM_IMClosed;
            
        }

       

        public IViewMainForm Form
        {
            get
            {
                return viewMainForm;
            }
        }


        private void IIM_IMClosed()
        {
            log.Debug("openfire已下线");
            this.viewFormShowMessage.Message = "网络出现异常，请重新登录";
            this.viewFormShowMessage.ShowDialog();
            this.viewMainForm.CloseApplication();
        }

        private void IIM_IMStreamError()
        {
            this.viewFormShowMessage.Message = "错误.同一用户在其他客户端登录,您已被迫下线";
            this.viewFormShowMessage.ShowDialog();
            CloseApplication();
        }
        
        public void CloseApplication()
        {
            bllLoginLog.MemberLogoff(GlobalViables.CurrentCustomerService.Id.ToString(), string.Empty);

            viewMainForm.CloseApplication();
        }

        public void AddIdentityTabContent(string identityTabFriendly,IViewTabContent viewTabContent)
        {
            viewMainForm.AddIdentityTab(identityTabFriendly, viewTabContent);
        }

        public void RemoveIdentityTabContent(string identityTabFriendly)
        {
            string currentFriendlyName =
                PHSuit.StringHelper.SafeNameForWpfControl(IdentityManager.CurrentCustomerId,
                    GlobalViables.PRE_TAB_CUSTOMER);

            bool isActived = currentFriendlyName == identityTabFriendly;
            viewMainForm.RemoveIdentityTab(identityTabFriendly,isActived);
        }
    }
}
