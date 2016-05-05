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
/// <summary>
/// Summary description for Installer
/// </summary>
public class Installer
{
    static WindsorContainer container;
    public static WindsorContainer Container
    {
        get { return container; }
    }
    static Installer()
    {
        container = new WindsorContainer();

        container.Register(Component.For<BLLAdvertisement>());
        container.Register(Component.For<BLLArea>());

        container.Register(Component.For(typeof(IRepository<>), typeof(NHRepository<>)).ImplementedBy(typeof(NHRepository<>)));
        container.Register(Component.For<IUnitOfWork>().ImplementedBy<NHUnitOfWork>().LifestylePerWebRequest());

        container.Register(Component.For<IRepository<Advertisement>>().ImplementedBy<DALAdvertisement>());
        container.Register(Component.For<IRepository<Area>>().ImplementedBy<DALArea>());




    }
}