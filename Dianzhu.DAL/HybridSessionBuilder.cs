using System.Web;

using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate.Tool.hbm2ddl;
using NHibernate.Hql;
using NHibernate.Criterion.Lambda;
using System;
using System.Security.Cryptography;
using System.Text;
using NHibernate.Context;

namespace Dianzhu.DAL_Hyber
{
    /// <summary>
    /// Nhibernate Session工厂.
    /// </summary>
    public class HybridSessionBuilder 
    {
        
        private static ISessionFactory _sessionFactory;

        public HybridSessionBuilder()
        {
            _sessionFactory = getSessionFactory();
        }
        public ISession GetSession()
        {
            ISession session=null;
            if (HttpContext.Current != null)
            {
                 session = GetExistingWebSession();
                if (session == null || !session.IsOpen)
                {
                    session = openSessionAndAddToContext();
                }
                else
                {
                    throw new Exception("Session Error");
                } 

                return session;
            }

            if (session == null || !session.IsOpen)
            {
                session = _sessionFactory.OpenSession();
            }
            else
            {
                throw new Exception("Session Error");
            }

            return session; 

        }
 
        
        private static readonly object __lock = new object();

        /// <summary>
        /// 多线程的单件模式
        /// http://www.yoda.arachsys.com/csharp/singleton.html
        /// </summary>
        /// <returns></returns>
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
                           PHSuit.Security.Decrypt(
                             System.Configuration.ConfigurationManager
                             .ConnectionStrings["DianzhuConnectionString"].ConnectionString, false)
                                   )
                                  
                        )
                      .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dianzhu.DAL.Mapping.CashTicketMap>())
                     .ExposeConfiguration(BuildSchema)
                      //  .CurrentSessionContext<ThreadStaticSessionContext>()
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

            //update.Execute(true, true);

        }
        
        public ISession GetExistingWebSession()
        {
            return HttpContext.Current.Items[GetType().FullName] as ISession;
        }

        private ISession openSessionAndAddToContext( )
        {
            ISession session = _sessionFactory.OpenSession();
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