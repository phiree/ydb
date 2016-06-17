using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALClaims :NHRepositoryBase<Claims,Guid>,IDAL.IDALClaims
    {
        public Claims GetOneByOrder(ServiceOrder order)
        {
            return FindOne(x => x.Order.Id == order.Id);
        }
    }
}
