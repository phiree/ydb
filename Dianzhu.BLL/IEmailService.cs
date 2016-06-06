using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL
{
   public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}
