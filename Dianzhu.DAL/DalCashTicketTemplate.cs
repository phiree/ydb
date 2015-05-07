using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.IDAL;
namespace Dianzhu.DAL
{
   public class DALCashTicketTemplate:IDALCashTicketTemplate
    {
       IDALBase<CashTicketTemplate> dalBase = null;
       public IDALBase<CashTicketTemplate> DalBase{
           get { return new DalBase<CashTicketTemplate>(); }
           set { dalBase = value; }
       }
    }
}
