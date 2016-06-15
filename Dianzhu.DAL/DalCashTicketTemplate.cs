using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
   public class DALCashTicketTemplate:NHRepositoryBase<CashTicketTemplate,Guid>,IDAL.IDALCashTicketTemplate
    {
        
       public IList<CashTicketTemplate> GetListByBusiness(Business business)
       {
           return Find(x=>x.Business.Id==business.Id);

       }
    }
}
