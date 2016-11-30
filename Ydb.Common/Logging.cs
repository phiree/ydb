using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;
using log4net;
using log4net.Repository;
using System.Xml;
using System.IO;
using System.Text;
using System;
using System.Linq;
using Log4Mongo;

namespace Ydb.Common
{
 
    public static class LoggingConfiguration
    {
          /// <summary>
        /// Configures log4net
        /// </summary>
        public static void Config(string logFilePath)
        {
            
            string logFileNameRoot = "../logs/" + logFilePath + "/" + System.Environment.MachineName;
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
          
            hierarchy.Root.RemoveAllAppenders();
            
            Logger rootLogger = hierarchy.Root;
            rootLogger.Level = Level.Error;
            /********/
            CreateLoggerMogo(hierarchy, log4net.Core.Level.Debug, "Ydb", logFileNameRoot+"Ydb", "YdbAppender", 5, 20);
            CreateLoggerMogo(hierarchy, log4net.Core.Level.Warn, "NHibernate", logFileNameRoot+"Nhibernate", "NhibernateAppender", 5, 20);
            hierarchy.Configured = true;
           
        }
        private static void CreateLogger(Hierarchy hierarchy,string loggerName,string logfileName, string appenderName,int maxFileSize,int maxRollBackups)
        {
            Logger logger = hierarchy.GetLogger(loggerName) as Logger;

            RollingFileAppender appenderMain = new RollingFileAppender();
            appenderMain.Name = appenderName;
            appenderMain.AppendToFile = true;
            appenderMain.MaximumFileSize =maxFileSize+ "MB";
            appenderMain.MaxSizeRollBackups = maxRollBackups;
            appenderMain.RollingStyle = RollingFileAppender.RollingMode.Size;
            appenderMain.StaticLogFileName = true;
            appenderMain.LockingModel = new FileAppender.MinimalLock();
            appenderMain.File = logfileName + ".log";
            appenderMain.Layout = new PatternLayout(
                "%date [%thread] %-5level %logger- %message%newline");
            // this activates the FileAppender (without it, nothing would be written)
            appenderMain.ActivateOptions();
 
            logger.Level = Level.Debug;
            logger.AddAppender(appenderMain);
        }
        private static void CreateLoggerMogo(Hierarchy hierarchy,log4net.Core.Level logLevel, string loggerName, string logfileName, string appenderName, int maxFileSize, int maxRollBackups)
        {
            Logger logger = hierarchy.GetLogger(loggerName) as Logger;

            
             MongoDBAppender appenderMain = new MongoDBAppender();
            appenderMain.ConnectionString = "mongodb://localhost";
            appenderMain.Name = appenderName;
 
            appenderMain.Layout = new PatternLayout(
                "%date [%thread] %-5level %logger- %message%newline");
            // this activates the FileAppender (without it, nothing would be written)
            appenderMain.ActivateOptions();

            logger.Level = logLevel;
            logger.AddAppender(appenderMain);
        }
    }
    public class RollingFileAppenderRemoveNewLine : RollingFileAppender
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            var val = Convert(loggingEvent);
            base.Append(val);
        }

        protected override void Append(LoggingEvent[] loggingEvents)
        {
            var vals = loggingEvents.Select(Convert).ToArray();
            base.Append(vals);
        }

        private static LoggingEvent Convert(LoggingEvent loggingEvent)
        {
            var eventData = loggingEvent.GetLoggingEventData();
            eventData.ExceptionString = Convert(eventData.ExceptionString);
            eventData.Message = Convert(eventData.Message);
            var val = new LoggingEvent(eventData);
            return val;
        }

        static string Convert(object val)
        {
            var res = val.ToString().Replace("\r", "\\r").Replace("\n", "\\n");
            return res;
        }
    }

}