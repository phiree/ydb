using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.DAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.Web;
using System.IO;

namespace Dianzhu.BLL
{
   public class BLLPaymentLog
    {
       
        public DALPaymentLog DALPaymentLog=DALFactory.DALPaymentLog;

        public void SaveOrUpdate(PaymentLog PaymentLog)
        {
            DALPaymentLog.SaveOrUpdate(PaymentLog);
        }
    }
}
