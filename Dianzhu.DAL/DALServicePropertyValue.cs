using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.IDAL;
namespace Dianzhu.DAL
{
   public class DALServicePropertyValue:IDALServicePropertyValue
    {
     
        
       
       IDALBase<ServicePropertyValue> dalBase = null;
       public IDALBase<ServicePropertyValue> DalBase
       {
           get { return new DalBase<ServicePropertyValue>(); }
           set { dalBase = value; }
       }
 
     
    }
}
