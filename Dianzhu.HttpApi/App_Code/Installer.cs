using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;



using NHibernate;
using System.Configuration;
using nhf = FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Castle.MicroKernel.SubSystems.Configuration;
/// <summary>
/// Summary description for Installer
/// </summary>
public class InstallerApi : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(Component.For<ResponseFactory>());
        

    }
}