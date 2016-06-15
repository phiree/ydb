using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALClaims :NHRepositoryBase<Claims,Guid>
    {
         
        
        public Claims GetOneByOrder(ServiceOrder order)
        {
            return Session.QueryOver<Claims>().Where(x => x.Order == order).SingleOrDefault();
        }
    }
}
