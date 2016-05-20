using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
using System.Net.Mail;
using System.Net.Configuration;
using System.Net;
using System.Configuration;
using log4net;
namespace JSYK.Infrastructure
{
    public class EmailService : IEmailService
    {
        ILog log = LogManager.GetLogger("JSYK.InfraStructure.EmailService");
        static SmtpClient client = null;
        static string from = string.Empty;
       public   EmailService()
        {
            if (client == null)
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                client = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port);
                client.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                from = smtpSection.From;
            }

        }
        public   void SendEmail(string to, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message+"_"+ex.StackTrace);
            }
        }
    }
}
