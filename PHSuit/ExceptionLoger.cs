using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace PHSuit
{
    /// <summary>
    /// ExceptionLoger 的摘要说明
    /// </summary>
    public class ExceptionLoger
    {
        public ExceptionLoger()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static void ExceptionLog(log4net.ILog log, Exception e)
        {
            string message = e.ToString();
            message = message.Replace(Environment.NewLine, "<br/>");

            string emails = ConfigurationManager.AppSettings["MonitorEmails"];
            try {
                string[] emailList = emails.Split(',');
                EmailHelper.SendEmail(emailList[0], "异常_" + log.Logger.Name, message,
                emailList);
            }
            catch(Exception phEx) {
                log.Error(phEx.ToString());
            }
            log.Error(message);
        }
        static int a = 0;
        
        public static void ExceptionLogForLog(log4net.ILog log, Exception e)
        {
            string str = string.Empty;
            if (a > 0)
            {
                str = "InnerException:";
            }
            string err = str + e.Message;
             
                err += "___stack:" + e.StackTrace;
             
            log.Error(err);
            if (e.InnerException != null)
            {
                a++;
                ExceptionLogForLog(log, e.InnerException);
            }
            else
            {
                a = 0;
            }
        }
    } 
}