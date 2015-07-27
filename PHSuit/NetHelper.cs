using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Configuration;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace PHSuit
{
    public class EmailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        static SmtpClient client = null;
        static string from = string.Empty;
        static EmailHelper()
        {
            if (client == null)
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                client = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port);
                client.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                from = smtpSection.From;
            }
    
        }
        public static void SendEmail(string to, string subject,string body)
        {
            MailMessage mail = new MailMessage(from,to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            client.Send(mail);
        }
    }
}
