using System;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Dianzhu.DAL
{
    public class DianzhuUW2
    {
        // [ThreadStatic]
        //use httpcontext.current.items instead http://piers7.blogspot.com/2005/11/threadstatic-callcontext-and_02.html
        static ISession _session;
        public static ISession Session
        {
            get {
                if (_session == null)
                {
                    _session = sessionFactory.OpenSession();
                }
               
                if (!_session.Transaction.IsActive)
                {
                    _session.BeginTransaction();
                }
                return _session;
            }
        }
        static ISessionFactory sessionFactory;
        static  DianzhuUW2()
        {
           var _configuration = Fluently.Configure()
                      .Database(
                           MySQLConfiguration
                          .Standard
                          .ConnectionString(
                               PHSuit.Security.Decrypt(
                             System.Configuration.ConfigurationManager
                             .ConnectionStrings["DianzhuConnectionString"].ConnectionString, false)
                             )


                        )
                      .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dianzhu.DAL.Mapping.AreaMap>())
                      .BuildConfiguration();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            sessionFactory = _configuration.BuildSessionFactory();


        }

        public static void EndSession()
        {
            
                //.......
                using (_session.Transaction)
                {
                    try
                    {
                    _session.Transaction.Commit();
                    }
                    catch (Exception)
                    {
                    _session.Transaction.Rollback();
                     throw;
                    }
                }
            
           
        }

    }
}