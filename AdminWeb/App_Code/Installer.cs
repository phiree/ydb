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
       
        container.Register(Component.For<VMCustomerAdapter>());

    }
   
    
  
}