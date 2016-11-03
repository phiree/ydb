using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Infrastructure
{
   public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
        void SendEmail(string to, string subject, string body, params string[] cc);
    }
}
