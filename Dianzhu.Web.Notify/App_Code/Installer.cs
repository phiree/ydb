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
using nhf=FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
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

        container = Dianzhu.DependencyInstaller.Installer.Container;
        string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
        string domain = Dianzhu.Config.Config.GetAppSetting("ImDomain");
        container.Register(Component.For<Dianzhu.CSClient.IInstantMessage.InstantMessage>().ImplementedBy<Dianzhu.CSClient.XMPP.XMPP>()
                            .DependsOn(
                              Dependency.OnValue("server", server)
                            , Dependency.OnValue("domain", domain)
                            , Dependency.OnValue("resourceName",Dianzhu.Model.Enums.enum_XmppResource.YDBan_IMServer.ToString())
                            )
                        );

        container.Register(Component.For<Dianzhu.NotifyCenter.IMNotify>());
       

    }
   
    
  
}