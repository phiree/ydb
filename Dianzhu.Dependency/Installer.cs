using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.BLL;
using NHibernate;
using System.Configuration;
using nhf = FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Dianzhu.BLL.Finance;
using Castle.MicroKernel.SubSystems.Configuration;

namespace Dianzhu.DependencyInstaller
{
    public class InstallerComponent : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<BLLAdvertisement>());
            container.Register(Component.For<BLLArea>());
            container.Register(Component.For<DZMembershipProvider>());
            container.Register(Component.For<BLLBusiness>());
            container.Register(Component.For<BLLBusinessImage>());
            container.Register(Component.For<BLLRefund>());
            container.Register(Component.For<PushService>());
            container.Register(Component.For<BLLPay>());
            // container.Register(Component.For<OrderShare>());

            //20160614_longphui_add
            container.Register(Component.For<BLLComplaint>());

            //20160615_longphui_add
            container.Register(Component.For<BLLDeviceBind>());

            //20160616_longphui_add
            container.Register(Component.For<BLLServiceOrderRemind>());

        }
    }
    public class InstallerRepository : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(NHRepositoryBase<,>)));
            container.Register(Component.For<IRepository<Advertisement, Guid>, IDALAdvertisement>().ImplementedBy<DALAdvertisement>());
            container.Register(Component.For<IRepository<Area, int>, IDALArea>().ImplementedBy<DALArea>());
            container.Register(Component.For<IRepository<ServiceOrder, Guid>, IDALServiceOrder>().ImplementedBy<DALServiceOrder>());
            container.Register(Component.For<IRepository<Refund, Guid>, IDALRefund>().ImplementedBy<DALRefund>());
            container.Register(Component.For<IRepository<DZMembership, Guid>, IDALMembership>().ImplementedBy<DALMembership>());
            container.Register(Component.For<IRepository<Business, Guid>, IDALBusiness>().ImplementedBy<DALBusiness>());
            container.Register(Component.For<IRepository<BusinessImage, Guid>, IDALBusinessImage>().ImplementedBy<DALBusinessImage>());
            container.Register(Component.For<IRepository<IMUserStatus, Guid>, IDALIMUserStatus>().ImplementedBy<DALIMUserStatus>());

            container.Register(Component.For<IUnitOfWork>().ImplementedBy<NHUnitOfWork>());

            //20160614_longphui_add
            container.Register(Component.For<IRepository<Complaint, Guid>, IDALComplaint>().ImplementedBy<DALComplaint>());

            //20160615_longphui_add
            container.Register(Component.For<IRepository<DeviceBind, Guid>, IDALDeviceBind>().ImplementedBy<DALDeviceBind>());

            //20160616_longphui_add
            container.Register(Component.For<IRepository<ServiceOrderRemind, Guid>, IDALServiceOrderRemind>().ImplementedBy<DALServiceOrderRemind>());
        }
    }
    public class InstallerApplicationService : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //finance
            container.Register(Component.For<IBLLSharePoint>().ImplementedBy< BLLSharePoint>());
            container.Register(Component.For<IBLLServiceTypePoint>().ImplementedBy<BLLServiceTypePoint>());
            container.Register(Component.For<IOrderShare>().ImplementedBy<OrderShare>());
            container.Register(Component.For<IBalanceFlowService>().ImplementedBy<BalanceFlowService>());
            //agent
            container.Register(Component.For<Dianzhu.BLL.Agent.IAgentService>().ImplementedBy<Dianzhu.BLL.Agent.AgentService>());

            container.Register(Component.For<IBLLMembershipLoginLog>().ImplementedBy<BLLMembershipLoginLog>());
            container.Register(Component.For<IIMSession>().ImplementedBy<IMSessionsDB>());
            container.Register(Component.For<IIMSession>().ImplementedBy<IMSessionsOpenfire>()
                                .DependsOn(Dependency.OnValue("restApiUrl", Dianzhu.Config.Config.GetAppSetting("OpenfireRestApiSessionListUrl")))
                                .DependsOn(Dependency.OnValue("restApiSecretKey", Dianzhu.Config.Config.GetAppSetting("OpenfireRestApiAuthKey"))) 
                );
            container.Register(Component.For<CashTicketAssigner_Task>());
            container.Register(Component.For<IBLLServiceOrder>().ImplementedBy<BLLServiceOrder>()
                               .DependsOn(Dependency.OnValue("bllServiceOrderStateChangeHis", new BLLServiceOrderStateChangeHis()))
                               .DependsOn(Dependency.OnValue("bllPayment", new BLLPayment()))
                               .DependsOn(Dependency.OnValue("bllClaims",new BLLClaims()))
                               
                               )
                               ;


        }
    }
    public class InstallerInfrstructure : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //iadapter
            container.Register(Component.For<CSClient.MessageAdapter.MessageAdapter,CSClient.IMessageAdapter.IAdapter>().ImplementedBy<CSClient.MessageAdapter.MessageAdapter>());

            //instantmessage
            string server = Config.Config.GetAppSetting("ImServer");
            string domain = Config.Config.GetAppSetting("ImDomain");
            container.Register(Component.For<CSClient.IInstantMessage.InstantMessage>().ImplementedBy<Dianzhu.CSClient.XMPP.XMPP>()
                                .DependsOn(
                                    
                                  Dependency.OnValue("server", server),
                                  Dependency.OnValue("messageAdapter", container.Resolve<CSClient.IMessageAdapter.IAdapter>())
                                , Dependency.OnValue("domain", domain)
                                , Dependency.OnValue("resourceName", Model.Enums.enum_XmppResource.YDBan_CustomerService.ToString())
                                )
                            );
            container.Register(Component.For<Dianzhu.BLL.IEmailService>().ImplementedBy<JSYK.Infrastructure.EmailService>());
            container.Register(Component.For<Dianzhu.BLL.IEncryptService>().ImplementedBy<JSYK.Infrastructure.EncryptService>());


        }
    }
    /// <summary>
    /// Summary description for Installer
    /// </summary>
 
}