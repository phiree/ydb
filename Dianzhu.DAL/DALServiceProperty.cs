using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.IDAL;
namespace Dianzhu.DAL
{
   public class DALServiceProperty:IDALServiceProperty
    {
     
        
       
       IDALBase<ServiceProperty> dalBase = null;
       public IDALBase<ServiceProperty> DalBase
       {
           get { return new DalBase<ServiceProperty>(); }
           set { dalBase = value; }
       }
 

       public IList<ServiceProperty> GetList(Guid serviceTypeId)
       {
         return   DalBase.GetList("select p from ServiceProperty where p.ServiceType.id='"+serviceTypeId+"'");
       }
       public void   Save(ServiceProperty property)
       {
            DalBase.Save(property);
       }
     
    }
}
