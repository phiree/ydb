using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Ydb.Common;
using Dianzhu.DAL;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using Dianzhu.CSClient.ViewModel;
using Dianzhu.CSClient.Presenter.VMAdapter;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Application.Dto;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.Application;
using Dianzhu.CSClient.LocalStorage;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 用户列表的控制.
    /// 功能:控制用户按钮的显示样式，显示对应用户的tabContent控件
    /// </summary>
    public class PIdentityList
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PIdentityList");

        IViewIdentityList iView;
        IViewMainForm viewMainForm;

        public PIdentityList(
            IViewIdentityList iView,
            IViewMainForm viewMainForm)
        {
            this.iView = iView;
            this.viewMainForm = viewMainForm;

            iView.IdentityClick += IView_IdentityClick;
        }
        
        public void IView_IdentityClick(string identity)
        {
            try
            {
                IdentityManager.SetCurrentCustomerId(identity);

                string identityFriendly = PHSuit.StringHelper.SafeNameForWpfControl(identity, GlobalViables.PRE_TAB_CUSTOMER);
                viewMainForm.ShowIdentityTab(identityFriendly);
            }
            catch (Exception ex)
            {
                log.Error("IView_IdentityClick Error,skip.");
                log.Error(ex);
            }
        }
        
        public void SetIdentityUnread(string identity,int messageAmount)
        {
            iView.SetIdentityUnread(identity, messageAmount);
        }
        
        public void AddIdentity(VMIdentity vmIdentity)
        {
            iView.AddIdentity(vmIdentity);
        }

        public void RemoveIdentity(string customerId)
        {
            if (iView != null)
            {
                iView.RemoveIdentity(customerId);
            }
        }

    }
}


