using System;
using System.IO;
using System.Xml;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateUnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private const string Default_HibernateConfig = "hibernate.cfg.xml";

         [ThreadStatic]
        private static ISession _currentSession;
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        internal UnitOfWorkFactory()
        { }

        public IUnitOfWork Create()
        {
            ISession session = CreateSession();
            session.FlushMode = FlushMode.Commit;
            _currentSession = session;
            return new UnitOfWorkImplementor(this, session);
        }

        public Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    /*
                   var   _configuration = new Configuration();
                     string hibernateConfig = Default_HibernateConfig;
                     //if not rooted, assume path from base directory
                     if (Path.IsPathRooted(hibernateConfig) == false)
                         hibernateConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, hibernateConfig);
                     if (File.Exists(hibernateConfig))
                         _configuration.Configure(new XmlTextReader(hibernateConfig));
                         */
                     _configuration = Fluently.Configure()
                      .Database(
                           MySQLConfiguration
                          .Standard
                          .ConnectionString(
                               PHSuit.Security.Decrypt(
                             System.Configuration.ConfigurationManager
                             .ConnectionStrings["DianzhuConnectionString"].ConnectionString,false)
                             )


                        )
                      .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dianzhu.DAL.Mapping.AreaMap>())
 
                       .ExposeConfiguration(BuildSchema)
 
 
                      .BuildConfiguration();
                      

                }
                HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
                return _configuration;
            }
        }
        private static void BuildSchema(Configuration config)
        {

            SchemaUpdate update = new SchemaUpdate(config);
            if (System.Configuration.ConfigurationManager.AppSettings["UpdateSchema"] == "1")
            {
                update.Execute(true, true);
            }
           

        }
        object lockObject = new object();
        public ISessionFactory SessionFactory
        {
            get
            {
                lock (lockObject)
                { 
                    if (_sessionFactory == null)
                    _sessionFactory = Configuration.BuildSessionFactory();
                return _sessionFactory;
                }
            }
        }

        public ISession CurrentSession
        {
            get
            {
                if (_currentSession == null)
                    throw new InvalidOperationException("You are not in a unit of work.");
                if (!_currentSession.Transaction.IsActive)
                {
                    _currentSession.BeginTransaction();
                }
                return _currentSession;
            }
            set { _currentSession = value; }
        }

        public void DisposeUnitOfWork(UnitOfWorkImplementor adapter)
        {
            CurrentSession = null;
            UnitOfWork.DisposeUnitOfWork(adapter);
        }

        private ISession CreateSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}