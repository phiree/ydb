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
            // Remove any other appenders
            hierarchy.Root.RemoveAllAppenders();
            // define some basic settings for the root
            Logger rootLogger = hierarchy.Root;
            rootLogger.Level = Level.Debug;
       
            // declare a RollingFileAppender with 5MB per file and max. 10 files
            RollingFileAppender appenderNH = new RollingFileAppenderRemoveNewLine();
            appenderNH.Name = "RollingLogFileAppenderNHibernate";
            appenderNH.AppendToFile = true;
            appenderNH.MaximumFileSize = "1MB";
            appenderNH.MaxSizeRollBackups = 10;
            appenderNH.RollingStyle = RollingFileAppender.RollingMode.Size;
            appenderNH.StaticLogFileName = true;
            appenderNH.LockingModel = new FileAppender.MinimalLock();
            appenderNH.File = logFileNameRoot + "_nhibernate.log";
            appenderNH.Layout = new PatternLayout("%date [%thread] %-5level %logger - %message%newline");
            // this activates the FileAppender (without it, nothing would be written)
            appenderNH.ActivateOptions();

            // This is required, so that we can access the Logger by using 
            // LogManager.GetLogger("NHibernate.SQL") and it can used by NHibernate
            Logger loggerNH = hierarchy.GetLogger("NHibernate") as Logger;
            loggerNH.Level = Level.Debug;
            loggerNH.AddAppender(appenderNH);

            // declare RollingFileAppender with 5MB per file and max. 10 files
            Logger logger = hierarchy.GetLogger("Dianzhu") as Logger;

            RollingFileAppender appenderMain = new RollingFileAppenderRemoveNewLine();
            appenderMain.Name = "RollingLogFileAppenderMyProgram";
            appenderMain.AppendToFile = true;
            appenderMain.MaximumFileSize = "2MB";
            appenderMain.MaxSizeRollBackups = 10;
            appenderMain.RollingStyle = RollingFileAppender.RollingMode.Size;
            appenderMain.StaticLogFileName = true;
            appenderMain.LockingModel = new FileAppender.MinimalLock();
            appenderMain.File = logFileNameRoot + ".log";
            appenderMain.Layout = new PatternLayout(
                "%date [%thread] %-5level %logger- %message%newline");
            // this activates the FileAppender (without it, nothing would be written)
            appenderMain.ActivateOptions();

            // This is required, so that we can access the Logger by using 
            // LogManager.GetLogger("MyProgram") 
            
            
            logger.Level = Level.Debug;
            logger.AddAppender(appenderMain);

            // this is required to tell log4net that we're done 
            // with the configuration, so the logging can start
            hierarchy.Configured = true;
           // log4net.Config.BasicConfigurator.Configure();
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