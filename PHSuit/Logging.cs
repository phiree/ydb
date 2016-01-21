using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;
using log4net;
using log4net.Repository;

namespace PHSuit
{
    public class Logging
    {
       
         static  Logging()
        {
          //  Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
           // ILayout layout= CreateLayout();
           // IAppender appender = CreateAppender(layout,"Log");
            //IAppender appenderNhibernate = CreateAppender(layout, "NHibernate");
            //IAppender appenderNhibernateSQL = CreateAppender(layout, "NHibernate.SQL");

            //hierarchy.Root.AddAppender(roller);
            //Logger logger = CreateLogger("Dianzhu", appender, hierarchy);
            //Logger loggerNhibernate = CreateLogger("NHibernate", appenderNhibernate, hierarchy);
            //Logger loggerNhibernateSQL = CreateLogger("NHibernate.SQL", appenderNhibernateSQL, hierarchy);

            //hierarchy.Root.Level = Level.Debug;
            //hierarchy.Configured = true;
            log4net.Config.BasicConfigurator.Configure();//,appenderNhibernate,appenderNhibernateSQL);
             
        }
        static ILayout CreateLayout()
        {
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();
            return patternLayout;
        }
        static void AddAppender(ILog log, IAppender appender)
        {
         
            Logger l =(Logger) log.Logger;
            l.AddAppender(appender);
        }
        static IAppender CreateAppender(ILayout layout,string coreFileName)
        {
            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            
            roller.File = @"logs/" + System.Environment.MachineName + "_"+coreFileName + ".log";
            roller.Layout = layout;
            roller.MaximumFileSize = "1MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            return roller;
        }
        static Logger CreateLogger(string name,IAppender appender, Hierarchy hierarchy)
        {
            var logger = hierarchy.LoggerFactory.CreateLogger(null, name);
            logger.AddAppender(appender);
            logger.Level = Level.Debug;
             logger.Parent =hierarchy.Root ;

            return logger;
        } 
       
        public static ILog GetLog(string loggerName)
        {
           ILog[] loggers= LogManager.GetCurrentLoggers();
            
            log4net.ILog log = LogManager.GetLogger(loggerName);
            
            Logger l = (Logger)log.Logger;
            if (loggerName.ToLower().Contains("nhibernate"))
            {
                l.Level = Level.Error;
                
            }
            else
            {

            }
            AddAppender(log, CreateAppender(CreateLayout(), loggerName));
            foreach (ILog one in loggers)
            {
                log.Debug(((Logger)one.Logger).Name);
            }
            return log;
        }

    }
}