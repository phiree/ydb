using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace Dianzhu.DAL
{
    public class DALPaymentLog : NHRepositoryBase<Model.PaymentLog,Guid>,IDAL.IDALPaymentLog
    {

    }
}
