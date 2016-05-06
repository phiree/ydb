using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PHSuit;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using log4net;
using NHibernate.Context;
using System.Configuration;
using HibernatingRhinos.Profiler.Appender.NHibernate;

namespace Dianzhu.DAL
{
    public class NHUnitOfWork : IDAL.IUnitOfWork
    {

        public static NHUnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }
        [ThreadStatic]
        private static NHUnitOfWork _current;
        private readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; private set; }
        

        public NHUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;// CreateNhSessionFactory(); 
            Current = this;
        }

        public void BeginTransaction()
        {

            Session = _sessionFactory.OpenSession();

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
            finally
            {
                Session.Close();

            }
        }
        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            finally
            {
                Session.Close();
            }
        }

        private static ISessionFactory CreateNhSessionFactory()
        {
            var connStr = ConfigurationManager.ConnectionStrings["PhoneBook"].ConnectionString;
            //var f12= Fluently.Configure()
            //    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connStr))
            //    .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetAssembly(typeof(PersonMap))))

            //    .BuildSessionFactory();
            var f = Fluently.Configure()
                        .Database(
                             MySQLConfiguration
                            .Standard
                            .ConnectionString(
                               connStr
                                     )

                          )
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dianzhu.DAL.Mapping.AdvertisementMap>())
                         .ExposeConfiguration(BuildSchema)
                        .BuildSessionFactory();
            NHibernateProfiler.Initialize();
            return f;
        }


        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            SchemaUpdate update = new SchemaUpdate(config);
            update.Execute(true, true);
        }
    }

}
