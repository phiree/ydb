using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLPaymentLog
    {
        public IDAL.IDALPaymentLog DALPaymentLog;

        public BLLPaymentLog(IDAL.IDALPaymentLog dal)
        {
            DALPaymentLog = dal;
        }

        public void Save(PaymentLog p)
        {
            DALPaymentLog.Add(p);
        }
    }
}
