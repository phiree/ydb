using Castle.MicroKernel.Registration;
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
using Ydb.BusinessResource.Application;

namespace Dianzhu.CSClient
{
    public class InstallerUI : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Component.For<Presenter.PMain>());
            container.Register(Component.For<CSClient.Presenter.LoginPresenter>());
            container.Register(Component.For<CSClient.Presenter.IdentityManager>());
            container.Register(Component.For<CSClient.Presenter.PIdentityList>().LifestyleTransient());
            container.Register(Component.For<CSClient.Presenter.PChatList>().LifestyleTransient());
            container.Register(Component.For<CSClient.Presenter.PChatSend>().LifestyleTransient());
            container.Register(Component.For<CSClient.Presenter.PToolsControl>().LifestyleTransient());
            container.Register(Component.For<CSClient.Presenter.POrderHistory>().LifestyleTransient());

            container.Register(Component.For<CSClient.Presenter.PSearch>().LifestyleTransient());

            container.Register(Component.For<IViewMainForm>().ImplementedBy<ViewWPF.FormMain>());
            container.Register(Component.For<ILoginForm>().ImplementedBy<ViewWPF.FormLogin>());
            container.Register(Component.For<IViewChatList>().ImplementedBy<ViewWPF.UC_ChatList>().LifestyleTransient());
            container.Register(Component.For<IViewChatSend>().ImplementedBy<ViewWPF.UC_ChatSend>().LifestyleTransient());
            container.Register(Component.For<IViewIdentityList>().ImplementedBy<ViewWPF.UC_IdentityList>());
            container.Register(Component.For<IViewNotice>().ImplementedBy<ViewWPF.UC_Notice>());
            container.Register(Component.For<IViewOrderHistory>().ImplementedBy<ViewWPF.UC_OrderHistory>().LifestyleTransient());
            container.Register(Component.For<IViewSearch>().ImplementedBy<ViewWPF.UC_Search>().LifestyleTransient());
            container.Register(Component.For<IViewSearchResult>().ImplementedBy<ViewWPF.UC_SearchResult>().LifestyleTransient());
            container.Register(Component.For<IViewToolsControl>().ImplementedBy<ViewWPF.UC_TabControlTools>().LifestyleTransient());
            container.Register(Component.For<IViewFormShowMessage>().ImplementedBy<ViewWPF.FormShowMessage>());
            container.Register(Component.For<IViewCustomer>().ImplementedBy<ViewWPF.UC_Customer>().LifestyleTransient());
            container.Register(Component.For<IViewTabContent>().ImplementedBy<ViewWPF.UC_TabContent>().LifestyleTransient());

            container.Register(Component.For<Presenter.VMAdapter.IVMChatAdapter>().ImplementedBy<Presenter.VMAdapter.VMChatAdapter>());
            container.Register(Component.For<Presenter.VMAdapter.IVMIdentityAdapter>().ImplementedBy<Presenter.VMAdapter.VMIdentityAdatper>());
            container.Register(Component.For<Presenter.VMAdapter.IVMOrderHistoryAdapter>().ImplementedBy<Presenter.VMAdapter.VMOrderHistoryAdapter>()); 
            
            container.Register(Component.For<IViewTabContentTimer>().ImplementedBy<ViewWPF.UC_TabContentTimer>().LifestyleTransient());

            container.Register(Component.For<IViewTypeSelect>().ImplementedBy<ViewWPF.UC_TypeSelect>().LifestyleTransient());


        }
    }
}
