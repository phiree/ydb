using Castle.Windsor;
using Dianzhu.DependencyInstaller;
using Ydb.Common.Infrastructure;
/// <summary>
/// Summary description for Installer
/// </summary>
namespace Dianzhu.CSClient
{
    public class Bootstrap
{
    static IWindsorContainer container;
    public static IWindsorContainer Container
    {
        get { return container; }
        private set { container = value; }
    }
    public static void Boot()
    {
        container = new WindsorContainer();
       

        container.Install(new Ydb.Infrastructure.Installer());
        container.Install(
                        new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
                        new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(container.Resolve<IEncryptService>()),
                        new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage()
                        );

        container.Install(

           new Ydb.Membership.Infrastructure.InstallerUnitOfWorkMembership(),
           new Ydb.Membership.Infrastructure.InstallerMembership(),
           new Ydb.Membership.Application.InstallerMembershipDB(container.Resolve<IEncryptService>())
            // new Application.InstallerMembershipTestDB()



            );

            container.Install(

            new InstallerComponent(),
               new InstallerInfrstructure(),
               new InstallerRepository(),
               new InstallerApplicationService(),
               new InstallerUI()


           );
            // Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
            AutoMapper.Mapper.Initialize(x =>
        {
            x.AddProfile<Ydb.Membership.Application.ModelToDtoMappingProfile>();
        });
    }


}
}