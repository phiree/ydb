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

            container.Register(Component.For<IUnitOfWork>().ImplementedBy<NHUnitOfWork>());

        }
    }
    public class InstallerApplicationService : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
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
            container.Register(Component.For<CSClient.IMessageAdapter.IAdapter>().ImplementedBy<CSClient.MessageAdapter.MessageAdapter>());

            //instantmessage
            string server = Config.Config.GetAppSetting("ImServer");
            string domain = Config.Config.GetAppSetting("ImDomain");
            container.Register(Component.For<CSClient.IInstantMessage.InstantMessage>().ImplementedBy<Dianzhu.CSClient.XMPP.XMPP>()
                                .DependsOn(
                                  Dependency.OnValue("server", server)
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