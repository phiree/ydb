﻿using System;
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
using Dianzhu.NotifyCenter;
using Castle.MicroKernel.SubSystems.Configuration;
/// <summary>
/// Summary description for Installer
/// </summary>
 

public class  InstallerNofity : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {   
        
        //container.Register(Component.For<IMNotify>()
        //    .DependsOn(Dependency.OnComponent<IIMSession,IMSessionsOpenfire>())
        //    );
        container.Register(Component.For<Dianzhu.NotifyCenter.IMNotify>().DependsOn(Dependency.OnValue("assigner",container.Resolve<ReceptionAssigner>("OpenFireRestAssigner"))));


    }
}