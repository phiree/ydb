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

namespace PHSuit
{
    public static class Logging
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
            CreateLogger(hierarchy, "Ydb", logFileNameRoot + "Ydb", "DianzhuAppender", 5, 20);
            CreateLogger(hierarchy, "Dianzhu", logFileNameRoot+"Dianzhu", "DianzhuAppender", 5, 20);
            CreateLogger(hierarchy, "NHibernate", logFileNameRoot+"Nhibernate", "NhibernateAppender", 5, 20);
            CreateLogger(hierarchy, "JSYK", logFileNameRoot+"Infra", "DianzhuAppender", 5, 20);
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