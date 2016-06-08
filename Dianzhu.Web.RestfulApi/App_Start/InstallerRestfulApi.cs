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
//using NHibernate;
using System.Configuration;
//using nhf = FluentNHibernate.Cfg;
//using FluentNHibernate.Cfg.Db;
//using NHibernate.Tool.hbm2ddl;
using Castle.MicroKernel.SubSystems.Configuration;
using Dianzhu.ApplicationService;

public class InstallerRestfulApi : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        //container.Register(Component.For<Dianzhu.Web.RestfulApi.Controllers.Users.UsersController>());
        container.Register(Component.For<DALClient>());
        container.Register(Component.For<DALRefreshToken>());
        container.Register(Component.For<Dianzhu.BLL.Client.IBLLClient>().ImplementedBy<Dianzhu.BLL.Client.BLLClient>());
        container.Register(Component.For<Dianzhu.BLL.Client.IBLLRefreshToken>().ImplementedBy<Dianzhu.BLL.Client.BLLRefreshToken>());
        container.Register(Component.For<Dianzhu.ApplicationService.Client.IClientService>().ImplementedBy<Dianzhu.ApplicationService.Client.ClientService>());
        container.Register(Component.For<Dianzhu.ApplicationService.User.IUserService>().ImplementedBy<Dianzhu.ApplicationService.User.UserService>());
    }
}