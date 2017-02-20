using System;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.BLL;
//using Dianzhu.BLL.Finance;
using Castle.MicroKernel.SubSystems.Configuration;

namespace Dianzhu.DependencyInstaller
{
    public class InstallerComponent : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<BLLAdvertisement>());
          //  container.Register(Component.For<BLLArea>());

        //    container.Register(Component.For<BLLBusiness>());
        //    container.Register(Component.For<BLLBusinessImage>());

   container.Register(Component.For<BLLComplaint>());

          //   container.Register(Component.For<dzServiceService>());
          //  container.Register(Component.For<BLLDZTag>());
          

            container.Register(Component.For<BLLIMUserStatus>());
            container.Register(Component.For<BLLIMUserStatusArchieve>());

 
           

          
         //   container.Register(Component.For<BLLServiceOpenTime>());
        //    container.Register(Component.For<BLLServiceOpenTimeForDay>());
       //     container.Register(Component.For<BLLServiceType>());
            

 
           


            // , BLLPayment bllPayment,BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis






            //20160616_longphui_add
       
     //       container.Register(Component.For<BLLStaff>());






            //20160628_longphui_add
            //container.Register(Component.For<BLL.Client.BLLUserToken>());


            //20160727_longphui_add
            container.Register(Component.For<BLL.BLLStorageFileInfo>());

            //20160802_longphui_add
        


        }
    }
    public class InstallerRepository : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {


            //todo: Registering components by conventions https://github.com/castleproject/Windsor/blob/master/docs/registering-components-by-conventions.md
            container.Register(Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(DAL.NHRepositoryBase<,>)));

            container.Register(Component.For<IRepository<Advertisement, Guid>, IDALAdvertisement>().ImplementedBy<DALAdvertisement>());
          //  container.Register(Component.For<IRepository<Area, int>, IDALArea>().ImplementedBy<DALArea>());

            //bb
         //   container.Register(Component.For<IRepository<Business, Guid>, IDALBusiness>().ImplementedBy<DALBusiness>());
         //   container.Register(Component.For<IRepository<BusinessImage, Guid>, IDALBusinessImage>().ImplementedBy<DALBusinessImage>());



             container.Register(Component.For<IRepository<Complaint, Guid>, IDALComplaint>().ImplementedBy<DALComplaint>());
             //dddddddddddd
          //   container.Register(Component.For<IRepository<DZService, Guid>, IDALDZService>().ImplementedBy<DALDZService>());
            //container.Register(Component.For<IRepository<DZTag, Guid>, IDALDZTag>().ImplementedBy<DALDZTag>());
          
            //fffffffff
            //container.Register(Component.For<IRepository<Model.Finance.BalanceFlow, Guid>, IDAL.Finance.IDALBalanceFlow>().ImplementedBy<DAL.Finance.DALBalanceFlow>());
            //container.Register(Component.For<IRepository<Model.Finance.DefaultSharePoint, Guid>, IDAL.Finance.IDALDefaultSharePoint>().ImplementedBy<DAL.Finance.DALDefaultSharePoint>());


            //container.Register(Component.For<IRepository<Model.Finance.ServiceTypePoint, Guid>, IDAL.Finance.IDALServiceTypePoint>().ImplementedBy<DAL.Finance.DALServiceTypePoint>());


            //container.Register(Component.For<IRepository<Model.Finance.SharePoint, Guid>, IDAL.Finance.IDALSharePoint>().ImplementedBy<DAL.Finance.DALSharePoint>());
            //mmmmmmmmmm
            container.Register(Component.For<IRepository<MembershipLoginLog, Guid>, IDALMembershipLoginLog>().ImplementedBy<DALMembershipLoginLog>());
            //iiiiiiiiii

            container.Register(Component.For<IRepository<IMUserStatus, Guid>, IDALIMUserStatus>().ImplementedBy<DALIMUserStatus>());
            container.Register(Component.For<IRepository<IMUserStatusArchieve, Guid>, IDALIMUserStatusArchieve>().ImplementedBy<DAL.DALIMUserStatusArchieve>());
            //ooooooo
           //ppppppppp
          
            //rrrrrrrrrrrrrr
          

            //ssssssssss
         
           // container.Register(Component.For<IRepository<Staff, Guid>, IDALStaff>().ImplementedBy<DALStaff>());
          //  container.Register(Component.For<IRepository<ServiceType, Guid>, IDALServiceType>().ImplementedBy<DALServiceType>());
          //  container.Register(Component.For<IRepository<ServiceOpenTime, Guid>, IDALServiceOpenTime>().ImplementedBy<DALServiceOpenTime>());
           //  container.Register(Component.For<IRepository<ServiceOpenTimeForDay, Guid>, IDALServiceOpenTimeForDay>().ImplementedBy<DALServiceOpenTimeForDay>());
            //  container.Register(Component.For<IRepository<ServiceOpenTimeForDaySnapShotForOrder, Guid>, IDALServiceOpenTimeForDaySnapShotForOrder>().ImplementedBy<DALServiceOpenTimeForDaySnapShotForOrder>());

         

            //  container.Register(Component.For<IUnitOfWork>().ImplementedBy<NHUnitOfWork_backup>());


            //20160628_longphui_add
            //container.Register(Component.For<IRepository<UserToken, Guid>, IDALUserToken>().ImplementedBy<DAL.Client.DALUserToken>());

            //20160727_longphui_add
            container.Register(Component.For<IRepository<StorageFileInfo, Guid>, IDALStorageFileInfo>().ImplementedBy<DALStorageFileInfo>());

            //20160802_longphui_add
 
        }
    }
    public class InstallerApplicationService : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //finance
 
            //agent
           
            container.Register(Component.For<IBLLMembershipLoginLog>().ImplementedBy<BLLMembershipLoginLog>());
            
         
            // IDAL.IDALServiceOrder repoServiceOrder, BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis,
            //IDALMembership repoMembership, IDALRefund bllRefund, IDALOrderAssignment repoOrderAssignment, BLLPayment bllPayment ,IDALClaims dalClaims

   
          
        }
    }
    public class InstallerInfrstructure : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<CSClient.LocalStorage.LocalChatManager>().ImplementedBy<CSClient.LocalStorage.ChatManagerInMemory>());
        }
    }
}