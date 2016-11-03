using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Infrastructure;
using System.Net.Configuration;
 
using System.Net;
using System.Net.Mail;
namespace Ydb.Infrastructure
{
    public class EmailService : IEmailService
    {
        static SmtpClient client = null;
        static string from = string.Empty;
        public EmailService()
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
            SendEmail(to, subject, body, null);
        }
        public   void SendEmail(string to, string subject, string body, params string[] cc)
        {
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            if(cc!=null)
            { 
            foreach (string c in cc)
            {
                mail.CC.Add(new MailAddress(c));
            }
            }

            client.Send(mail);
        }
       
    }
}
