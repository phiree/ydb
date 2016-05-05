using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.Windsor.Configuration;
using Castle.MicroKernel.Registration;
using Dianzhu.Model;
using Dianzhu.IDAL;
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

      
        container.Register(Component.For<BLLArea>());

        //container.Register(Component.For(typeof(IRepository<>), typeof(MemoryRepository<>)).ImplementedBy(typeof(MemoryRepository<>)));
        //container.Register(Component.For<IUnitOfWork>().ImplementedBy<MemoryUnitOfWork>());
        //List<Area> areaList = new List<Area>
        //{ new Area { AreaOrder = 1, Code = "110000", Id = 1, MetaDescription = "desp11", Name = "Beijing", SeoName = "BJ" } };

        //container.Register(Component.For<IRepository<Area>>().ImplementedBy<Dianzhu.Test.Repository.Resource.MemoryRepositoryArea>()
        //                    .DependsOn(Dependency.OnValue("list",areaList))
        //    );




    }
}