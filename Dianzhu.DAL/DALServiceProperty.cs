using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.IDAL;
namespace Dianzhu.DAL
{
   public class DALServiceProperty:DALBase<ServiceProperty>
    {
     
        
       
       
 

       public IList<ServiceProperty> GetList(Guid serviceTypeId)
       {
         return   GetList("select p from ServiceProperty p where p.ServiceType.id='"+serviceTypeId+"'");
       }
       public void   Save(ServiceProperty property)
       {
            Save(property);
       }
     
    }
}
