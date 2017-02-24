using System;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Ydb.Common
{
    public static class LoggingConfiguration
    {
        /// <summary>
        ///     txt文件的方式保存
        /// </summary>
        public static void Config(string logFilePath)
        {
            var logFileNameRoot = "../logs/" + logFilePath + "/" + Environment.MachineName;
            var hierarchy = (Hierarchy) LogManager.GetRepository();

            hierarchy.Root.RemoveAllAppenders();

            var rootLogger = hierarchy.Root;
            rootLogger.Level = Level.Error;
            /********/
            CreateLogger(hierarchy, Level.Debug, "Ydb", logFileNameRoot + "Ydb", "YdbAppender", 5, 20);
            CreateLogger(hierarchy, Level.Debug, "Dianzhu", logFileNameRoot + "Dianzhu", "DianzhuAppender", 5, 20);
            CreateLogger(hierarchy, Level.Warn, "NHibernate", logFileNameRoot + "Nhibernate", "NhibernateAppender", 5,
                20);
            CreateLogger(hierarchy, Level.Debug, "JSYK", logFileNameRoot + "Infra", "JSYKAppender", 5, 20);
            hierarchy.Configured = true;
        }

        /// <summary>
        ///     MongoDB方式保存
        /// </summary>
        /// <param name="strConn"></param>
        private static void CreateLogger(Hierarchy hierarchy, Level logLevel, string loggerName, string logfileName,
            string appenderName, int maxFileSize, int maxRollBackups)
        {
            var logger = hierarchy.GetLogger(loggerName) as Logger;

            var appenderMain = new RollingFileAppender();
            appenderMain.Name = appenderName;
            appenderMain.AppendToFile = true;
            appenderMain.MaximumFileSize = maxFileSize + "MB";
            appenderMain.MaxSizeRollBackups = maxRollBackups;
            appenderMain.RollingStyle = RollingFileAppender.RollingMode.Size;
            appenderMain.StaticLogFileName = true;
            appenderMain.LockingModel = new FileAppender.MinimalLock();
            appenderMain.File = logfileName + ".log";
            //appenderMain.Layout = new PatternLayout(
            //    "%date [%thread] %-5level %logger- %message%newline");
            // this activates the FileAppender (without it, nothing would be written)

            appenderMain.Layout = new PatternLayout(
                "{date:\"%date\",thread:\"%thread\",leve:\"%-5level\",logger:\"%logger\",filename:\"%file\",linenumber:\"%line\",classname:\"%class\",domain:\"%appdomain\"},message=%message%newline");

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

        private static string Convert(object val)
        {
            var res = val.ToString().Replace("\r", "\\r").Replace("\n", "\\n");
            return res;
        }
    }
}