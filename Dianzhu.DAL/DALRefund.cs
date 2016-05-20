using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate.Criterion;

namespace Dianzhu.DAL
{
    public class DALRefund : NHRepositoryBase<Refund, Guid>, IDAL.IDALRefund
    {
         

        public Refund GetRefundByPlatformTradeNo(string platformTradeNo)
        {
            return FindOne( x => x.PlatformTradeNo == platformTradeNo);
        }
    }
}
