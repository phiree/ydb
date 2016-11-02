using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ydb.Membership.Application;
using Ydb.InstantMessage.Infrastructure;
namespace Ydb.Test.Integration
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
            container.Install(
                    new Ydb.Infrastructure.Installer(),
                      new Ydb.Membership.Infrastructure.InstallerUnitOfWorkMembership(),
                            new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
              
              
                  new InstallerMembershipTestDB(),
              
           
                new InstallerInstantMessageTestDB(),
                    new Ydb.InstantMessage.Infrastructure.InstallerIntantMessage(),
                      new Ydb.Membership.Application.InstallerMembership()


                );


        }


    }
}
