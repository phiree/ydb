using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PHSuit;
using NHibernate.Tool.hbm2ddl;

namespace Dianzhu.DAL
{
    public class NHUnitOfWork :IDAL.IUnitOfWork
    {
        private static readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; private set; }

        static NHUnitOfWork()
        {
            _sessionFactory = Fluently.Configure()
                        .Database(
                             MySQLConfiguration
                            .Standard
                            .ConnectionString(
                               PHSuit.Security. Decrypt(
                               System.Configuration.ConfigurationManager
                               .ConnectionStrings["DianzhuConnectionString"].ConnectionString, false)
                                     )
                                     .Dialect<NHCustomDialect>()
                          )
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dianzhu.DAL.Mapping.CashTicketMap>())
                       .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
                        .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

        }

        public NHUnitOfWork()
        {
            Session = _sessionFactory.OpenSession();
        }

        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            //finally
            //{
            //    Session.Close();
            //}
        }
    }
}
