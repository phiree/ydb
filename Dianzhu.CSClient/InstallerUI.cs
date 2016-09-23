﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IView;
using Dianzhu.BLL;
using Dianzhu.IDAL;

namespace Dianzhu.CSClient
{
    public class InstallerUI : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Component.For<Presenter.PMain>());
            container.Register(Component.For<CSClient.Presenter.LoginPresenter>());
            container.Register(Component.For<CSClient.Presenter.IdentityManager>());
            container.Register(Component.For<CSClient.Presenter.PIdentityList>());
            container.Register(Component.For<CSClient.Presenter.PChatList>());
            container.Register(Component.For<CSClient.Presenter.PChatSend>());
            container.Register(Component.For<CSClient.Presenter.PTabControl>());
            container.Register(Component.For<CSClient.Presenter.POrderHistory>());
 
            container.Register(Component.For<CSClient.Presenter.PSearch>().DependsOn(
               
                Dependency.OnValue("bllPushService", new PushService(container.Resolve<IDALServiceOrderPushedService>(), container.Resolve<IBLLServiceOrder>(),new BLLPayment(container.Resolve<IDALPayment>(),container.Resolve<IDALClaims>()),new BLLServiceOrderStateChangeHis(container.Resolve<IDALServiceOrderStateChangeHis>()))),
 
                Dependency.OnValue("bllReceptionStatus", container.Resolve<BLLReceptionStatus>())

                ));

            container.Register(Component.For<IView.IViewMainForm>().ImplementedBy<ViewWPF.FormMain>());
            container.Register(Component.For<IView.ILoginForm>().ImplementedBy<ViewWPF.FormLogin>());
            container.Register(Component.For<IViewChatList>().ImplementedBy<ViewWPF.UC_ChatList>());
            container.Register(Component.For<IViewChatSend>().ImplementedBy<ViewWPF.UC_ChatSend>());
            container.Register(Component.For<IViewIdentityList>().ImplementedBy<ViewWPF.UC_IdentityList>());
            container.Register(Component.For<IViewNotice>().ImplementedBy<ViewWPF.UC_Notice>());
            container.Register(Component.For<IViewOrderHistory>().ImplementedBy<ViewWPF.UC_OrderHistory>());
            container.Register(Component.For<IViewSearch>().ImplementedBy<ViewWPF.UC_Search>());
            container.Register(Component.For<IViewSearchResult>().ImplementedBy<ViewWPF.UC_SearchResult>());
            container.Register(Component.For<IViewTabControl>().ImplementedBy<ViewWPF.UC_TabControlTools>());
            container.Register(Component.For<IViewFormShowMessage>().ImplementedBy<ViewWPF.FormShowMessage>());
            container.Register(Component.For<IViewCustomer>().ImplementedBy<ViewWPF.UC_Customer>());

            container.Register(Component.For<Presenter.VMAdapter.IVMChatAdapter>().ImplementedBy<Presenter.VMAdapter.VMChatAdapter>());
        }
    }
}
