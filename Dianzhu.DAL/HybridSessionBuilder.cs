using System.Web;

using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate.Tool.hbm2ddl;
using NHibernate.Hql;
using NHibernate.Criterion.Lambda;
namespace Dianzhu.DAL
{
    /// <summary>
    /// Nhibernate Session工厂.
    /// </summary>
    public class HybridSessionBuilder
    {
        private static ISession _currentSession;
        private static ISessionFactory _sessionFactory;

        public ISession GetSession()
        {
            ISessionFactory factory = getSessionFactory();
            ISession session = getExistingOrNewSession(factory);

            return session;
        }

        public Configuration GetConfiguration()
        {
            var configuration = new Configuration();
            configuration.Configure();
            return configuration;
        }

        private static readonly object __lock = new object();

        private ISessionFactory getSessionFactory()
        {
            lock (__lock)
            {
                if (_sessionFactory == null)
                {

                    _sessionFactory = Fluently.Configure()
                        .Database(
                             MySQLConfiguration
                            .Standard
                            .ConnectionString(
                                Dianzhu.Config.Config.ConnectionString
                                 )
                      )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dianzhu.DAL.Mapping.CashTicketMap>())
                   .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
                    HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
                }

            }
            return _sessionFactory;
        }
        private static void BuildSchema(Configuration config)
        {
             

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            SchemaUpdate update = new SchemaUpdate(config);
       //    update.Execute(true, true);

        }
        private static void GetUpdateScript(string ss)
        {
            throw new System.Exception(ss);
        }
        private ISession getExistingOrNewSession(ISessionFactory factory)
        {
            if (HttpContext.Current != null)
            {
                ISession session = GetExistingWebSession();
                if (session == null)
                {
                    session = openSessionAndAddToContext(factory);
                }
                else if (!session.IsOpen)
                {
                    session = openSessionAndAddToContext(factory);
                }

                return session;
            }

            if (_currentSession == null)
            {
                _currentSession = factory.OpenSession();
            }
            else if (!_currentSession.IsOpen)
            {
                _currentSession = factory.OpenSession();
            }

            return _currentSession;
        }

        public ISession GetExistingWebSession()
        {
            return HttpContext.Current.Items[GetType().FullName] as ISession;
        }

        private ISession openSessionAndAddToContext(ISessionFactory factory)
        {
            ISession session = factory.OpenSession();
            HttpContext.Current.Items.Remove(GetType().FullName);
            HttpContext.Current.Items.Add(GetType().FullName, session);
            return session;
        }

        public static void ResetSession()
        {
            var builder = new HybridSessionBuilder();
            builder.GetSession().Dispose();
        }
    }
}