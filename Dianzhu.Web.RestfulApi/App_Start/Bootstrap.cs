﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;

//using NHibernate;
using System.Configuration;
//using nhf = FluentNHibernate.Cfg;
//using FluentNHibernate.Cfg.Db;
//using NHibernate.Tool.hbm2ddl;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor.Installer;
using Dianzhu.ApplicationService;
using Ydb.Common.Infrastructure;
using AutoMapper;
namespace Dianzhu.Web.RestfulApi
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
                new Dianzhu.DependencyInstaller.InstallerComponent(),
                new Dianzhu.DependencyInstaller.InstallerInfrstructure(),
                new Dianzhu.DependencyInstaller.InstallerRepository(),
                new Dianzhu.DependencyInstaller.InstallerApplicationService()
                );



            container.Install(
                new Ydb.Infrastructure.Installer()
                );
            container.Install(
new Ydb.InstantMessage.Infrastructure.InstallerUnitOfWorkInstantMessage(),
new Ydb.InstantMessage.Infrastructure.InstallerIntantMessageDB(container.Resolve<IEncryptService>()),
new Ydb.InstantMessage.Infrastructure.InstallerInstantMessage()
                );

            container.Install(

               new Ydb.Membership.Infrastructure.InstallerUnitOfWorkMembership(),
               new Ydb.Membership.Infrastructure.InstallerMembership(),
               new Ydb.Membership.Application.InstallerMembershipDB(container.Resolve<IEncryptService>()),
            // new Application.InstallerMembershipTestDB()
            new InstallerRestfulApi()
                );
           // Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
            Mapper.Initialize(x =>
            {
                x.AddProfile<ApplicationService.Mapping.ModelToDtoMappingProfile>();
                x.AddProfile<ApplicationService.Mapping.DtoToModelMappingProfile>();
                x.AddProfile<Ydb.Membership.Application.ModelToDtoMappingProfile>();
            });
        }
    }
}