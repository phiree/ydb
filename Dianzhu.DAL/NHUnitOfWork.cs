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
                       .ExposeConfiguration(BuildSchema)
                        .BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

        }
        private static void BuildSchema(Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            SchemaUpdate update = new SchemaUpdate(config);
            //update.Execute(true, true);
            config.SetProperty("current_session_context_class", "thread_static");
        }
        public static ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory))
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());

            return _sessionFactory.GetCurrentSession();
        }

        public static void DisposeCurrentSession()
        {
            ISession currentSession = CurrentSessionContext.Unbind(_sessionFactory);

            currentSession.Close();
            currentSession.Dispose();
        }

        public NHUnitOfWork()
        {
          
           //Session = _sessionFactory.OpenSession();
            Session= GetCurrentSession();
            
        }

        public void BeginTransaction()
        {
            if(!Session.IsOpen)
            { 
                Session = _sessionFactory.OpenSession();
            }
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
    }
   
}
