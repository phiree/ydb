using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping;
using Ydb.Common.Infrastructure;
namespace Ydb.Membership.Application
{
    public class InstallerMembershipDB : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var _sessionFactory = Fluently.Configure()
                      .Database(
                           MySQLConfiguration
                          .Standard

                          .ConnectionString(
                            Ydb.Common.Infrastructure.EncryptService.Decrypt(
                                 System.Configuration.ConfigurationManager
                               .ConnectionStrings["ydb_membership"].ConnectionString, false)
                               )
                    )
                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DZMembershipMap>())
                  .ExposeConfiguration(BuildSchema)
                  .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            container.Register(Component.For<ISessionFactory>().Instance(_sessionFactory).Named("MembershipSessionFactory"));


        }
        private void BuildSchema(Configuration config)
        {
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);
        }
    }
}