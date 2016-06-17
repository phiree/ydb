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
 
            
            container.Register(Component.For<BLLDZService>());
            container.Register(Component.For<BLLDZTag>());
            container.Register(Component.For<BLLOrderAssignment>());
            container.Register(Component.For<BLLComplaint>());
            container.Register(Component.For<BLLPayment>());
            container.Register(Component.For<BLLDeviceBind>());
            container.Register(Component.For<BLLReceptionChat>());
            container.Register(Component.For<BLLReceptionChatDD>());
            container.Register(Component.For<BLLClaims>());
            container.Register(Component.For<BLLServiceOrderAppraise>());
            container.Register(Component.For<BLLServiceOrderStateChangeHis>());

            // , BLLPayment bllPayment,BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis
 
 

       

            //20160616_longphui_add
            container.Register(Component.For<BLLServiceOrderRemind>());
          
            container.Register(Component.For<BLLStaff>());

        }
    }
    public class InstallerRepository : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {


            //todo: Registering components by conventions https://github.com/castleproject/Windsor/blob/master/docs/registering-components-by-conventions.md
            container.Register(Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(NHRepositoryBase<,>)));

            container.Register(Component.For<IRepository<Advertisement, Guid>, IDALAdvertisement>().ImplementedBy<DALAdvertisement>());
            container.Register(Component.For<IRepository<Area, int>, IDALArea>().ImplementedBy<DALArea>());
 

            container.Register(Component.For<IRepository<Business, Guid>, IDALBusiness>().ImplementedBy<DALBusiness>());
            container.Register(Component.For<IRepository<BusinessImage, Guid>, IDALBusinessImage>().ImplementedBy<DALBusinessImage>());

            container.Register(Component.For<IRepository<CashTicket, Guid>, IDALCashTicket>().ImplementedBy<DALCashTicket>());
            container.Register(Component.For<IRepository<CashTicketAssignDetail, Guid>>().ImplementedBy<DALCashTicketAssignDetail>());
            container.Register(Component.For<IRepository<CashTicketAssignRecord, Guid>>().ImplementedBy<DALCashTicketAssignRecord>());
            container.Register(Component.For<IRepository<CashTicketCreateRecord, Guid>, IDALCashTicketCreateRecord>().ImplementedBy<DALCashTicketCreateRecord>());
            container.Register(Component.For<IRepository<CashTicketTemplate, Guid>, IDALCashTicketTemplate>().ImplementedBy<DALCashTicketTemplate>());
            container.Register(Component.For<IRepository<Claims, Guid>>().ImplementedBy<DALClaims>());
            container.Register(Component.For<IRepository<Complaint, Guid>>().ImplementedBy<DALComplaint>());
            //dddddddddddd
            container.Register(Component.For<IRepository<DeviceBind, Guid>, IDALDeviceBind>().ImplementedBy<DALDeviceBind>());
            container.Register(Component.For<IRepository<DZService, Guid>, IDALDZService>().ImplementedBy<DALDZService>());
            container.Register(Component.For<IRepository<DZTag, Guid>, IDALDZTag>().ImplementedBy<DALDZTag>());
            container.Register(Component.For<IRepository<DZMembership, Guid>, IDAL.IDALMembership>().ImplementedBy<DALMembership>());
            //mmmmmmmmmm
            container.Register(Component.For<IRepository<MembershipLoginLog, Guid>>().ImplementedBy<DALMembershipLoginLog>());
            //iiiiiiiiii

            container.Register(Component.For<IRepository<IMUserStatus, Guid>, IDALIMUserStatus>().ImplementedBy<DALIMUserStatus>());
            container.Register(Component.For<IRepository<IMUserStatusArchieve, Guid>>().ImplementedBy<DAL.DALIMUserStatusArchieve>());
           //ooooooo
            container.Register(Component.For<IRepository<OrderAssignment, Guid>, IDALOrderAssignment>().ImplementedBy<DALOrderAssignment>());
            //ppppppppp
            container.Register(Component.For<IRepository<Payment, Guid>, IDALPayment>().ImplementedBy<DALPayment>());
            container.Register(Component.For<IRepository<PaymentLog, Guid>, IDALPaymentLog>().ImplementedBy<DALPaymentLog>());

            //rrrrrrrrrrrrrr
            container.Register(Component.For<IRepository<Refund, Guid>, IDALRefund>().ImplementedBy<DALRefund>());
            container.Register(Component.For<IRepository<RefundLog, Guid>, IDALRefundLog>().ImplementedBy<DALRefundLog>());
            container.Register(Component.For<IRepository<ReceptionStatusArchieve, Guid>, IDALReceptionStatusArchieve>().ImplementedBy<DALReceptionStatusArchieve>());
            container.Register(Component.For<IRepository<ReceptionStatus, Guid>, IDALReceptionStatus>().ImplementedBy<DALReceptionStatus>());
            container.Register(Component.For<IRepository<ReceptionChat, Guid>, IDALReceptionChat>().ImplementedBy<DALReceptionChat>());
            container.Register(Component.For<IRepository<ReceptionChatDD, Guid>, IDALReceptionChatDD>().ImplementedBy<DALReceptionChatDD>());


            //ssssssssss
             container.Register(Component.For<IRepository<ServiceOrderAppraise, Guid>, IDALServiceOrderAppraise>().ImplementedBy<DALServiceOrderAppraise>());
            container.Register(Component.For<IRepository<ServiceOrderPushedService, Guid>, IDALServiceOrderPushedService>().ImplementedBy<DALServiceOrderPushedService>());
            container.Register(Component.For<IRepository<ServiceOrderStateChangeHis, Guid>, IDALServiceOrderStateChangeHis>().ImplementedBy<DALServiceOrderStateChangeHis>());

            container.Register(Component.For<IRepository<Staff, Guid>, IDALStaff>().ImplementedBy<DALStaff>());
            container.Register(Component.For<IRepository<ServiceType, Guid>,  IDALServiceType>().ImplementedBy<DALServiceType>());
            container.Register(Component.For<IRepository<ServiceOpenTime, Guid>, IDALServiceOpenTime>().ImplementedBy<DALServiceOpenTime>());
            container.Register(Component.For<IRepository<ServiceOrder, Guid>, IDALServiceOrder>().ImplementedBy<DALServiceOrder>());
            container.Register(Component.For<IRepository<ServiceOrderRemind, Guid>, IDALServiceOrderRemind>().ImplementedBy<DALServiceOrderRemind>());
            container.Register(Component.For<IRepository<OrderAssignment, Guid>, IDALOrderAssignment>().ImplementedBy<DALOrderAssignment>());
 

            container.Register(Component.For<IUnitOfWork>().ImplementedBy<NHUnitOfWork>());

 
           
            
        }
    }
    public class InstallerApplicationService : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<PushService>().DependsOn(
                Dependency.OnValue("bllPayment", new BLLPayment(container.Resolve<IDALPayment>(), container.Resolve<IDALClaims>())),
                Dependency.OnValue("bllServiceOrderStateChangeHis", new BLLServiceOrderStateChangeHis(container.Resolve<IDALServiceOrderStateChangeHis>()))
                ));

            //finance
            container.Register(Component.For<IBLLSharePoint>().ImplementedBy<BLLSharePoint>());
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
                               .DependsOn(Dependency.OnValue("bllServiceOrderStateChangeHis", new BLLServiceOrderStateChangeHis(container.Resolve<IDALServiceOrderStateChangeHis>())))
                               .DependsOn(Dependency.OnValue("bllPayment",new BLLPayment(container.Resolve<IDALPayment>(),container.Resolve<IDALClaims>())))
                               
 
                               )
                               ;

            container.Register(Component.For<BLLPay>().DependsOn(Dependency.OnValue("bllPayment", new BLLPayment(container.Resolve<IDALPayment>(), container.Resolve<IDALClaims>()))));
            
            // IDAL.IDALServiceOrder repoServiceOrder, BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis,
            //IDALMembership repoMembership, IDALRefund bllRefund, IDALOrderAssignment repoOrderAssignment, BLLPayment bllPayment ,IDALClaims dalClaims


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